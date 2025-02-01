using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Terminal2Manager : MonoBehaviour, ITerminal
{
    RunesManager _runesManager;
    CircleController _circleController;
    ProgressBarManager _progressBarManager;
    RuneSpaceController _runeSpaceController;


    bool _isActive = false;

    public bool IsActive => _isActive;
    private void Awake()
    {
        _runesManager = GetComponentInChildren<RunesManager>();
        if (_runesManager == null)
        {
            Debug.LogError("Runes manager was not found");
            enabled = false;
            return;
        }

        _circleController = GetComponentInChildren<CircleController>();
        if (_circleController == null)
        {
            Debug.LogError("Circle controller was not found");
            enabled = false;
            return;
        }

        _progressBarManager = GetComponentInChildren<ProgressBarManager>();
        if (_progressBarManager == null)
        {
            Debug.LogError("Progress Bar Manager was not found");
            enabled = false;
            return;
        }

        _runeSpaceController = GetComponentInChildren<RuneSpaceController>();
        if (_runeSpaceController == null)
        {
            Debug.LogError("Rune Space Controller was not found");
            enabled = false;
            return;
        }
    }


    public void ActivateTerminal(PlayerInput input)
    {
        _runesManager.AssignSignsToRunes();

        _circleController.SetPlayerInput(input);
        _circleController.SetActive(true);

        _isActive = true;

    }

    public void DeactivateTerminal()
    {
        _runesManager.CleanRunes();

        _circleController.SetActive(false);

        StopTerminal();

        _isActive = false;
    }

    public void StartTerminal()
    {
        _progressBarManager.SetActive(true);

        _runeSpaceController.SetActive(true);
    }

    public void StopTerminal()
    {
        _progressBarManager.SetActive(false);

        _runeSpaceController.SetActive(false);
    }
}
