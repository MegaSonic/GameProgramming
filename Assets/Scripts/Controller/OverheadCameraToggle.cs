using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OverheadCameraToggle : MonoBehaviour {

    public CinemachineVirtualCamera fpsCamera;
    public CinemachineVirtualCamera overheadCamera;
    public CinemachineVirtualCamera thirdPersonCamera;
    public Camera gunCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C))
        {
            if (!overheadCamera.enabled)
            {
                overheadCamera.enabled = true;
                thirdPersonCamera.enabled = false;
                fpsCamera.enabled = false;
                gunCamera.enabled = false;
            }
            else
            {
                overheadCamera.enabled = false;
                thirdPersonCamera.enabled = false;
                fpsCamera.enabled = true;
                gunCamera.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!thirdPersonCamera.enabled)
            {
                overheadCamera.enabled = false;
                thirdPersonCamera.enabled = true;
                fpsCamera.enabled = false;
                gunCamera.enabled = false;
            }
            else
            {
                overheadCamera.enabled = false;
                thirdPersonCamera.enabled = false;
                fpsCamera.enabled = true;
                gunCamera.enabled = true;
            }
        }
	}
}
