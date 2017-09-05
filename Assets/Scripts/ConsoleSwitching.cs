using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleSwitching : MonoBehaviour {

    [SerializeField]
    private Canvas minimizedCanvas;

    [SerializeField]
    private Canvas maximizedCanvas;

	// Use this for initialization
	void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        minimizedCanvas.gameObject.SetActive(true);
        maximizedCanvas.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            SwitchCanvas();
        }
	}

    public void SwitchCanvas()
    {
        if (minimizedCanvas.gameObject.activeSelf)
        {
            minimizedCanvas.gameObject.SetActive(false);
            maximizedCanvas.gameObject.SetActive(true);
        }
        else
        {
            minimizedCanvas.gameObject.SetActive(true);
            maximizedCanvas.gameObject.SetActive(false);
        }
    }
}
