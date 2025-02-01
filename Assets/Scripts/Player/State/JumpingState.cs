using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    public override void EnterState(PlayerStateManager manager)
    {
        manager.Movement.ApplyJump();
        manager.Movement.Move(manager.Input.Move);
        manager.Animator.SetBool("Jump", true);
    }

    public override void UpdateState(PlayerStateManager manager)
    {
        manager.Movement.Move(manager.Input.Move);
        manager.Movement.RotateToCameraForward();

        if (manager.Movement.IsGrounded)
        {
            manager.Animator.SetBool("Jump", false);
            if (manager.Input.Move == Vector2.zero)
            {
                manager.SwitchState(manager.Idle);
                return;
            }

            manager.SwitchState(manager.Walking);
            return;
        }

    }
}
