using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateOnLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (SaveLoad.Instance.selectedGameToLoad != null && SaveLoad.Instance.loadGame)
        {
            if (EventManager.OnLoadGameSave != null)
            {
                EventManager.OnLoadGameSave(SaveLoad.Instance.selectedGameToLoad);
            }
            SaveLoad.Instance.selectedGameToLoad = null;
            SaveLoad.Instance.loadGame = false;
        }

        StartCoroutine(WaitTilNextFrame());

		
	}

    public IEnumerator WaitTilNextFrame()
    {
        yield return new WaitForEndOfFrame();
        yield return null;

        
    }
}
