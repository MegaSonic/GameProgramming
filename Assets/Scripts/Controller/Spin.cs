using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float spinForce;
    public Rigidbody rigid;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space))
        {

            rigid.inertiaTensorRotation = Quaternion.identity;
            rigid.AddRelativeTorque(0f, spinForce, 0f, ForceMode.Acceleration);
            // transform.Rotate(new Vector3(spinForce * Time.deltaTime, 0f, 0f));
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x + spinForce * Time.deltaTime, transform.eulerAngles.y, transform.eulerAngles.z);
        }
	}
}
