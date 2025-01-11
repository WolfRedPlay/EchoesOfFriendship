using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    InputAction _move;

    public Vector2 Move => _move.ReadValue<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        _move = GetComponent<PlayerInput>().actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
