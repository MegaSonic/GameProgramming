using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public Game game;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        LevelToLoad.Instance.AssignLevelNumber(0);
        SceneManager.LoadScene(2);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void SaveGame()
    {
        game = new Game();

        if (EventManager.OnStartGameSave != null)
        {
            EventManager.OnStartGameSave(ref game);
        }

        game.dateTime = System.DateTime.Now.ToLongTimeString();

        SaveLoad.Instance.Save(game);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
