using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GridManager : Singleton<GridManager>
{
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private Vector2Int _scale;
    [SerializeField] private Transform _parent;
    public Ease ease;
    public AnimationCurve animationCurve;
    [SerializeField] private GridController _gridPrefab;
    public List<GridController> allGrids;
    public List<GridController> topGrids;
    public Color disabledGridColor;
    private void Awake() {
    }
    void Start()
    {
        foreach (var item in allGrids)
        {
            item.SetNeighbor();
        }
        FindTopGrids();
    }
    public void FindTopGrids()
    {
        topGrids.Clear();
        foreach (var item in allGrids)
        {
            if(item.top == null)
            {
                topGrids.Add(item);
            }
        }
    }
    private void CreateGridController(Vector2Int index,Vector2 pos)
    {
        var obj = Instantiate(_gridPrefab,pos,Quaternion.identity,_parent);
        obj.index = index;
        allGrids.Add(obj);
    }
    [ContextMenu("CreateGrids")]
    private void CreateGrids()
    {
        for (int y = 0; y < _scale.y; y++)
        {            
            for (int x = 0; x < _scale.x; x++)
            {
                CreateGridController(new Vector2Int(x,y),new Vector2(_startPos.x + x,_startPos.y - y));
            }
        }
    }
    [ContextMenu("ClearGrids")]
    private void Clear()
    {
        int count = allGrids.Count;
        for (int i = 0; i < _parent.childCount; i++)
        {
            allGrids.Remove(_parent.GetChild(i).GetComponent<GridController>());
        }
    }
}
