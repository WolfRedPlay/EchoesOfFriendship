using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputHandler : MonoBehaviour, AxisState.IInputAxisProvider
{
    [SerializeField] PlayerInput _input;

    InputAction _look;
    InputAction _zoom;

    private void Start()
    {
        _look = _input.actions.FindAction("Look");
        _zoom = _input.actions.FindAction("Zoom");
    }

    public float GetAxisValue(int axis)
    {
        if (enabled)
        {
            switch (axis)
            {
                case 0: return _look.ReadValue<Vector2>().x;
                case 1: return _look.ReadValue<Vector2>().y;
                case 2: return _zoom.ReadValue<float>();
            }
        }
        return 0;
    }

}
