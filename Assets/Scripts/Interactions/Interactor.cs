using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public GameObject PlayerModel;
    public GameObject BackPack;
    PlayerInput _input;
    InputAction _action;


    IInteractable _interactable;

    bool _isActive = false;


    public PlayerInput Input => _input;
    public void SetActive(bool active) { _isActive = active; }
    public void SetInteractable(IInteractable interactable) {  _interactable = interactable; }

    private void Start()
    {
        if(!TryGetComponent(out _input))
        {
            Debug.LogError("Player Input was not find!!!");
            enabled = false;
            return;
        }

        _action = _input.actions.FindAction("Interact");

        _action.started += TryToInteract;
    }

    private void TryToInteract(InputAction.CallbackContext context)
    {
        if(_interactable != null && !_isActive)
        {
            _interactable.Interact(this);
            _isActive = true;
        }
    }
}
