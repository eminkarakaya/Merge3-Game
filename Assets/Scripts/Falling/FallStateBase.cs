using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallStateBase : MonoBehaviour
{
    [SerializeField] protected Fall fall;
    public abstract void OnUpdate();
    public abstract void OnStart();
    public abstract void OnTrigger();
}
