using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] [Range(0.1f, 100f)] float secondsBetweenSpawns = 2f;
    [SerializeField] int numberOfEnemies = 10;


    // Start is called before the first frame update
    private IEnumerator Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    } 

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
