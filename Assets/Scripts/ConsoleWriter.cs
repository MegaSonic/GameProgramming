using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Linq;

public class ConsoleWriter : MonoBehaviour {

    [SerializeField]
    private Text text;

    [SerializeField]
    private Scrollbar scrollbar;

    [SerializeField]
    private int maxLines;

    private int currentLines;

    // Use this for initialization
    void Start () {
        text.text = "";
        Debug.Log("text cleared");
	}

    private void OnDisable()
    {
        EventManager.OnPlayerFired -= WriteOnPlayerFired;
        EventManager.OnEnemyReachedNode -= WriteOnEnemyReachNode;
        EventManager.OnEnemySpawned -= WriteOnEnemySpawned;
        EventManager.OnEnemyDestroyed -= WriteOnEnemyDestroyed;
    }

    private void OnEnable()
    {
        EventManager.OnPlayerFired += WriteOnPlayerFired;
        EventManager.OnEnemyReachedNode += WriteOnEnemyReachNode;
        EventManager.OnEnemySpawned += WriteOnEnemySpawned;
        EventManager.OnEnemyDestroyed += WriteOnEnemyDestroyed;
    }

    public void CheckForMaxLines()
    {
        if (currentLines > maxLines)
        {
            var lines = Regex.Split(text.text, "\r\n|\r|\n").Skip(1);
            text.text = string.Join(Environment.NewLine, lines.ToArray());
        }
    }

    public void WriteOnPlayerFired(Transform playerPosition)
    {
        text.text += ("\nPlayer fired. Location: " + playerPosition.position);
        currentLines++;
        CheckForMaxLines();
    }

    public void WriteOnEnemyReachNode(string name, Transform transform, string path, int node)
    {
        text.text += string.Format("\nEnemy {0} reached node {1} on path {2}.", name, node, path);
        currentLines++;
        CheckForMaxLines();
    }

    public void WriteOnEnemySpawned(string name, Transform transform, string path)
    {
        text.text += string.Format("\nEnemy {0} spawned on path {1}.", name, path);
        currentLines++;
        CheckForMaxLines();
    }

    public void WriteOnEnemyDestroyed(string name, Transform transform)
    {
        text.text += string.Format("\nEnemy {0} destroyed.", name);
        currentLines++;
        CheckForMaxLines();
    }

    public void SetScrollBarToBottom(Vector2 vector)
    {
        scrollbar.value = 0f;
    }
}
