using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour {

    [SerializeField]
    private float timeTilDestroy;


    private float timer;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        
        if (timer > timeTilDestroy)
        {
            Destroy(this.gameObject);
        }	
	}
}
