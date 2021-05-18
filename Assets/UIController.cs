using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject lightPanel;

    private IEnumerator lightCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableBlink()
    {
        lightCoroutine = LightOnOff();
        StartCoroutine(lightCoroutine);
    }

    public void DisableBlink()
    {
        StopCoroutine(lightCoroutine);
    }
    
    private IEnumerator LightOnOff()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            lightPanel.SetActive(!lightPanel.activeSelf);
        }
    }
}
