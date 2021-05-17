using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public Bounds bounds;

    void Start()
    {
        bounds = GetComponent<Renderer>().bounds;
       
    }
    
    public bool OnClick()
    {
        var playerIsClose = CloseEnough();
        if (!playerIsClose)
        {
            Debug.Log("Да это же акка.. акку.. большая батарейка!!!");
            return false;
        }
        
        var inventory = Inventory.Instance;
        var tool = inventory.currentItem;
        inventory.PlaceItem(inventory.currentItem);
        switch (tool.itemId)
        {
            default:
                Debug.Log("Ты нажал на меня, держа в руках " + tool.name);
                break;
        }

        return true;
    }
    
    
    private bool CloseEnough()
    {
        return Vector2.Distance(transform.root.position, Game.Instance.currentPlayer.transform.position) <= 1;
    }
}
