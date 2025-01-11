using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public abstract void EnterState(PlayerStateManager manager);
    public abstract void UpdateState(PlayerStateManager manager);
}
