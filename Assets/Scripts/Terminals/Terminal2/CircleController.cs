using UnityEngine;
using UnityEngine.InputSystem;

public class CircleController : MonoBehaviour
{
    [SerializeField] PlayerInput _input;
    [SerializeField] float _rotateSpeed = 5f;


    AudioSource _source;
    InputAction _action;
    PowerController _powerController;
    bool _isActive = false;

    Quaternion _startRotation;

    public void SetActive(bool active) 
    { 
        _isActive = active;
        if (_isActive) _source.Stop();
        transform.localRotation = _startRotation;
    }
    public void SetPlayerInput(PlayerInput playerInput)
    {
        _input = playerInput;
        _action = _input.actions.FindAction("Navigate");
    }

    private void Start()
    {
        _powerController = FindAnyObjectByType<PowerController>();
        if (_powerController == null)
        {
            Debug.LogError("No Power Controller found!");
            this.enabled = false;
            return;
        }

        _source = GetComponent<AudioSource>();

        _startRotation = transform.localRotation;
    }

    private void Update()
    {
        if (_isActive)
        {
            float rotation = CalculateRotation();
            Debug.Log(rotation);

            if (Mathf.Abs(rotation) > 0)
            {
                if (!_source.isPlaying)_source.Play();
            }
            else
            {
                if(_source.isPlaying) _source.Stop();
            }

            transform.Rotate(0, 0, rotation * Time.deltaTime, Space.Self);
        }

    }


    private float CalculateRotation()
    {
        float inputValue = -_action.ReadValue<Vector2>().x;

        float rotation = inputValue * (_rotateSpeed + 50 * _powerController.Value);

        return rotation;
    }

}
