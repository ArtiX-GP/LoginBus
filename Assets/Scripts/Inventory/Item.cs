using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Item: MonoBehaviour
{
   public string name;
   public int itemId;
   public int cellId = -1;
   
   private Vector3 handleToOriginVector;
   private bool isDragging, shouldDrag;
   private IEnumerator dragCoroutine;
   private Battery battery;

   void Start()
   {
      dragCoroutine = Drag();
      battery = FindObjectOfType<Battery>();
   }
   void OnMouseDown () {
      var inventory = Inventory.Instance;
      if (shouldDrag)
      {
         if (battery.bounds.Contains(transform.position))
         {
            if (battery.OnClick())
            {
               EndDrag();
               cellId =  inventory.PlaceItem(this);
            }
         }
         else
            PlaceObject();
      }
      else
      {
         var playerIsClose = CloseEnough();
         var itemInInventory = inventory.ContainsItem(this);
         if (!playerIsClose && !itemInInventory)
         {
            Debug.Log("Да это же " + name);
            return;
         }
         handleToOriginVector = transform.root.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
         isDragging = true;
         if (!itemInInventory)
            cellId = inventory.PlaceItem(this);
         else if(inventory.currentItem == null)
         {
            shouldDrag = true;
            inventory.currentItem = this;
            inventory.RemoveItem(cellId);
            cellId = -1;

            StartCoroutine(dragCoroutine);
         }
      }
   }

   private bool CloseEnough()
   {
      var dist = Vector2.Distance(transform.root.position, Game.Instance.currentPlayer.transform.position);
      return dist <= 1;
   }
   
   IEnumerator Drag()
   {
      while (shouldDrag)
      {
         yield return null;
         transform.root.position = Camera.main.ScreenToWorldPoint (Input.mousePosition) + handleToOriginVector;
      }
      Debug.Log("end dragging");
   }
    
   /*void OnMouseDrag ()
   {
      var inventory = Inventory.Instance;
        
      if (cellId >= 0)
      {
         inventory.cells[cellId].isOccupied = false;
         inventory.cells[cellId].item = null;
         cellId = -1;
      }

      GetComponent<SpriteRenderer>().sortingOrder = 10;
      transform.root.position = Camera.main.ScreenToWorldPoint (Input.mousePosition) + handleToOriginVector;
   }*/

   private void PlaceObject()
   {
      var inventory = Inventory.Instance;
      isDragging = false;
      var pos = transform.root.position;
      int x = Mathf.RoundToInt((pos.x - inventory.minX) / inventory.stepX);
      int y = Mathf.RoundToInt((pos.y - inventory.minY) / inventory.stepY);
      if (x == inventory.width)
         x -= 1;
      else if (x == -1)
         x += 1;
      if (y == inventory.height)
         y -= 1;
      else if (y == -1)
         y += 1;
        
      Debug.Log(x + " " + y);
      if (inventory.width > x && x >= 0 && inventory.height > y && y >= 0)
      {
         var cell = inventory.cells[y];
         if (!cell.isOccupied)
         {
            EndDrag();
            inventory.PlaceItem(this, y);
            cellId = y;
            
            //GetComponent<SpriteRenderer>().sortingOrder = 1;
         }
      }
   }

   private void EndDrag()
   {
      //GetComponent<SpriteRenderer>().sortingOrder = 1;
      shouldDrag = false;
      StopCoroutine(dragCoroutine);
      
   }
   
}
