using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Speed of the player")]
    [SerializeField] float _moveSpeed = 10f;
    
    Transform _camera;

    CharacterController _controller;

    InputAction _move;
    InputAction _look;

    Vector3 cameraForwardInPlayer = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        _camera = GetComponentInChildren<Camera>().transform;

        PlayerInput playerInput = GetComponent<PlayerInput>();

        _move = playerInput.actions.FindAction("Move");
        if (_move == null) Debug.LogError("Move action is not assigned");

        _look = playerInput.actions.FindAction("Look");
        if (_look == null) Debug.LogError("Look action is not assigned");

    }

    // Update is called once per frame
    void Update()
    {
        

        Vector3 movement = ReadInput();
        CalculateMovement(ref movement);

        _controller.Move(movement);

        cameraForwardInPlayer = transform.InverseTransformDirection(_camera.forward);
        cameraForwardInPlayer.y = 0f;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, cameraForwardInPlayer);
    }

    private Vector3 ReadInput()
    {
        if(_move == null) return Vector3.zero;
        float x = _move.ReadValue<Vector2>().x;
        float y = _move.ReadValue<Vector2>().y;

        Vector3 movement = new Vector3(x, 0, y);

        return movement;
    }


    private void CalculateMovement(ref Vector3 movement)
    {
        movement *= _moveSpeed * Time.deltaTime;
    }
}
