using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RegenHealth : MonoBehaviour {

    [SerializeField]
    private float healthPerSecond;

    private IHealth health;

    private int decimalTracker;
    private float timer;


	// Use this for initialization
	void Start () {
        health = GetComponent<IHealth>();

        if (health == null)
        {
            Debug.LogError("No health component found for health regen!");
        }
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer -= 1f;

            int first2DecimalPlaces = (int)(((decimal)healthPerSecond % 1) * 100);
            decimalTracker += first2DecimalPlaces;

            if (decimalTracker >= 100)
            {
                decimalTracker -= 100;
                health.Heal(Mathf.FloorToInt(healthPerSecond) + 1);
            }
            else
            {
                health.Heal(Mathf.FloorToInt(healthPerSecond));
            }

        }
	}
}
