using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : FallStateBase
{
    public override void OnStart()
    {

    }
    public override void OnUpdate()
    {   
        if(fall.gridController.bot != null && fall.gridController.bot.IsAvailableDown())
        {
            fall.CurrentState = fall.FallState;
            
        }
    }

    public override void OnTrigger()
    {

    }
}
