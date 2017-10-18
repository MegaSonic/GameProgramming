using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour {

    public Scrollbar scrollbar;

    public int levelToLoad;
    private AsyncOperation asyncLoad;
    public float timeToLoad;
    private float loadTimer;

    private void Start()
    {
        levelToLoad = LevelToLoad.Instance.GetLevelNumber();
        loadTimer = timeToLoad;
        StartCoroutine(LoadLevelAsync());
    }

    void Update()
    {
        scrollbar.size = GetLoadProgress();

        loadTimer -= Time.deltaTime;

        if (loadTimer < 0f)
        {
            asyncLoad.allowSceneActivation = true;
        }
    }

    public void LoadLevel()
    {

    }

    public float GetLoadProgress()
    {
        if (timeToLoad == 0f) return 0f;

        float result = (timeToLoad - loadTimer) / timeToLoad;
        if (result > 1f) result = 1f;

        return result;
    }

    IEnumerator LoadLevelAsync()
    {
        asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
