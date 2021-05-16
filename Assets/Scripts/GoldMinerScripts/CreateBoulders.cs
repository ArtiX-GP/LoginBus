using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoulders : MonoBehaviour
{
    public int boulders_n;

    public GameObject BoulderPrefab;

    // box border exchange wirh collider
    float minHeight = -4f;

    float maxWidth = 10f;

    float minWidth = -10f;

    float maxHeight = 0f;

    void Start()
    {
        for (int i = 0; i < boulders_n; i++)
        {
            GameObject boulderT = createBoulder( i,
                Random.Range(maxWidth, minWidth),
                Random.Range(minHeight, maxHeight),
                BoulderPrefab);
            boulderT.transform.SetParent(gameObject.transform);
        }
    }

    public GameObject createBoulder(int id, float x, float y, GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        obj.AddComponent<BoxCollider2D>();
        obj.gameObject.name = id.ToString();
        return obj;
    }
}
