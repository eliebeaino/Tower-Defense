using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform towerParentTransform;

    Queue<Tower> towerQueue = new Queue<Tower>();

    // build a tower when its called or move old one if limit reached
    public void AddTower(Waypoint waypoint)
    {
        if (towerLimit > 0)
        {
            InstantiateNewTower(waypoint);
            towerLimit--;
        }
        else
        {
            MoveExistingTower(waypoint);
        }
    }

    // builds new tower and stops the tile from being placeable - stores waypoint for tower - adds to queue
    private void InstantiateNewTower(Waypoint waypoint)
    {
        Tower newTower =  Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity);

        newTower.transform.parent = towerParentTransform; // move to parent in hierarchy

        newTower.baseWaypoint = waypoint;
        waypoint.isPlacable = false;

        towerQueue.Enqueue(newTower);
     }

    // moves existing towers when queue is full and edit the placeable variables for old/new waypoint
    private void MoveExistingTower(Waypoint waypoint)
    {
        Tower oldTower = towerQueue.Dequeue();
        oldTower.baseWaypoint.isPlacable = true;
        oldTower.transform.position = waypoint.transform.position;
        oldTower.baseWaypoint = waypoint;
        waypoint.isPlacable = false;
        towerQueue.Enqueue(oldTower);
    }
}
