using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();   // store coords of each waypoint
    Queue<Waypoint> queue = new Queue<Waypoint>();                                    // queue list of waypoints for searching algorithm
    bool isRunning = true;                                                            // bool to check if the end is found and stop searching
    Waypoint searchCenter;                                                            // the current center block of the search
    List<Waypoint> path = new List<Waypoint>();                                       // path to be stored in from the algorithm search

    Vector2Int[] directions =
        {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
        };                                                     // directions presets to search around the center block


    // providing the path list to be called from the enemy movement .cs
    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            BreadthFirstSearch();
            CreatePath();
        }
        return path;
    }

    // Load up all the blocks in game into the dictionary excluding overlaps
    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if (isOverlapping)
            {
                Debug.LogWarning("Skipping overlapping Block " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    } 

    // pathfinding sequence starts by checking if its running, queueing the start than dequeing it, stopping if it reached the end, exploring neighbouring blocks if it doesn't.
    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count >0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbours();
        }
    }

    // if the current center block is the end waypoint, stop pathfinding
    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    // exploring neighbour WPs if they exist and queueing them
    private void ExploreNeighbours()
    {
        if (!isRunning) return;

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }   
        }
    }

    // adding WP neighbours to queue by grabbing it from dictionary  -- while making sure its not already in queue list or  explored already -- Store WP variable of explorer
    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        { 
            //do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter; // store coords of WP that explored the neighbouur 
        }
    }

    // create the path discovered from the algorithm working backwards - then reversing it.
    private void CreatePath()
    {
        path.Add(endWaypoint);
        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }
        path.Add(startWaypoint);
        path.Reverse();
    }
}
