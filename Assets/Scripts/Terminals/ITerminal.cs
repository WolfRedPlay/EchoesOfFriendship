using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ITerminal
{
    public void ActivateTerminal(PlayerInput input);
    public void DeactivateTerminal();
    public void StartTerminal();
    public void StopTerminal();
}
