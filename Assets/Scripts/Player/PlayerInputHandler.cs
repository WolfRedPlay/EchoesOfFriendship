using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    InputAction _move;
    InputAction _jump;

    public Vector2 Move => _move.ReadValue<Vector2>();
    public bool Jump => _jump.IsPressed();

    // Start is called before the first frame update
    void Start()
    {
        _move = GetComponent<PlayerInput>().actions.FindAction("Move");
        _jump = GetComponent<PlayerInput>().actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
