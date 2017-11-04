using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth {

    public int currentHealth;
    public int maxHealth;
    public GameObject deathVFX;

    [SerializeField]
    public int basePhysicalDefense;

    [SerializeField]
    public int bonusPhysicalDefense;

    [SerializeField]
    public int baseMagicalDefense;

    [SerializeField]
    public int bonusMagicalDefense;

    [SerializeField]
    [Range(0, 100)]
    public int baseDodgeChance;

    [SerializeField]
    public int bonusDodgeChance;

    [SerializeField]
    public int enemyType;

    public void Damage(GameObject source, DamageType type, int amount)
    {
        if (CheckForDodge())
        {
            if (EventManager.OnEnemyDodged != null)
            {
                EventManager.OnEnemyDodged(transform.name, transform);
            }
        }
        else
        {

            int newDamage = AccountForDamageResistance(type, amount);

            if (EventManager.OnEnemyDamaged != null)
            {
                EventManager.OnEnemyDamaged(transform.name, source.name, transform, type, newDamage);
            }

            currentHealth -= newDamage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die(source);
            }
        }
    }

    public bool CheckForDodge()
    {
        if (UnityEngine.Random.Range(0, 100) < baseDodgeChance + bonusDodgeChance)
        {
            return true;
        }

        return false;
    }

    public int AccountForDamageResistance(DamageType type, int amount)
    {
        int newAmount = amount;

        switch (type)
        {
            case DamageType.PHYSICAL:
                newAmount -= (basePhysicalDefense + bonusPhysicalDefense);
                break;
            case DamageType.MAGICAL:
                newAmount -= (baseMagicalDefense + bonusMagicalDefense);
                break;
        }

        return newAmount;
    }

    public void Die(GameObject source)
    {
        if (EventManager.OnEnemyKilled != null)
        {
            EventManager.OnEnemyKilled(transform.name, source.name, transform);
        }

        if (deathVFX != null)
        {
            Instantiate(deathVFX, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject);
    }

    public void Heal(int amount)
    {
        if (currentHealth < maxHealth && amount > 0)
        {
            if (EventManager.OnEnemyHealed != null)
            {
                EventManager.OnEnemyHealed(transform.name, transform, amount);
            }
            currentHealth += amount;
            if (currentHealth > maxHealth) currentHealth = maxHealth;

        }
    }

    public void SetHealth(int newHealth)
    {
        currentHealth = newHealth;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Die(gameObject);
        }
        else if (currentHealth > maxHealth) currentHealth = maxHealth;
    }


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
