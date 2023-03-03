using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CreateCardManager : Singleton<CreateCardManager>
{
    public float dur;
    public AnimationCurve animationCurve;
    public Ease ease;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _animationTime;
    bool qwe = true;
    void Update()
    {
        if(qwe)
        {
            StartCoroutine(IECreateItem());
            qwe = false;
        }
    }
    // IEnumerator Start()
    // {
    //     yield return new WaitForSeconds(.2f);
    //     if(!GridManager.Instance.topGrids[0].isFull)
    //     {
    //         Vector3 createPos = new Vector3
    //         (
    //             GridManager.Instance.topGrids[0].position.x,
    //             GridManager.Instance.topGrids[0].position.y +1
    //         );
    //         // var obj = Instantiate(cardPrefabs[Random.Range(0,cardPrefabs.Count)],createPos,Quaternion.identity, GridManager.Instance.canvasParent);
    //         int type = 0;// Random.Range(0,ObjectPool.Instance.pools.Length);
    //         var obj = ObjectPool.Instance.GetPooledObject(type);
    //         obj.transform.position = createPos;
    //         obj.transform.parent = _parent;
    //         obj.gameObject.SetActive(true);
    //         // allItems.Add(obj);
    //         obj.gridController = GridManager.Instance.topGrids[0];
    //         obj.gridController.isFull = true;
    //         GridManager.Instance.topGrids[0].item = obj;
    //         // if(obj.gridController.bot.isFull)
    //         // {
    //         //     StartCoroutine(obj.GetComponent<Fall>().FallItem());
    //         // }
    //     }
    // }
    
    IEnumerator IECreateItem()
    {
        yield return new WaitForSeconds(_animationTime);
        for (int i = 0; i < GridManager.Instance.topGrids.Count; i++)
        {
            if(!GridManager.Instance.topGrids[i].isFull)
            {
                Vector3 createPos = new Vector3
                (
                    GridManager.Instance.topGrids[i].position.x,
                    GridManager.Instance.topGrids[i].position.y +1
                );
                // var obj = Instantiate(cardPrefabs[Random.Range(0,cardPrefabs.Count)],createPos,Quaternion.identity, GridManager.Instance.canvasParent);
                int type = 0;// Random.Range(0,ObjectPool.Instance.pools.Length);
                var obj = ObjectPool.Instance.GetPooledObject(type);
                obj.transform.position = createPos;
                obj.transform.parent = _parent;
                obj.gameObject.SetActive(true);
                // allItems.Add(obj);
                obj.gridController = GridManager.Instance.topGrids[i];
                obj.gridController.isFull = true;
                GridManager.Instance.topGrids[i].item = obj;
                if(!obj.gridController.bot.isFull)
                {
                    obj.GetComponent<Fall>().FallState.OnTrigger();
                }
            }
        }
        qwe = true;
    }
}
