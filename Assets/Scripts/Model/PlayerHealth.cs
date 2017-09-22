using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int currentHealth;
    public int maxHealth;

    public int damageOnPassedEnemy;

    public Transform lossText;

	// Use this for initialization
	void Start () {
        EventManager.OnEnemyDespawned += EnemyDespawned;
	}

    private void OnDestroy()
    {
        EventManager.OnEnemyDespawned -= EnemyDespawned;
    }

    public void EnemyDespawned(string name, Transform transform)
    {
        currentHealth -= damageOnPassedEnemy;

        if (currentHealth < 0)
        {
            currentHealth = 0;
            lossText.gameObject.SetActive(true);
        }

    }
}
