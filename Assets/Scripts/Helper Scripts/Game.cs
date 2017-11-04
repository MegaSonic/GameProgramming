using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game {

    public List<EnemyData> enemyData;
    public PlayerData playerData;
    public string dateTime;

    public Game()
    {
        enemyData = new List<EnemyData>();
        playerData = new PlayerData();
        dateTime = "";
    }
}

[System.Serializable]
public class EnemyData
{
    public SerializableVector3 position;
    public SerializableVector3 rotation;

    public int enemyType;

    public int health;
    public int pathNumber;
    public int headingToNode;

    public EnemyData()
    {
        position = Vector3.zero;
        rotation = Vector3.zero;
    }
}

[System.Serializable]
public class PlayerData
{
    public int levelScore;
    public int highScore;
    public int sumScore;

    public int health;

    public SerializableVector3 position;
    public SerializableVector3 rotation;

    public float timeInLevel;
    public int enemiesKilled;

    public string name;

    public PlayerData()
    {
        position = Vector3.zero;
        rotation = Vector3.zero;

        name = "";
    }
}