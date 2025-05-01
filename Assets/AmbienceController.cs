using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    AudioSource _audioSource;



    float _time = 0f;
    float _delay = 0f;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _delay = Random.Range(15f, 30f);
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _time += Time.deltaTime;

            if (_time >= _delay)
            {
                _time = 0f;
                _audioSource.Play();
                _delay = Random.Range(15f, 30f);
            }
        }
    }
}
