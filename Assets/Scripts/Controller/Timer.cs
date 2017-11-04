using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float timer;

    public Text text;
    public StringBuilder stringBuilder;

    private void Awake()
    {
        EventManager.OnStartGameSave += SaveTime;
        EventManager.OnLoadGameSave += LoadTime;
    }

    private void OnDestroy()
    {
        EventManager.OnStartGameSave -= SaveTime;
        EventManager.OnLoadGameSave -= LoadTime;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        int seconds = Mathf.FloorToInt(timer) % 60;
        int minutes = seconds / 60;

        StringBuilder minutesString = new StringBuilder(minutes.ToString());
        StringBuilder secondsString = new StringBuilder("00");

        if (seconds == 0)
        {
            secondsString = new StringBuilder("00");
        }
        else if (seconds < 10)
        {
            secondsString = new StringBuilder("0" + seconds.ToString());
        }
        else
        {
            secondsString = new StringBuilder(seconds.ToString());
        }

        stringBuilder = new StringBuilder(minutesString.ToString() + ":" + secondsString.ToString());

        text.text = stringBuilder.ToString();
	}

    public void SaveTime(ref Game game)
    {
        game.playerData.timeInLevel = timer;
    }

    public void LoadTime(Game game)
    {
        timer = game.playerData.timeInLevel;
        Debug.Log("Loaded time!");

        int seconds = Mathf.FloorToInt(timer) % 60;
        int minutes = seconds / 60;

        StringBuilder minutesString = new StringBuilder(minutes.ToString());
        StringBuilder secondsString = new StringBuilder("00");

        if (seconds == 0)
        {
            secondsString = new StringBuilder("00");
        }
        else if (seconds < 10)
        {
            secondsString = new StringBuilder("0" + seconds.ToString());
        }
        else
        {
            secondsString = new StringBuilder(seconds.ToString());
        }

        stringBuilder = new StringBuilder(minutesString.ToString() + ":" + secondsString.ToString());

        text.text = stringBuilder.ToString();
    }
}
