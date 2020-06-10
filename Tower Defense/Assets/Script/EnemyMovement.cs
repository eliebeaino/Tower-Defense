using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Tooltip("Enemy movement speed accross tiles in secs")]
    float EnemySpeed = 1f;

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
            yield return new WaitForSeconds(EnemySpeed);
        }
    }
}
