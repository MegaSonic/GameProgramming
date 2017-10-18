using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainMenu : MonoBehaviour {

    public float timeToLoad;
    public bool loadingLevel;

    public Transform mainMenuCanvas;

    [SerializeField]
    private GameObject newGameButton;

    [SerializeField]
    private GameObject quitGameButton;

    private float loadTimer;
    private AsyncOperation asyncLoad;

    [SerializeField]
    private Animator animator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	

    public void ClickStartButton()
    {
        animator.Play("startGame");
        LevelToLoad.Instance.AssignLevelNumber(1);
        newGameButton.SetActive(false);
        quitGameButton.SetActive(false);
    }

    public void TransititionToLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void ClickExitButton()
    {
        Application.Quit();
    }

    
}
