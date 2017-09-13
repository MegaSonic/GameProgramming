using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth {


    int GetCurrentHealth();

    int GetMaxHealth();

    void SetHealth(int newHealth);

    void Damage(GameObject damageSource, DamageType type, int amount);

    void Heal(int amount);

    void Die(GameObject source);
}
