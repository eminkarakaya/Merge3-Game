using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GridController : MonoBehaviour
{
    [SerializeField] private Item _item;
    public Item item
    {
        get => _item;
        set
        {
            _item = value;
            if(_item == null)
            {
                isFull = false;
            }
            else
            {
                isFull = true;
            }
        }
    }
    public Vector2Int index;
    public Vector2 position;
    public bool roadBlocked;
    public bool isDisabledGrid;
    public bool isFull;
    // public List<GridController> directions;
    public GridController top,bot,right,left,topLeft,topRight,botLeft,botRight;
    void Start()
    {
        position = this.transform.position;
        SetNeighbor();
        // directions.Add(top);
        // directions.Add(bot);
        // directions.Add(right);
        // directions.Add(left);
        if(isDisabledGrid)      
        {
            GetComponent<SpriteRenderer>().color = GridManager.Instance.disabledGridColor;
        }

    }
    [ContextMenu("DisableGrids")]
    public void DisableGrids()
    {
        if(isDisabledGrid)      
        {
            GridController temp = bot;
            while(temp != null)
            {
                temp.roadBlocked = true;
                temp = temp.bot;
            }

        }
    }

    public bool IsAvailableDown()
    {
        if(isFull || isDisabledGrid) 
            return false;
        return true;
    }
    public bool IsAvailableSide()
    {
        if(isFull || isDisabledGrid || !roadBlocked) 
            return false;
        
        return true;
    }
    bool CheckLeft(int i)
    {
        if(this.index.x -1 == GridManager.Instance.allGrids[i].index.x) return true;
        return false;
    }
    bool CheckRight(int i)
    {
        if(this.index.x +1 == GridManager.Instance.allGrids[i].index.x) return true;
        return false;
    }
    bool CheckBottom(int i)
    {
        if(this.index.y +1 == GridManager.Instance.allGrids[i].index.y) return true;
        return false;
    }
    bool CheckTop(int i)
    {
        if(this.index.y -1 == GridManager.Instance.allGrids[i].index.y) return true;
        return false;
    }
    [ContextMenu("SetNeighbor")]
    public void SetNeighbor()
    {
        for (int i = 0; i < GridManager.Instance.allGrids.Count; i++)
        {
            if(this.index.y == GridManager.Instance.allGrids[i].index.y)
            {
                if(CheckRight(i))
                {
                    right = GridManager.Instance.allGrids[i];
                }
                else if(CheckLeft(i))
                {
                    left = GridManager.Instance.allGrids[i];
                }
            }
            else if(this.index.x == GridManager.Instance.allGrids[i].index.x)
            {
                if(CheckBottom(i))
                {
                    bot = GridManager.Instance.allGrids[i];
                }
                else if(CheckTop(i))
                {  
                    top = GridManager.Instance.allGrids[i];
                }
            }
            if(CheckTop(i) && CheckRight(i))
                topRight = GridManager.Instance.allGrids[i];
            else if(CheckTop(i)&& CheckLeft(i))
                topLeft = GridManager.Instance.allGrids[i];
            
            else if(CheckBottom(i)&& CheckLeft(i))
                botLeft = GridManager.Instance.allGrids[i];
            else if(CheckBottom(i)&& CheckRight(i))
                botRight = GridManager.Instance.allGrids[i];
            
            
            // else if(this.index.x+1 == gridManeger.allGrids[i].index.x && this.index.y -1 == gridManeger.allGrids[i].index.y)
            // {
            //     topRight = gridManeger.allGrids[i];
            // }
            // else if(this.index.x-1 == gridManeger.allGrids[i].index.x && this.index.y -1 == gridManeger.allGrids[i].index.y)
            // {
            //     topLeft = gridManeger.allGrids[i];
            // }
            // else if(this.index.x-1 == gridManeger.allGrids[i].index.x && this.index.y +1 == gridManeger.allGrids[i].index.y)
            // {
            //     botLeft = gridManeger.allGrids[i];
            // }
            // else if(this.index.x+1 == gridManeger.allGrids[i].index.x && this.index.y +1 == gridManeger.allGrids[i].index.y)
            // {
            //     botRight = gridManeger.allGrids[i];
            // } 
        }
        
    }

    
}