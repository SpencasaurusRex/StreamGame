using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkoffLength;
    public float walkingSpeed;

    bool walking;
    float timeSinceLastHit;
    UnitStats stats;
    EnemyController targetEnemy;

    void Awake ()
    {
        walking = true;
        stats = GetComponent<UnitStats>();
    }

    void Start()
    {
        TargetClosestEnemy();
    }

    void Update()
    {
        if (walking)
        {
            transform.position += new Vector3(walkingSpeed * Time.deltaTime, 0);

            if (targetEnemy == null)
            {
                return;
            }
            // Check if we're done
            else if (Vector3.Distance(transform.position, targetEnemy.transform.position) < stats.attackRange)
            {
                walking = false;
            }
        }
        else
        {
            if (timeSinceLastHit > stats.attackDelay)
            {
                timeSinceLastHit = 0;
                if (targetEnemy.TakeDamage(stats.attackStrength))
                {
                    // TODO: Base xp amount on enemy level
                    GetXp(50);
                    TargetClosestEnemy();
                }
            }
            else
            {
                timeSinceLastHit += Time.deltaTime;
            }
        }
    }

    public void StartRun()
    {
        stats.StartRun();
    }

    public void GetXp(int xp)
    {
        stats.xpUntilLevel -= xp;

        while (stats.xpUntilLevel <= 0)
        {
            stats.currentLevel++;
            stats.unspentSkillPoints++;
            stats.xpUntilLevel += stats.xpAcceleration * stats.currentLevel;
        }
    }

    public void TargetClosestEnemy()
    {
        float shortestDistance = float.MaxValue;
        EnemyController closestTarget = null;

        var enemies = FindObjectsOfType<EnemyController>();
        if (enemies.Length == 0)
        {
            // TODO: we killed everything
        }
        foreach (var enemy in enemies)
        {
            if (enemy.IsDead())
            {
                continue;
            }
            var newDistance = Vector3.Distance(enemy.transform.position, transform.position);
            if (newDistance < shortestDistance)
            {
                shortestDistance = newDistance;
                closestTarget = enemy;
            }
        }
        targetEnemy = closestTarget;
        walking = true;
    }

    public void TakeDamage(float damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            // TODO
        }
    }
}
