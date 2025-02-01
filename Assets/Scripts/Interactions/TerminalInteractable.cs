using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class TerminalInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _camera;

    PlayerInput _input;

    Interactor _interactor;
    ITerminal _terminalManager;

    bool _isBusy = false;

    private void Start()
    {
        if(!TryGetComponent(out _terminalManager))
        {
            Debug.LogError("Terminal manager was not found");
            enabled = false;
            return;
        }
    }


    public void Interact(Interactor interactor)
    {
        if (!_isBusy)
        {
            _input = _interactor.Input;

            _camera.layer = _interactor.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer;

            string newLayer = LayerMask.LayerToName(_camera.layer);

            if (newLayer == "Player1")
            {
                _interactor.PlayerModel.layer = LayerMask.NameToLayer("Hidden1");
                _interactor.BackPack.layer = LayerMask.NameToLayer("Hidden1");
                foreach (Transform child in _interactor.BackPack.transform)
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Hidden1");
                }
            }
            if (newLayer == "Player2")
            {
                _interactor.PlayerModel.layer = LayerMask.NameToLayer("Hidden2");
                _interactor.BackPack.layer = LayerMask.NameToLayer("Hidden2");
                foreach (Transform child in _interactor.BackPack.transform)
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Hidden2");
                }
            }


            _camera.gameObject.SetActive(true);
            _input.SwitchCurrentActionMap("UI");

            _terminalManager.ActivateTerminal(_input);

            _input.actions.FindAction("Cancel").started += OnCancelPressed;
            _isBusy = true;

        }
        

        
    }

    private void OnCancelPressed(InputAction.CallbackContext context)
    {
        CloseTerminal();

    }

    public void CloseTerminal()
    {
        _terminalManager.DeactivateTerminal();
        _camera.gameObject.SetActive(false);
        _input.SwitchCurrentActionMap("Player");

        _interactor.PlayerModel.layer = LayerMask.NameToLayer("Default");
        _interactor.BackPack.layer = LayerMask.NameToLayer("Default");
        foreach (Transform child in _interactor.BackPack.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        _interactor.SetActive(false);

        _input.actions.FindAction("Cancel").started -= OnCancelPressed;
        _isBusy = false;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _interactor))
        {
            _interactor.SetInteractable(this);
        }
    }
}
