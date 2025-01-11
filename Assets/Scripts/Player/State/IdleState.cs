using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public override void EnterState(PlayerStateManager manager)
    {
        
    }

    public override void UpdateState(PlayerStateManager manager)
    {
        if (manager.Input.Move != Vector2.zero)
        {
            manager.SwitchState(manager.Walking);
        }
    }
}
