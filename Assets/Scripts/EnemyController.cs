using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTransform;
    UnitStats stats;
    float timeSinceLastHit;
    bool dead;

    private void Awake()
    {
        stats = GetComponent<UnitStats>();
    }

    void Update ()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) <= stats.attackRange)
        {
            if (timeSinceLastHit >= stats.attackDelay)
            {
                // Hit the player
                timeSinceLastHit = 0;
            }
            else
            {
                timeSinceLastHit += Time.deltaTime;
            }
        }
    }
    
    public bool TakeDamage(float damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            dead = true;
            // TODO: Drop gold

            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public bool IsDead()
    {
        return dead;
    }
}
