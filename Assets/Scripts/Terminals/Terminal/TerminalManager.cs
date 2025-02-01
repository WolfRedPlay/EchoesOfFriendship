using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TerminalManager : MonoBehaviour, ITerminal
{
    PowerController powerController;
    FrameController frameController;
    RuneMovementController runeController;
    StabilityManager stabilityManager;

    bool _isActive = false;

    public bool IsActive => _isActive;

    private void Awake()
    {
        powerController = GetComponentInChildren<PowerController>();
        if (powerController == null)
        {
            Debug.LogError("Power controller was not found");
            enabled = false;
            return;
        }

        frameController = GetComponentInChildren<FrameController>();
        if (frameController == null)
        {
            Debug.LogError("Frame controller was not found");
            enabled = false;
            return;
        }

        runeController = GetComponentInChildren<RuneMovementController>();
        if (runeController == null)
        {
            Debug.LogError("Rune controller was not found");
            enabled = false;
            return;
        }

        stabilityManager = GetComponentInChildren<StabilityManager>();
        if (stabilityManager == null)
        {
            Debug.LogError("Stability manager was not found");
            enabled = false;
            return;
        }
    }

    public void ActivateTerminal(PlayerInput input)
    {
        powerController.SetPlayerInput(input);
        powerController.SetActive(true);

        frameController.SetPlayerInput(input);
        frameController.SetActive(true);

        _isActive = true;
    }

    public void DeactivateTerminal()
    {
        powerController.SetActive(false);

        frameController.SetActive(false);

        StopTerminal();

        _isActive = false;
    }
    public void StartTerminal()
    {
        runeController.SetActive(true);
        stabilityManager.SetActive(true);
    }


    public void StopTerminal()
    {
        runeController.SetActive(false);
        stabilityManager.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
