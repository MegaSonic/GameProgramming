using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesKilled : MonoBehaviour {

    [SerializeField]
    private int enemies;

    [SerializeField]
    private Text text;

    private void Awake()
    {
        EventManager.OnEnemyKilled += IncrementEnemies;
        EventManager.OnStartGameSave += SaveEnemiesKilled;
        EventManager.OnLoadGameSave += LoadEnemiesKilled;
    }

    // Use this for initialization
    void Start () {
        

        text.text = new StringBuilder(enemies.ToString()).ToString();
    }

    private void OnDestroy()
    {
        EventManager.OnEnemyKilled -= IncrementEnemies;
        EventManager.OnStartGameSave -= SaveEnemiesKilled;
        EventManager.OnLoadGameSave -= LoadEnemiesKilled;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void IncrementEnemies(string name, string source, Transform transform)
    {
        enemies++;
        text.text = new StringBuilder(enemies.ToString()).ToString();
    }

    public void SaveEnemiesKilled(ref Game game)
    {
        PlayerData data = game.playerData;
        data.enemiesKilled = enemies;
        game.playerData = data;
    }

    public void LoadEnemiesKilled(Game game)
    {
        enemies = game.playerData.enemiesKilled;
        text.text = new StringBuilder(enemies.ToString()).ToString();
        Debug.Log("Enemies killed!");
    }
}
