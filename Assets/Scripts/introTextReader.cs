using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class introTextReader : MonoBehaviour
{
    public string[] introText;

    int curTextId;

    public GameObject textObj;

    void Start()
    {
        curTextId = 0;
        textObj.GetComponent<Text>().text = introText[curTextId];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            curTextId++;
            Debug.Log(curTextId+" "+introText.Length);
            if (curTextId >= introText.Length)
                SceneManager.LoadScene("Scenes/StartScreen");
            else
                textObj.GetComponent<Text>().text = introText[curTextId];
        }
    }
}
