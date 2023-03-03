using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EminTween : MonoBehaviour
{
    public IEnumerator Move(Transform current, Vector3 target, float time, Coroutine onComplate)
    {
        var passed = 0f;
        var initPosition =  Vector3.zero;//fall.transform.position;
        while (passed < time)
        {
            passed += Time.deltaTime;
            var normalized = passed / time;
            
            var position = Vector3.Lerp(initPosition,target,CreateCardManager.Instance.animationCurve.Evaluate(normalized));
            current.position = position;
            yield return null;
        }
        StartCoroutine("onComplate");
    }
}
