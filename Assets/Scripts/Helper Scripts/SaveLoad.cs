using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class SaveLoad : Singleton<SaveLoad> {

    public List<Game> savedGames = new List<Game>();

    public Game selectedGameToLoad;
    public Game currentGame;

    public bool loadGame;

    public Game cloudSave;

    public void Save(Game gameToSave)
    {
        savedGames.Add(gameToSave);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gam");
        bf.Serialize(file, savedGames);
        file.Close();
    }

    public void Save()
    {
        Save(currentGame);
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gam"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gam", FileMode.Open);
            savedGames = (List<Game>)bf.Deserialize(file);
            file.Close();
        }
    }

    public void SetGameToLoad(Game game)
    {
        selectedGameToLoad = game;
        loadGame = true;
    }

}
