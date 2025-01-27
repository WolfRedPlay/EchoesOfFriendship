using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RuneMovementController : MonoBehaviour
{
    [SerializeField] float _defaultSpeed = 0.5f;

    PowerController _power;


    float _leftBorder;
    float _rightBorder;
    float _nextPosition;
    float _minPossibleRadius;
    float _nextMoveTime;
    float _defaultMinDelay = 2f;
    float _defaultMaxDelay = 5f;

    const float PositionThreshold = .01f;

    private void Awake()
    {
        _power = FindAnyObjectByType<PowerController>(FindObjectsInactive.Include);
        if (_power == null)
        {
            Debug.LogError("Power source wasn't find!!!");
            enabled = false;
        }
    }


    private void Start()
    {
        BoxCollider areaBox = transform.parent.GetComponent<BoxCollider>();
        if (areaBox == null)
        {
            Debug.LogError("Rune's parent object doesn't have BoxCollider!!!");
            enabled = false;
        }
        BoxCollider runeBox = GetComponent<BoxCollider>();

        _rightBorder = areaBox.bounds.max.x - runeBox.bounds.extents.x;
        _leftBorder = areaBox.bounds.min.x + runeBox.bounds.extents.x;

        _minPossibleRadius = areaBox.bounds.extents.x / 2.5f + runeBox.bounds.extents.x;

        _nextPosition = GetNextPosition();
        _nextMoveTime = Time.time;
        StartCoroutine(MoveToNextPosition());
    }


    private void Update()
    {
        if (Time.time >= _nextMoveTime)
        {
            StartCoroutine(MoveToNextPosition());

            SetNextMoveTime();
        }
    }


    void SetNextMoveTime()
    {
        _nextMoveTime = Time.time + Random.Range(_defaultMinDelay - _power.Value, _defaultMaxDelay - 3 * _power.Value);
    }

    private float GetNextPosition()
    {
        float minX = transform.position.x - (_minPossibleRadius + _minPossibleRadius * (_power.Value));
        minX = Mathf.Clamp(minX, _leftBorder, _rightBorder);

        float maxX = transform.position.x + (_minPossibleRadius + _minPossibleRadius * (_power.Value));
        maxX = Mathf.Clamp(maxX, _leftBorder, _rightBorder);

        return Random.Range(minX, maxX);

    }



    IEnumerator MoveToNextPosition()
    {
        Vector3 targetPosition = new Vector3(_nextPosition, transform.position.y, transform.position.z);

        while (Vector3.Distance(transform.position, targetPosition) > PositionThreshold)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (_defaultSpeed + 2f * _power.Value) * Time.deltaTime);

            yield return null;
        }

        transform.position = targetPosition;

        _nextPosition = GetNextPosition();
    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            BoxCollider runeBox = GetComponent<BoxCollider>();
            Gizmos.color = Color.blue;
            float minX = transform.position.x - (_minPossibleRadius + _minPossibleRadius * (_power.Value));
            minX = Mathf.Clamp(minX, _leftBorder, _rightBorder);

            float maxX = transform.position.x + (_minPossibleRadius + _minPossibleRadius * (_power.Value));
            maxX = Mathf.Clamp(maxX, _leftBorder, _rightBorder);


            Vector3 firstPoint = new Vector3(minX, runeBox.bounds.min.y, runeBox.bounds.max.z);
            Vector3 secondPoint = new Vector3(minX, runeBox.bounds.max.y, runeBox.bounds.max.z);
            Vector3 thirdPoint = new Vector3(maxX, runeBox.bounds.max.y, runeBox.bounds.max.z);
            Vector3 fourthPoint = new Vector3(maxX, runeBox.bounds.min.y, runeBox.bounds.max.z);

            Gizmos.DrawLine(firstPoint, secondPoint);
            Gizmos.DrawLine(secondPoint, thirdPoint);
            Gizmos.DrawLine(thirdPoint, fourthPoint);
            Gizmos.DrawLine(fourthPoint, firstPoint);

            Gizmos.color = Color.red;

            Gizmos.DrawCube(new Vector3(_nextPosition, transform.position.y, transform.position.z), runeBox.bounds.extents);
        }
    }
}
