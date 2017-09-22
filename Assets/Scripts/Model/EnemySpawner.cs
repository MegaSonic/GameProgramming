using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private List<GameObject> enemyPrefabs;

    [SerializeField]
    private float baseTimeBetweenSpawns;

    [SerializeField]
    private float spawnTimeRange;

    [SerializeField]
    private List<EnemyPath> paths;

    private float spawnTimer;

	// Use this for initialization
	void Start () {
        spawnTimer = baseTimeBetweenSpawns + Random.Range(-spawnTimeRange, spawnTimeRange);
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            GameObject chosenEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            EnemyPath chosenPath = paths[Random.Range(0, paths.Count)];

            chosenEnemy.GetComponent<TravelPath>().pathToFollow = chosenPath.nodes;
            chosenEnemy.GetComponent<TravelPath>().pathGO = chosenPath.gameObject;

            GameObject instantiatedEnemy = Instantiate(chosenEnemy);
            instantiatedEnemy.transform.position = chosenPath.nodes[0].transform.position;

            spawnTimer = baseTimeBetweenSpawns + Random.Range(-spawnTimeRange, spawnTimeRange);

            if (EventManager.OnEnemySpawned != null)
            {
                EventManager.OnEnemySpawned(instantiatedEnemy.name, instantiatedEnemy.transform, chosenPath.name);
            }
        }
	}
}
