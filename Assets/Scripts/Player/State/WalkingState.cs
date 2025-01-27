using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerState
{
    public override void EnterState(PlayerStateManager manager)
    {
        
    }

    public override void UpdateState(PlayerStateManager manager)
    {

        if (manager.Input.Move == Vector2.zero)
        {
            manager.SwitchState(manager.Idle);
            return;
        }

        if (manager.Movement.IsGrounded && manager.Input.Jump)
        {
            manager.SwitchState(manager.Jumping);
            return;
        }

        manager.Movement.Move(manager.Input.Move);
        manager.Movement.RotateToCameraForward();
    }
}
