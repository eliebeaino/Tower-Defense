using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] int HitPoints = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }


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

    // destory enemy when reaching 0 health
    private void KillEnemy()
    {
        Destroy(gameObject);
    }
}
