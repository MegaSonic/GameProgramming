using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameList : MonoBehaviour {

    public Dropdown dropdown;
    public Button loadGameButton;
    public MainMenu mainMenu;

	// Use this for initialization
	void Start () {
        SaveLoad.Instance.Load();
        dropdown.options.Clear();

        if (SaveLoad.Instance.savedGames.Count > 0)
        {
            foreach (Game game in SaveLoad.Instance.savedGames)
            {
                dropdown.options.Add(new Dropdown.OptionData(game.dateTime));
            }
        }
        else
        {
            dropdown.interactable = false;
            loadGameButton.interactable = false;
        }
	}

    public void LoadGame()
    {
        int selectedGame = dropdown.value;
        SaveLoad.Instance.SetGameToLoad(SaveLoad.Instance.savedGames[selectedGame]);
        mainMenu.ClickStartButton();
    }
	
}
