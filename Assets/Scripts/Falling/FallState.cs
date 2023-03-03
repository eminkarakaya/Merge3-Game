using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallState : FallStateBase
{
    [SerializeField] private List <Transform> targets;
    private GridController temp;
    public override void OnStart()
    {
        temp = fall.gridController;
    }
    public override void OnUpdate()
    {
        // StartCoroutine(FallItem());
    }
    public override void OnTrigger()
    {
        temp = fall.gridController;
        FindDownTarget();
    }
    IEnumerator FallItem()
    {
        int count = 0;
        Vector2 pos = fall.gridController.transform.position;
        while(fall.gridController.bot != null && fall.gridController.bot.IsAvailableDown())
        {    
            count ++;
            fall.gridController.item = null;
            fall.gridController = fall.gridController.bot;
            fall.gridController.item = fall;
            pos = fall.gridController.transform.position;
            // yield return new WaitForSeconds(.2f);
            yield return null;
        }
        pos = fall.gridController.transform.position;
        if(count != 0)
            StartCoroutine(Move(fall.transform,pos,CreateCardManager.Instance.dur));
        else
        {
            fall.CurrentState = fall.WaitState;
        }
        // fall.transform.DOMove(fall.gridController.transform.position,2f).SetEase(Ease.Linear);
    }
    private bool CheckDownMovement(GridController gridController)
    {
        if(gridController.bot != null && gridController.bot.IsAvailableDown())
        {
            return true;
        }
        return false;
    }
    private bool CheckSideMovement(GridController gridController)
    {
        if((gridController.botRight != null && gridController.botRight.IsAvailableDown()) || (gridController.botLeft != null && gridController.botLeft.IsAvailableDown()))
        {
            return true;
        }
        
        return false;
    }
    private void FindSideTarget()
    {
        Transform target = null;
        if(temp.botRight == null || !temp.botRight.IsAvailableSide())
        {
            while(temp.botLeft != null && temp.botLeft.IsAvailableSide())
            {
                temp = temp.botLeft;
                if(CheckDownMovement(temp))
                {
                    target = temp.transform;
                    if(target != null)
                        targets.Add(target);
                    FindDownTarget();
                }
            }
        }
            
        else
        {
            while(temp.botRight != null && temp.botRight.IsAvailableSide())
            {
                temp = temp.botRight;
                if(CheckDownMovement(temp))
                {
                    target = temp.transform;
                    if(target != null)
                        targets.Add(target);
                    FindDownTarget();
                }
            }
        }
        
        target = temp.transform;
        if(target != null)
            targets.Add(target);
        if(CheckDownMovement(temp))
        {
            FindDownTarget();
        }
        else
        {
            fall.gridController.item = null;
            fall.gridController = temp;
            fall.gridController.item = fall;
            
            Sequence seq = DOTween.Sequence();
            for (int i = 0; i < targets.Count; i++)
            {
                seq.Append(fall.transform.DOMove(targets[i].transform.position,CreateCardManager.Instance.dur).SetEase(CreateCardManager.Instance.ease));
                // StartCoroutine(Move(fall.transform,targets[i].transform.position,CreateCardManager.Instance.dur));
            }
            temp = fall.gridController;
        }
    }
    // asagı dogru buluyo
    private void FindDownTarget()
    {
        if(temp.bot == null || !temp.bot.IsAvailableDown())
            return;
        Transform target = null;
        while(temp.bot != null && temp.bot.IsAvailableDown())
        {
            temp = temp.bot;
        }
        target = temp.transform;
        if(target != null)
            targets.Add(target);
        if(CheckSideMovement(temp))
        {
            FindSideTarget();
        }
        else
        {
            // fall ın grıdını atıyo
            fall.gridController.item = null;
            fall.gridController = temp;
            fall.gridController.item = fall;
            
             Sequence seq = DOTween.Sequence();
            for (int i = 0; i < targets.Count; i++)
            {
                seq.Append(fall.transform.DOMove(targets[i].transform.position,CreateCardManager.Instance.dur).SetEase(CreateCardManager.Instance.ease));
                // StartCoroutine(Move(fall.transform,targets[i].transform.position,CreateCardManager.Instance.dur));
            }
            temp = fall.gridController;

        }
    }
    IEnumerator Move(Transform current, Vector3 target,float time)    
    {
        var passed = 0f;
        var initPosition = fall.transform.position;
        while (passed < time)
        {
            passed += Time.deltaTime;
            var normalized = passed / time;
            
            var position = Vector3.Lerp(initPosition,target,CreateCardManager.Instance.animationCurve.Evaluate(normalized));
            current.position = position;
            yield return null;
        }
    }
    // IEnumerator FallLeaf(Transform target)
    // {
    //     float startY = target.position.y;
    //     float curvedPos = 0;
    //     float t = 0;
    //     float finalPosY = target.position.y;
    //     while(t<= CreateCardManager.Instance.dur)
    //     {
    //         var lerpVal = t/CreateCardManager.Instance.dur;
    //         // curvedPos = Mathf.Lerp(startPos.z,finalPosZ,lerpVal);
    //         curvedPos = startY + CreateCardManager.Instance.animationCurve.Evaluate(lerpVal);
    //         fall.transform.position = new Vector2(target.position.x,curvedPos);
    //         t += Time.deltaTime;
    //         yield return null;
    //     }
    // }
}