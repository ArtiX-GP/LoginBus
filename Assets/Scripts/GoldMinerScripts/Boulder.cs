using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public float x;

    public float y;

    public int id;

    public bool status; // true if picked, false if not

    public GameObject prefab;

    public GameObject obj;

    public Boulder(int _id, float _x, float _y, GameObject _prefab)
    {
        this.id = _id;
        this.x = _x;
        this.y = _y;
        this.status = false;
        this.prefab = _prefab;
    }

    public void render()
    {
        this.obj =  Instantiate(this.prefab, new Vector3(this.x, this.y, 0),  Quaternion.identity);
        this.obj.AddComponent<BoxCollider2D>();
    }
}
