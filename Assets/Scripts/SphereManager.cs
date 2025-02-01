using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 2f;


    void Update()
    {
        transform.Rotate(Vector3.one * _rotationSpeed * Time.deltaTime);
    }
}
