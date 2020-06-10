using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("General:")]
    [SerializeField] Transform catapult;
    [SerializeField] Transform targetEnemy;

    [SerializeField] float towerRange = 10f;
    [SerializeField] ParticleSystem projectilePartile;


    // Update is called once per frame
    void Update()
    {
        if (targetEnemy)
        {
            LookAtEnemy();
            ProcessFiring();
        }
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