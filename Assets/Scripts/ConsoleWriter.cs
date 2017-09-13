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
        EventManager.OnPlayerFiredGun -= WriteOnPlayerFired;
        EventManager.OnEnemyReachedNode -= WriteOnEnemyReachNode;
        EventManager.OnEnemySpawned -= WriteOnEnemySpawned;
        EventManager.OnEnemyDespawned -= WriteOnEnemyDestroyed;
        EventManager.OnEnemyKilled -= WriteOnEnemyKilled;
        EventManager.OnDamagedSomething -= WriteOnDamagedSomething;
        EventManager.OnEnemyDamaged -= WriteOnEnemyDamaged;
        EventManager.OnEnemyHealed -= WriteOnEnemyHealed;
        EventManager.OnEnemyDodged -= WriteOnEnemyDodged;
    }

    private void OnEnable()
    {
        EventManager.OnPlayerFiredGun += WriteOnPlayerFired;
        EventManager.OnEnemyReachedNode += WriteOnEnemyReachNode;
        EventManager.OnEnemySpawned += WriteOnEnemySpawned;
        EventManager.OnEnemyDespawned += WriteOnEnemyDestroyed;
        EventManager.OnEnemyKilled += WriteOnEnemyKilled;
        EventManager.OnDamagedSomething += WriteOnDamagedSomething;
        EventManager.OnEnemyDamaged += WriteOnEnemyDamaged;
        EventManager.OnEnemyHealed += WriteOnEnemyHealed;
        EventManager.OnEnemyDodged += WriteOnEnemyDodged;
    }

    public void CheckForMaxLines()
    {
        if (currentLines > maxLines)
        {
            var lines = Regex.Split(text.text, "\r\n|\r|\n").Skip(1);
            text.text = string.Join(Environment.NewLine, lines.ToArray());
        }
    }

    public void WriteOnEnemyKilled(string name, string source, Transform transform)
    {
        text.text += String.Format("\nEnemy {0} killed at {1} by source {2}.", name, transform.position, source);
        currentLines++;
        CheckForMaxLines();
    }

    public void WriteOnDamagedSomething(string damageDealer, string damageReceiver, DamageType type, int amount)
    {
        text.text += String.Format("\nDamage dealer {0} hit {1} for {2} points of {3} damage before resistance.", damageDealer, damageReceiver, amount, type.ToString());
        currentLines++;
        CheckForMaxLines();
    }

    public void WriteOnEnemyDamaged(string name, string source, Transform transform, DamageType type, int amount)
    {
        text.text += String.Format("\nEnemy {0} hit for {1} points of {2} damage, originating from {3}.", name, amount, type.ToString(), source);
        currentLines++;
        CheckForMaxLines();
    }

    public void WriteOnEnemyHealed(string name, Transform transform, int amount)
    {
        text.text += String.Format("\nEnemy {0} healed {1} hp.", name, amount);
        currentLines++;
        CheckForMaxLines();
    }

    public void WriteOnEnemyDodged(string name, Transform transform)
    {
        text.text += String.Format("\nEnemy {0} dodged an attack.", name);
        currentLines++;
        CheckForMaxLines();
    }

    public void WriteOnPlayerFired(Transform playerPosition, string name)
    {
        text.text += ("\nPlayer fired " + name + " gun. Location: " + playerPosition.position);
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
        text.text += string.Format("\nEnemy {0} despawned.", name);
        currentLines++;
        CheckForMaxLines();
    }

    public void SetScrollBarToBottom(Vector2 vector)
    {
        scrollbar.value = 0f;
    }
}
