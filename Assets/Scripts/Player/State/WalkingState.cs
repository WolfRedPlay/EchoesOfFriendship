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
        manager.Animator.SetFloat("X", manager.Input.Move.x);
        manager.Animator.SetFloat("Y", manager.Input.Move.y);
        manager.Movement.RotateToCameraForward();
    }
}
