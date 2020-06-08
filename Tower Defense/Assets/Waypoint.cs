using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // ok to use as this is data class
    public bool isExplored = false;
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

    // public method to change the cube color
    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }
}
