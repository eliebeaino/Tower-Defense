using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class Waypoint : MonoBehaviour
{
    // ok to use as this is data class
    public bool isExplored = false;  // explored in BFS ?
    public bool isPlacable = true;   // buildeable slot ?
    public Waypoint exploredFrom;

    const int gridSize = 10;

    // public method to get the CONST grid size
    public int GetGridSize()
    {
        return gridSize;
    }

    // public method to get the coords of WP
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / gridSize),
        Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    // get the current location of mouse - if left click is down, build a tower
    private void OnMouseOver()
    {
        HighlightBlocks();
        BuildTowers();
    }

    // build towers on mouse clock
    private void BuildTowers()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlacable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                print("You can't place that here!");
            }
        }
    }

    // highlight buildeable tiles or stop when built upon
    private void HighlightBlocks()
    {
        if (isPlacable)
        {
            this.GetComponentInChildren<Renderer>().material.SetFloat("_Metallic", 0.5f);
        }
        else
        {
            this.GetComponentInChildren<Renderer>().material.SetFloat("_Metallic", 0f);
        }
    }

    // revert the highlight on mouse exit
    private void OnMouseExit()
    {
        this.GetComponentInChildren<Renderer>().material.SetFloat("_Metallic", 0f);
    }
}
