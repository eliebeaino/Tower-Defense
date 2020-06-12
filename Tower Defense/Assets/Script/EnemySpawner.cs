using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyType;
    [SerializeField] [Range(0.1f, 100f)] float secondsBetweenSpawns = 2f;
    [SerializeField] int numberOfEnemies = 10;                                  // enemy limit count
    [SerializeField] Transform enemyParentTransform;

    // Coroutine to spawn enemies
    private IEnumerator Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var enemySpawned = Instantiate(enemyType, transform.position, Quaternion.identity);
            enemySpawned.transform.parent = enemyParentTransform;               // move to parent in hierarchy
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    } 
}
