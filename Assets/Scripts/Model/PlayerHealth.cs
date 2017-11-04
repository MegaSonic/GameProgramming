using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int currentHealth;
    public int maxHealth;

    public int damageOnPassedEnemy;

    public Transform lossText;

	// Use this for initialization
	void Awake () {
        EventManager.OnEnemyDespawned += EnemyDespawned;
        EventManager.OnStartGameSave += SavePlayer;
        EventManager.OnLoadGameSave += LoadPlayer;
	}

    private void OnDestroy()
    {
        EventManager.OnEnemyDespawned -= EnemyDespawned;
        EventManager.OnStartGameSave -= SavePlayer;
        EventManager.OnLoadGameSave -= LoadPlayer;
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

    public void SetCurrentHealth(int newHealth)
    {
        if (newHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = newHealth;
        }
    }

    public void SavePlayer(ref Game game)
    {
        PlayerData player = game.playerData;

        player.position = transform.position;
        player.rotation = transform.rotation.eulerAngles;

        player.name = "";

        player.health = currentHealth;

        game.playerData = player;

    }

    public void LoadPlayer(Game game)
    {
        PlayerData player = game.playerData;

        transform.position = player.position;
        transform.rotation = Quaternion.Euler(player.rotation);
        SetCurrentHealth(game.playerData.health);
    }

}
