using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Speed of the player")]
    [SerializeField] float _movementSpeed = 5f;
    
    
    Transform _camera;
    CharacterController _controller;


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>().transform;
    }

    public void Move(Vector2 direction)
    {
        Vector3 movement = transform.right * direction.x + transform.forward * direction.y;

        _controller.Move(movement * _movementSpeed * Time.deltaTime);
    }

    public void RotateToCameraForward()
    {
        Vector3 cameraForwardInPlayerCoord = _camera.forward;
        cameraForwardInPlayerCoord.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraForwardInPlayerCoord);
    }

    
}
