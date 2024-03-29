﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]

public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize,
            0,
            waypoint.GetGridPos().y * gridSize
            );
    }

    private void UpdateLabel()
    {
        int gridSize = waypoint.GetGridSize();
        string coordText = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;

        //TextMesh textMesh = GetComponentInChildren<TextMesh>();
        //textMesh.text = coordText;

        gameObject.name = coordText;
    }

}
