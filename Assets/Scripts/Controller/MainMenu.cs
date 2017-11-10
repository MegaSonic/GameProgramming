using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;

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

    [SerializeField]
    private InputField usernameInput;

    [SerializeField]
    private InputField passwordInput;

    [SerializeField]
    private Dropdown saveGameList;

    [SerializeField]
    private Text cloudSaveLabel;

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

    public void ClickRegisterButton()
    {
        StartCoroutine(RegisterOnServer());
    }

    public void ClickHelloButton()
    {
        StartCoroutine(HelloOnServer());
    }

    IEnumerator HelloOnServer()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://shootergame.getsandbox.com/hello");

        yield return www.Send();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    IEnumerator RegisterOnServer()
    {
        UserInfo info = new UserInfo { username = usernameInput.text, password = passwordInput.text };

        string postData = JsonUtility.ToJson(info);
        Debug.Log(postData);
        byte[] bytes = Encoding.UTF8.GetBytes(postData);

        var request = new UnityWebRequest("http://shootergame.getsandbox.com/users", "POST");
        
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);

        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    IEnumerator CheckUserExists() {
        UserInfo info = new UserInfo { username = usernameInput.text, password = passwordInput.text };

        string postData = JsonUtility.ToJson(info);
        Debug.Log(postData);
        byte[] bytes = Encoding.UTF8.GetBytes(postData);

        var request = new UnityWebRequest("http://shootergame.getsandbox.com/users", "GET");

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("User check successful!");
        }
    }

    public void ClickExitButton()
    {
        Application.Quit();
    }

    public void ClickSendSavesButton()
    {
        StartCoroutine(SendSaveToServer());
    }

    public void ClickGetSavesButton()
    {
        StartCoroutine(GetSavesFromServer());
    }

    public void ClickLoadCloudSaveButton()
    {
        if (SaveLoad.Instance.cloudSave != null)
        {
            SaveLoad.Instance.SetGameToLoad(SaveLoad.Instance.cloudSave);
            ClickStartButton();
        }
    }

    IEnumerator GetSavesFromServer()
    {
        if (usernameInput.text.Length < 1 || passwordInput.text.Length < 1)
        {
            Debug.Log("Invalid username or password!");
            yield break;
        }

        UserInfo info = new UserInfo { username = usernameInput.text, password = passwordInput.text };

        string postData = JsonUtility.ToJson(info);
        Debug.Log(postData);
        byte[] bytes = Encoding.UTF8.GetBytes(postData);

        var request = new UnityWebRequest("http://shootergame.getsandbox.com/users", "GET");

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();

        bool userSuccess;
        if (request.isNetworkError || request.isHttpError)
        {
            userSuccess = false;
            Debug.Log(request.error);
        }
        else
        {
            userSuccess = true;
            Debug.Log("User check successful!");
        }

        if (!userSuccess) yield break;

        var saveRequest = new UnityWebRequest("http://shootergame.getsandbox.com/saves/" + usernameInput.text, "GET");

        saveRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        saveRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        saveRequest.SetRequestHeader("Content-Type", "application/json");

        yield return saveRequest.Send();

        byte[] saveResults;

        if (saveRequest.isNetworkError || saveRequest.isHttpError)
        {
            Debug.Log(saveRequest.error);
            yield break;
        }
        else
        {
            Debug.Log(saveRequest.downloadHandler.text);
            saveResults = saveRequest.downloadHandler.data;
        }

        string saveJson = saveRequest.downloadHandler.text;
        Game cloudSave = JsonUtility.FromJson<Game>(saveJson);
        SaveLoad.Instance.cloudSave = cloudSave;

        cloudSaveLabel.text = cloudSave.dateTime;
    }

    IEnumerator SendSaveToServer()
    {
        if (usernameInput.text.Length < 1 || passwordInput.text.Length < 1)
        {
            Debug.Log("Invalid username or password!");
            yield break;
        }

        UserInfo info = new UserInfo { username = usernameInput.text, password = passwordInput.text };

        string postData = JsonUtility.ToJson(info);
        Debug.Log(postData);
        byte[] bytes = Encoding.UTF8.GetBytes(postData);

        var request = new UnityWebRequest("http://shootergame.getsandbox.com/users", "GET");

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();

        bool userSuccess;
        if (request.isNetworkError || request.isHttpError)
        {
            userSuccess = false;
            Debug.Log(request.error);
        }
        else
        {
            userSuccess = true;
            Debug.Log("User check successful!");
        }

        if (!userSuccess) yield break;

        Game gameToCloudSave = SaveLoad.Instance.savedGames[saveGameList.value];
        
        string saveData = JsonUtility.ToJson(gameToCloudSave);
        Debug.Log(saveData);
        byte[] saveBytes = Encoding.UTF8.GetBytes(saveData);

        var saveRequest = new UnityWebRequest("http://shootergame.getsandbox.com/saves/" + usernameInput.text, "PUT");

        saveRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(saveBytes);
        saveRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        saveRequest.SetRequestHeader("Content-Type", "application/json");

        yield return saveRequest.Send();

        if (saveRequest.isNetworkError || saveRequest.isHttpError)
        {
            Debug.Log(saveRequest.error);

        }
        else
        {
            Debug.Log("Save upload complete!");
        }
    }
    
}

[System.Serializable]
public class UserInfo
{
    public string username;
    public string password;
}
