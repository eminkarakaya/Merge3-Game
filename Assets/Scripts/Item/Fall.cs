using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : Item
{
    [Header("States")]
    [SerializeField] private FallState _fallState;
    [SerializeField] private WaitState _waitState;
    public FallState FallState { get => _fallState; set{} }
    public WaitState WaitState { get => _waitState; set{} }
    [Space(10)]
    [SerializeField] private FallStateBase _currentState;
    public FallStateBase CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            _currentState.OnStart();
        }
    }

    
    
    void Start()
    {
        CurrentState.OnStart();
    }
    void Update()
    {
        _currentState.OnUpdate();
    }
}
