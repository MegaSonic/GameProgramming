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

    [SerializeField]
    private List<GameObject> spawnedEnemies;

    private float spawnTimer;

    private void Awake()
    {
        EventManager.OnStartGameSave += SaveEnemies;
        EventManager.OnEnemyKilled += ClearListOnKill;
        EventManager.OnEnemyDespawned += ClearListOnDespawn;
        EventManager.OnLoadGameSave += LoadEnemies;
    }

    // Use this for initialization
    void Start () {
        

        spawnTimer = baseTimeBetweenSpawns + Random.Range(-spawnTimeRange, spawnTimeRange);
        spawnedEnemies = new List<GameObject>();
	}

    private void OnDestroy()
    {
        EventManager.OnStartGameSave -= SaveEnemies;
        EventManager.OnEnemyKilled -= ClearListOnKill;
        EventManager.OnEnemyDespawned -= ClearListOnDespawn;
        EventManager.OnLoadGameSave -= LoadEnemies;
    }

    // Update is called once per frame
    void Update () {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            GameObject chosenEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            int randomPath = Random.Range(0, paths.Count);
            EnemyPath chosenPath = paths[randomPath];

            chosenEnemy.GetComponent<TravelPath>().pathToFollow = chosenPath.nodes;
            chosenEnemy.GetComponent<TravelPath>().pathGO = chosenPath.gameObject;
            chosenEnemy.GetComponent<TravelPath>().pathNum = randomPath;

            GameObject instantiatedEnemy = Instantiate(chosenEnemy);
            spawnedEnemies.Add(instantiatedEnemy);

            instantiatedEnemy.transform.position = chosenPath.nodes[0].transform.position;

            spawnTimer = baseTimeBetweenSpawns + Random.Range(-spawnTimeRange, spawnTimeRange);

            if (EventManager.OnEnemySpawned != null)
            {
                EventManager.OnEnemySpawned(instantiatedEnemy.name, instantiatedEnemy.transform, chosenPath.name);
            }
        }
	}

    public void SaveEnemies(ref Game game)
    {
        foreach (GameObject go in spawnedEnemies)
        {
            if (go != null)
            {

                EnemyData enemyData = new EnemyData();

                TravelPath path = go.GetComponent<TravelPath>();
                Enemy enemy = go.GetComponent<Enemy>();

                enemyData.enemyType = enemy.enemyType;
                enemyData.health = enemy.GetCurrentHealth();
                enemyData.pathNumber = path.pathNum;
                enemyData.headingToNode = path.i;
                enemyData.position = path.transform.position;
                enemyData.rotation = path.transform.rotation.eulerAngles;

                game.enemyData.Add(enemyData);
            }
        }
    }

    public void LoadEnemies(Game game)
    {
        foreach (EnemyData enemyData in game.enemyData)
        {
            GameObject go;

            if (enemyData.enemyType > enemyPrefabs.Count)
            {
                Debug.LogWarning("Trying to load an enemy type that doesn't exist!");
                go = Instantiate(enemyPrefabs[0]);
            }
            else
            {
                go = Instantiate(enemyPrefabs[enemyData.enemyType]);
            }

            TravelPath path = go.GetComponent<TravelPath>();
            Enemy enemy = go.GetComponent<Enemy>();

            enemy.SetHealth(enemyData.health);
            enemy.transform.position = enemyData.position;
            enemy.transform.rotation = Quaternion.Euler(enemyData.rotation);

            path.pathNum = enemyData.pathNumber;
            path.i = enemyData.headingToNode;

            path.pathGO = paths[path.pathNum].gameObject;
            path.pathToFollow = paths[path.pathNum].nodes;

            spawnedEnemies.Add(go);
        }

        Debug.Log("Spawned enemies!");
    }

    public void ClearListOnKill(string name, string source, Transform transform)
    {
        spawnedEnemies.RemoveAll(GameObject => GameObject == null);
    }

    public void ClearListOnDespawn(string name, Transform transform)
    {
        spawnedEnemies.RemoveAll(GameObject => GameObject == null);
    }
}
