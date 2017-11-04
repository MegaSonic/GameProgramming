using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour {

    IHealth health;

    // Use this for initialization
    void Start () {
        
        health = transform.root.GetComponent<IHealth>();

        if (health == null)
        {
            Debug.LogWarning("No health component on " + transform.root.name + " and a health bar is trying to access it!");
        }
	}

    // Update is called once per frame
    void Update () {
        transform.localScale = new Vector3((float) health.GetCurrentHealth() / (float) health.GetMaxHealth(), 1f, 1f);
	}

    
}
