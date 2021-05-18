using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public grumbleAMP gA;
    public GameObject dialogsObject;

    private static MySceneManager _instance;
    public GameObject mainGameCanvas;
    public static MySceneManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    private void Start()
    {
        _instance = this;
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(gA.transform.gameObject);
        DontDestroyOnLoad(dialogsObject);
        DontDestroyOnLoad(mainGameCanvas);
        OpenMenu();
    }

    public void OpenMenu()
    {
        gA.PlaySong(0, 0);
    }

    public void OpenMainGame()
    {
        SceneManager.LoadScene("Scenes/MainScene");
        gA.StopAll(0);
        mainGameCanvas.SetActive(true);
        mainGameCanvas.GetComponent<UIController>().EnableBlink();
        Dialogs.Instance.PlaySequnce(0);
    }

    public void OpenFishingGame()
    {
        StartCoroutine(OpenFishing());
    }

    private IEnumerator OpenFishing()
    {
        AsyncOperation async;
        async = SceneManager.LoadSceneAsync("Scenes/GoldMiner", LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }
        Debug.Log(async.isDone);
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.UnloadScene( activeScene.buildIndex );
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GoldMiner"));
        gA.CrossFadeToNewSong(2, 0, 1);
    }
    
    public void OpenSolderingGame()
    {
        StartCoroutine(OpenSoldering());
    }

    private IEnumerator OpenSoldering()
    {
        AsyncOperation async;
        async = SceneManager.LoadSceneAsync("Scenes/Soldering", LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }
        Debug.Log(async.isDone);
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.UnloadScene( activeScene.buildIndex );
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Soldering"));
        gA.CrossFadeToNewSong(2, 0, 1);
    }
}
