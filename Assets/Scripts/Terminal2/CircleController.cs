using UnityEngine;
using UnityEngine.InputSystem;

public class CircleController : MonoBehaviour
{
    [SerializeField] PlayerInput _input;
    [SerializeField] float _rotateSpeed = 5f;

    InputAction _action;
    PowerController _powerController;


    private void Start()
    {
        _action = _input.actions.FindAction("Navigate");

        _powerController = FindAnyObjectByType<PowerController>();
        if (_powerController == null)
        {
            Debug.LogError("No Power Controller found!");
            this.enabled = false;
            return;
        }
    }

    private void Update()
    {
        float rotation = CalculateRotation();

        transform.Rotate(0, 0, rotation * Time.deltaTime, Space.Self);
    }


    private float CalculateRotation()
    {
        float inputValue = -_action.ReadValue<Vector2>().x;

        float rotation = inputValue * (_rotateSpeed + 50 * _powerController.Value);

        return rotation;
    }

}
