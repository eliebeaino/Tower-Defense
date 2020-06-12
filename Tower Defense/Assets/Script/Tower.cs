using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Transform catapult;                    // object to pan (using lookat)
    [SerializeField] Animator catapultAnimator;
    [SerializeField] float towerRange = 10f;

    [Header("Assets to store")]
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] AudioClip FireSFX;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float soundTimer = 2f;
    float initialTimer;
    bool soundplayed = false;

    [Header("Assets stored while game runs")]
    public Waypoint baseWaypoint;                           // current location of tower 
    public Transform targetEnemy;

    private void Start()
    {
        initialTimer = soundTimer; // setting up intial timer to reset the timer in catapultSFX
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            LookAtEnemy();
            ProcessFiring();
        }
        else
        {
            FireAtEnemy(false);
        }
    }

    // defining the target enemy, if there is none exit by emptying the target to null
    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<Enemy>();

        // it theres no enmies in game, return null to avoid stuck animation and projectiles
        // todochange later when projectile is instantiated
        if (sceneEnemies.Length == 0)
        {
            targetEnemy = null; return;
        }
        Transform closestEnemy = sceneEnemies[0].transform;            // setting the first enemy found as the closest enemy in array
        
        foreach (Enemy testEnemy in sceneEnemies)
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
        if (distanceToEnemy <= towerRange)
        {
            FireAtEnemy(true);
            catapultAnimator.SetBool("Firing", true);
            catapultSFX();
        }
        else
        {
            FireAtEnemy(false);
        }
    }

    // Fires at the Enemy or Stops
    private void FireAtEnemy(bool firingActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = firingActive;
        catapultAnimator.SetBool("Firing", false);
    }

    // gets called from animator to run sound for catapult firing
    public void catapultSFX()
    {
        if (!soundplayed)
        {
            audioSource.PlayOneShot(FireSFX);
            soundplayed = true;    
        }
        else
        {
            soundTimer -= Time.deltaTime;
            if (soundTimer < 0)
            {
                soundTimer = initialTimer;
                soundplayed = false;
            }
        }
    }
}