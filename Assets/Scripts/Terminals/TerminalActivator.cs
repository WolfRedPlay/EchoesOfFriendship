using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalActivator : MonoBehaviour
{
    [SerializeField] TerminalManager _terminal;
    [SerializeField] Terminal2Manager _terminal2;

    bool _isStarted = false;
    void Update()
    {
        if (!_isStarted)
        {
            if (_terminal.IsActive && _terminal2.IsActive)
            {
                _terminal.StartTerminal();
                _terminal2.StartTerminal();
                _isStarted = true;

            }
        }
        else
        {
            if (!_terminal.IsActive || !_terminal2.IsActive)
            {
                _isStarted = false;
                _terminal.StopTerminal();
                _terminal2.StopTerminal();
            }

        }
    }

    public void Loss()
    {
        _isStarted = false;
        _terminal.GetComponent<TerminalInteractable>().CloseTerminal();

    }
}
