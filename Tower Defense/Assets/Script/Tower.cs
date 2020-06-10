using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("General:")]
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;

    [Header("Projectile info:")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject firingPosition;
    [SerializeField] float firingSpeed = 1f;
    [SerializeField] float projectileSpeed = 10f;
 
    // Update is called once per frame
    void Update()
    {
        LookAtEnemy();
        FireAtEnemy();
    }

    // Rotate towards enemy
    private void LookAtEnemy()
    {
        objectToPan.LookAt(targetEnemy);
    }

    // Fires at the Enemy
    private void FireAtEnemy()
    {
        if (targetEnemy == true)
        {
            StartCoroutine(FiringRoutine());
        }
    }

    private IEnumerator FiringRoutine()
    {
        GameObject currentProj = Instantiate(projectile, firingPosition.transform.position, Quaternion.identity);
        currentProj.transform.LookAt(targetEnemy);
        currentProj.transform.forward = Vector3.MoveTowards(firingPosition.transform.position, targetEnemy.transform.position, projectileSpeed);
        yield return new WaitForSeconds(firingSpeed);
    }
}