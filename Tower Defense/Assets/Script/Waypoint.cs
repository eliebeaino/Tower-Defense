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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlacable)
            {
                print(gameObject.name);
            }
            else
            {
                print("You can't place that here!");
            }
        }
    }
}
