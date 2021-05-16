using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    private Vector3 handleToOriginVector;
    public bool isDragging, isMovable = true;
    private int cellX = -1, cellY = -1;
    public int topCount, rightCount, bottomCount, leftCount;
    [SerializeField] private GameObject[] topPoints, rightPoints, bottomPoints, leftPoints;
    public int startX, startY;

    #region Connected
    public bool FullContacted()
    {
        return TopConnected() && RightConnected() && BottomConnected() && LeftConnected();
    }

    private bool TopConnected()
    {
        var controller = SolderingController.Instance;
        if (cellY + 1 >= controller.height)
        {
            if (topCount != 0)
                return false;
        }
        else if (topCount != 0)
        {
            if (controller.cells[cellY + 1, cellX].chip == null || controller.cells[cellY + 1, cellX].chip != null && 
                controller.cells[cellY + 1, cellX].chip.bottomCount != topCount)
                return false;
        }

        return true;
    }
    private bool RightConnected()
    {
        var controller = SolderingController.Instance;   
        if (cellX + 1 >= controller.height)
        {
            if (rightCount != 0)
                return false;
        }
        else if (rightCount != 0)
        {
            if (controller.cells[cellY, cellX + 1].chip == null || controller.cells[cellY, cellX + 1].chip != null && 
                controller.cells[cellY, cellX + 1].chip.leftCount != rightCount)
                return false;
        }
        return true;
    }
    private bool BottomConnected()
    {
        var controller = SolderingController.Instance;
        if (cellY - 1 < 0)
        {
            if (bottomCount != 0)
                return false;
        }
        else if (bottomCount != 0)
        {
            if (controller.cells[cellY - 1, cellX].chip == null || controller.cells[cellY - 1, cellX].chip != null && 
                controller.cells[cellY - 1, cellX].chip.topCount != bottomCount)
                return false;
        }
        return true;
    }
    private bool LeftConnected()
    {
        var controller = SolderingController.Instance;
        if (cellX - 1 < 0)
        {
            if (leftCount != 0)
                return false;
        }
        else if (leftCount != 0)
        {
            if (controller.cells[cellY, cellX - 1].chip == null || controller.cells[cellY, cellX - 1].chip != null && 
                controller.cells[cellY, cellX - 1].chip.rightCount != leftCount)
                return false;
        }
        return true;
    }
    #endregion
    
    private void Start()
    {
        var controller = SolderingController.Instance;
        CreateDots();
        controller.onSolderingControllerInitialized += () =>
        {
            if (!isMovable)
            {
                var cell = controller.cells[startY, startX];
                transform.root.position = new Vector2(controller.minX + startX * controller.stepX,
                    controller.minY + startY * controller.stepY);

                cellX = startX;
                cellY = startY;
                cell.isOccupied = true;
                cell.chip = this;
                GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
        };
    }

    private void CreateDots()
    {
        EnablePoints(topCount, topPoints);
        EnablePoints(rightCount, rightPoints);
        EnablePoints(bottomCount, bottomPoints);
        EnablePoints(leftCount, leftPoints);
    }

    private void PaintPoints(GameObject[] objects, Color color)
    {
        foreach (var obj in objects)
        {
            obj.GetComponent<SpriteRenderer>().color = color;
        }
    }
    
    private void LightPoints(Color color)
    {
        var controller = SolderingController.Instance;
        if (TopConnected() && topCount > 0)
        {
            PaintPoints(topPoints, color);
            PaintPoints(controller.cells[cellY + 1, cellX].chip.bottomPoints, color);
        }

        if (RightConnected() && rightCount > 0)
        {
            PaintPoints(rightPoints, color);
            PaintPoints(controller.cells[cellY, cellX + 1].chip.leftPoints, color);
        }

        if (BottomConnected() && bottomCount > 0)
        {
            PaintPoints(bottomPoints, color);
            PaintPoints(controller.cells[cellY - 1, cellX].chip.topPoints, color);
        }

        if (LeftConnected() && leftCount > 0)
        {
            PaintPoints(leftPoints, color);
            PaintPoints(controller.cells[cellY, cellX - 1].chip.rightPoints, color);
        }

    }
    
    private void EnablePoints(int count, GameObject[] points)
    {
        switch (count)
        {
            case 1:
                points[1].SetActive(true);
                break;
            case 2:
                points[0].SetActive(true);
                points[2].SetActive(true);
                break;
            case 3:
                points[0].SetActive(true);
                points[1].SetActive(true);
                points[2].SetActive(true);
                break;
        }
    }

    void OnMouseDown () {
        handleToOriginVector = transform.root.position - Camera.main.ScreenToWorldPoint (Input.mousePosition);
        isDragging = true;
    }
    
    
    void OnMouseDrag ()
    {
        if (!isMovable)
            return;
        var controller = SolderingController.Instance;
        
        if (cellX >= 0 && cellY >= 0)
        {
            LightPoints(Color.gray);
            controller.cells[cellY, cellX].isOccupied = false;
            controller.cells[cellY, cellX].chip = null;
            controller.OnChipRemoved();
            cellX = -1;
            cellY = -1;
        }

        foreach (var r in GetComponentsInChildren<SpriteRenderer>())
        {
            r.sortingOrder = 10;
        }
        GetComponent<SpriteRenderer>().sortingOrder = 10;
        transform.root.position = Camera.main.ScreenToWorldPoint (Input.mousePosition) + handleToOriginVector;
    }
 
    void OnMouseUp ()
    {
        var controller = SolderingController.Instance;
        isDragging = false;
        var pos = transform.root.position;
        int x = Mathf.RoundToInt((pos.x - controller.minX) / controller.stepX);
        int y = Mathf.RoundToInt((pos.y - controller.minY) / controller.stepY);
        if (x == controller.width)
            x -= 1;
        else if (x == -1)
            x += 1;
        if (y == controller.height)
            y -= 1;
        else if (y == -1)
            y += 1;
        
        if (controller.width > x && x >= 0 && controller.height > y && y >= 0)
        {
            var cell = controller.cells[y, x];
            if (!cell.isOccupied)
            {
                transform.root.position = new Vector2(controller.minX + x * controller.stepX,
                    controller.minY + y * controller.stepY);

                cellX = x;
                cellY = y;
                cell.isOccupied = true;
                cell.chip = this;
                foreach (var r in GetComponentsInChildren<SpriteRenderer>())
                {
                    r.sortingOrder = 1;
                }
                GetComponent<SpriteRenderer>().sortingOrder = 1;
                controller.OnChipPlaced();
                LightPoints(Color.green);
            }
        }
    }
}
