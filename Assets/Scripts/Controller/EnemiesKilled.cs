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

	// Use this for initialization
	void Start () {
        EventManager.OnEnemyKilled += IncrementEnemies;
        text.text = new StringBuilder(enemies.ToString()).ToString();
    }

    private void OnDisable()
    {
        EventManager.OnEnemyKilled -= IncrementEnemies;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void IncrementEnemies(string name, string source, Transform transform)
    {
        enemies++;
        text.text = new StringBuilder(enemies.ToString()).ToString();
    }
}
