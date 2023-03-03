using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private  List<Fall> Items = new List<Fall>();
    void Start()
    {
        
        for (int i = GridManager.Instance.allGrids.Count-1; i >= 0; i--)
        {
            if(GridManager.Instance.allGrids[i].item!= null && GridManager.Instance.allGrids[i].item.TryGetComponent(out Fall fall))
            {
                Items.Add(fall);
            }
        }
            
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Items.Clear();
            for (int i = GridManager.Instance.allGrids.Count-1; i >= 0; i--)
            {
                if(GridManager.Instance.allGrids[i].item!= null && GridManager.Instance.allGrids[i].item.TryGetComponent(out Fall fall))
                {
                    Items.Add(fall);
                }
            }
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].FallState.OnTrigger();
            }
        }
    }
    public void Trigger()
    {
        foreach (var item in Items)
        {
            if(item != null)
            {
                item.FallState.OnTrigger();
            }
        }
    }
}
