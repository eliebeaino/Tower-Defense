using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Tooltip("Enemy movement speed accross tiles in secs")]
    float EnemySpeed = 2f;

    void Start()
    {         
        // getting the path from the pathfinder.cs and calling the enemy to move on it.
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    //// enemy movement sequence on given path
    //IEnumerator FollowPath(List<Waypoint> path)
    //{
    //    foreach (Waypoint waypoint in path)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, EnemySpeed);
    //        //transform.position = waypoint.transform.position;
    //        yield return new WaitForSeconds(EnemySpeed);
    //    }
    //}

    int i = 0;
    IEnumerator FollowPath(List<Waypoint> path)
    {
        //yield return null;
        for (; i < path.Count; i++)
        {
            Vector3 randomValue = new Vector3(1, 0, 1) * Random.Range(-0.5f, 0.5f) + path[i].transform.position;
            transform.LookAt(randomValue);
            while (Vector3.Distance(transform.position, randomValue) >= 0.35f)
            {
                transform.position = Vector3.MoveTowards(transform.position, randomValue, Time.deltaTime * EnemySpeed);
                yield return null;
            }
        }
    }
}
