using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] List<AudioClip> _walkingSounds;
    [SerializeField] List<AudioClip> _jumpStart;
    [SerializeField] List<AudioClip> _jumpFinish;
    AudioSource _audioSource;

    bool _isWalking;
    bool _pause = false;
    float timer = 0f;
    float _delay = .2f;

    public void SetWalking(bool walking) {  _isWalking = walking; }
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpStart()
    {
        _audioSource.volume = .6f;
        _audioSource.clip = _jumpStart[Random.Range(0, _jumpStart.Count)];
        _audioSource.Play();
    }
    
    public void PlayJumpFinish()
    {
        _audioSource.volume = .25f;

        _audioSource.clip = _jumpFinish[Random.Range(0, _jumpFinish.Count)];
        _audioSource.Play();
    }


    private void Update()
    {
        if (_isWalking)
        {
            if (!_audioSource.isPlaying && !_pause)
            {
                _pause = true;
                timer = 0f;
            }

            if (_pause)
            {
                timer += Time.deltaTime;

                if (timer > _delay)
                {
                    _pause = false;
                    _audioSource.clip = _walkingSounds[Random.Range(0, _walkingSounds.Count)];
                    _audioSource.Play();
                }
            }
        }
    }
}
