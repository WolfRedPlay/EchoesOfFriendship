using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerController : MonoBehaviour
{

    [SerializeField] float _speed = 0.1f;
    [SerializeField] PlayerInput _input;
    [SerializeField] float _leftmostXPosition = 1f;



    InputAction _action;
    const float MinScale = 0f;
    const float MaxScale = 1f;

    float _value = 0f;

    public float Value => _value;
    private void Start()
    {
        _action = _input.actions.FindAction("Change");
    }

    private void Update()
    {
        float input = _action.ReadValue<float>();
        
        CalculateValue(input);

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Lerp(MinScale, MaxScale, _value);
        transform.localScale = scale;

        transform.localPosition = new Vector3(_leftmostXPosition - (_leftmostXPosition * transform.localScale.x), transform.localPosition.y, transform.localPosition.z);

    }

    private void CalculateValue(float input)
    {
        if (input > 0)
        {
            _value += _speed * Time.deltaTime;
        }
        if (input < 0)
        {
            _value -= _speed * Time.deltaTime;
        }
        _value = Mathf.Clamp01(_value);
    }
}
