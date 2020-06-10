using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("General:")]
    [SerializeField] Transform catapult;
    Transform targetEnemy;

    [SerializeField] float towerRange = 10f;
    [SerializeField] ParticleSystem projectilePartile;


    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            LookAtEnemy();
            ProcessFiring();
        }
    }

    // defining the target enemy
    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if(sceneEnemies.Length ==0)
        {
            return;
        }                                 // it theres no enmies in game, return
        Transform closestEnemy = sceneEnemies[0].transform;            // setting the first enemy found as the closest enemy in array
        
        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    // searching through all enemies for the closest one
    private Transform GetClosest(Transform closestEnemy, Transform testEnemy)
    {
        if (Vector3.Distance(closestEnemy.position, gameObject.transform.position) <
            Vector3.Distance(testEnemy.position, gameObject.transform.position))
        {
            return closestEnemy;
        }
        return testEnemy;
    }

    // Rotate towards enemy
    private void LookAtEnemy()
    {
        catapult.LookAt(targetEnemy);
    }

    // check for enemy nearby to start shooting or turn off if not
    private void ProcessFiring()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position); //gameobject instead of catapult bcz gameobject refers to the tower initial position
        print(distanceToEnemy);
        if (distanceToEnemy <= towerRange)
        {
            FireAtEnemy(true);
        }
        else
        {
            FireAtEnemy(false);
        }
    }

    // Fires at the Enemy
    private void FireAtEnemy(bool firingActive)
    {
        var emissionModule = projectilePartile.emission;
        emissionModule.enabled = firingActive;
    }

}