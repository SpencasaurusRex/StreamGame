using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public float startingAttackDelay;
    public float startingAttackStrength;

    public float attackDelay;
    public float attackStrength;
    public float health;
    public float attackRange;

    public int xpAcceleration;
    public int xpUntilLevel;
    public int currentLevel;

    public int unspentSkillPoints;

    public AnimationCurve strengthMultiplierCurve;

    // Increase damage
    public float strength = 1;
    // Health points
    public float constitution = 1;
    // Increase accuracy, Increase dodge chance
    // Decrease weapon delay, 
    public float dexterity = 1;

    private void Awake()
    {
        xpUntilLevel = xpAcceleration;
        currentLevel = 1;
    }

    public void StartRun()
    {
        attackStrength = startingAttackStrength * strengthMultiplierCurve.Evaluate(strength);
        health = constitution;

        var dexterityMultiplier = .2f * (-Mathf.Log(dexterity + 2) + Mathf.Log(2)) + 1;
        attackDelay = startingAttackDelay * dexterityMultiplier;
    }
}
