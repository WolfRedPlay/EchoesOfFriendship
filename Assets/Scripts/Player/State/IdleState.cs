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

        if (manager.Movement.IsGrounded && manager.Input.Jump)
        {
            manager.SwitchState(manager.Jumping);
            return;
        }


        if (manager.Input.Move != Vector2.zero)
        {
            manager.SwitchState(manager.Walking);
            return;
        }

        manager.Movement.Move(Vector2.zero);
        manager.Animator.SetFloat("X", 0);
        manager.Animator.SetFloat("Y", 0);

    }
}
