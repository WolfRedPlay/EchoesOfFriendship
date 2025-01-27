using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Speed of the player")]
    [SerializeField] float _movementSpeed = 5f;

    [Tooltip("Gravity of the player")]
    [SerializeField] float _gravity = 1f;

    [Tooltip("Height of player's jump")]
    [SerializeField] float _jumpHeight = 2f;

    Transform _camera;
    CharacterController _controller;
    float _velocityY = 0f;


    public bool IsGrounded => _controller.isGrounded;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>().transform;

    }

    private void Update()
    {
        ApplyGravity();
    }

    public void Move(Vector2 direction)
    {
        Vector3 movement = transform.right * direction.x + transform.forward * direction.y + Vector3.up * _velocityY;

        _controller.Move(movement * _movementSpeed * Time.deltaTime);
    }
    
    public void ApplyGravity()
    {
        if (IsGrounded && _velocityY < 0)
            _velocityY = -.5f;
        else
            _velocityY += -_gravity * Time.deltaTime;
    }

    public void ApplyJump()
    {
        _velocityY = Mathf.Sqrt(2f * _gravity * _jumpHeight);
    }

    public void RotateToCameraForward()
    {
        Vector3 cameraForwardInPlayerCoord = _camera.forward;
        cameraForwardInPlayerCoord.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraForwardInPlayerCoord);
    }
}
