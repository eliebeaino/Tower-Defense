using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats:")]
    [SerializeField] int HitPoints = 10;
    [SerializeField] float enemyDamage = 1f;
    [SerializeField] [Tooltip("timer for enemy body to despawn in seconds")] float enemyDespawn = 3f;

    [Header("Assets:")]
    [SerializeField] Animator animator;
    [SerializeField] EnemyMovement enemyMovement;

    // check projectile collision with enemy
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (HitPoints<= 0)
        {
            KillEnemy();
        }
    }

    // calculate new health after taking damage
    private void ProcessHit()
    {
        HitPoints = HitPoints - 1;
    }    

    // destory enemy when reaching 0 health - makes it untargetable - start death animation - stops it from moving by changing bool in enemymovement.cs
    private void KillEnemy()
    {
        animator.SetBool("Dead", true);                             // start animation
        Destroy(gameObject, enemyDespawn);                          // despawn enemy after few secs
        Destroy(this);                                              // removes this componenet so the tower doesnt target it anymore
        Destroy(enemyMovement);
    }

    // when reached the player base, start dealing damage and animate the enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            animator.SetBool("DealDamage", true);
            Destroy(enemyMovement);
        }
    }

    // gets called from animation to do the damage on the player base
    public void damagePlayerBase()
    {
        FindObjectOfType<PlayerBase>().ProcessDmg(enemyDamage);
    }
}
