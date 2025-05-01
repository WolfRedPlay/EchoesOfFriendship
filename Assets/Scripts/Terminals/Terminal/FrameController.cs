using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public class FrameController : MonoBehaviour
{
    [SerializeField] float _speed = 2f;

    [SerializeField] PlayerInput _input;
    InputAction _action;

    AudioSource _audioSource;

    float _leftBorder;
    float _rightBorder;

    bool _isActive = false;

    public void SetPlayerInput(PlayerInput playerInput) 
    {  
        _input = playerInput;
        _action = _input.actions.FindAction("Navigate");
    }
    public void SetActive(bool active) 
    {  
        _isActive = active;
        if (!active) _audioSource.Stop();

        transform.position = new Vector3(_leftBorder, transform.position.y, transform.position.z);
    }

    
    void Start()
    {
        BoxCollider areaBox = transform.parent.GetComponent<BoxCollider>();
        if (areaBox == null)
        {
            Debug.LogError("Frame's parent object doesn't have BoxCollider!!!");
            enabled = false;
        }
        BoxCollider runeBox = GetComponent<BoxCollider>();

        _audioSource = GetComponent<AudioSource>();

        _rightBorder = areaBox.bounds.max.x - runeBox.bounds.extents.x;
        _leftBorder = areaBox.bounds.min.x + runeBox.bounds.extents.x;

        transform.position = new Vector3(_leftBorder, transform.position.y, transform.position.z);
    }


    void Update()
    {
        if (_isActive)
        {
            float inputValue = _action.ReadValue<Vector2>().x;

            Vector3 newPosition = transform.position;
            newPosition.x += inputValue * _speed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, _leftBorder, _rightBorder);

            if (newPosition != transform.position)
            {
                if (!_audioSource.isPlaying) _audioSource.Play();
            }
            else
            {
                if (_audioSource.isPlaying) _audioSource.Stop();
            }
            transform.position = newPosition;
        }
    }
}
