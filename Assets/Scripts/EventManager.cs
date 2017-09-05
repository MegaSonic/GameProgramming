using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public static EventManager Instance = null;


    public delegate void PlayerFired(Transform playerPosition);
    public static PlayerFired OnPlayerFired;

    public delegate void EnemyReachedNode(string name, Transform transform, string path, int node);
    public static EnemyReachedNode OnEnemyReachedNode;

    public delegate void EnemySpawned(string name, Transform transform, string path);
    public static EnemySpawned OnEnemySpawned;

    public delegate void EnemyDestroyed(string name, Transform transform);
    public static EnemyDestroyed OnEnemyDestroyed;

    void Awake()
    {

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
