using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float timer = 1f; // time buffer for enemy movement

    // Start is called before the first frame update
    void Start()
    {
        // getting the path from the pathfinder.cs and calling the enemy to move on it.
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    // enemy movement sequence on given path
    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(timer);
        }
    }

}
