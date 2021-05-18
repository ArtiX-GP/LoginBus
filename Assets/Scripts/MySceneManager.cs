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
        OpenMenu();
    }

    public void OpenMenu()
    {
        gA.PlaySong(0, 0);
    }

    public void OpenMainGame()
    {
        SceneManager.LoadScene("Scenes/MainScene");
        gA.StopAll(1);
        Dialogs.Instance.PlaySequnce(0);
    }
}
