using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelToLoad : Singleton<LevelToLoad> {

    [SerializeField]
    private int levelNumberToLoad;


    public void AssignLevelNumber(int value)
    {
        levelNumberToLoad = value;
    }


    public int GetLevelNumber()
    {
        return levelNumberToLoad;
    }
}
