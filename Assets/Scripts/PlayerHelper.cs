using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        GetComponent<grumbleAMP>().PlaySong(0, 0, 1);
    }

}
