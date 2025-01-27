using UnityEngine;
using UnityEngine.Events;

public class StabilityManager : MonoBehaviour
{
    [Tooltip("Rate of value changing")]
    [SerializeField] float _speed = 0.1f;

    [SerializeField] float _leftmostXPosition = .4f;

    public UnityEvent OnStability0;


    BoxCollider _frameBox;
    Transform _runeTransform;
    float _value = 1f;


    const float MinScale = 0f;
    const float MaxScale = 1f;


    private void Awake()
    {
        _frameBox = FindAnyObjectByType<FrameController>().GetComponent<BoxCollider>();
        if(_frameBox == null)
        {
            Debug.LogError("Could not find frame box!!!");
            enabled = false;
        }

        _runeTransform = FindAnyObjectByType<RuneMovementController>().transform;
        if(_runeTransform == null)
        {
            Debug.LogError("Could not find Rune!!!");
            enabled = false;
        }
    }



    private void Update()
    {
        CalculateValue();

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Lerp(MinScale, MaxScale, _value);
        transform.localScale = scale;

        transform.localPosition = new Vector3(_leftmostXPosition - (_leftmostXPosition * _value), transform.localPosition.y, transform.localPosition.z);

        if (_value == 0f)
        {
            OnStability0?.Invoke();
        }
    }

    private void CalculateValue()
    {
        if (CheckRunePosition())
            _value += _speed * Time.deltaTime;
        else
            _value -= _speed * Time.deltaTime;

        _value = Mathf.Clamp01(_value);
    }

    private bool CheckRunePosition()
    {
        if (_frameBox.bounds.Contains(_runeTransform.position))
            return true;
        else 
            return false;
    }
}
