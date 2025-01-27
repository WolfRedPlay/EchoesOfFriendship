using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ProgressBarManager : MonoBehaviour
{
    [SerializeField] BoxCollider _checkBox;
    [SerializeField] float _leftmostXPosition = 1f;
    [SerializeField] float _speed = 0.1f;


    List<DecalProjector> _runesDecals;
    DecalProjector _currentSignDecal;
    float _value = 0f;


    const float MinScale = 0f;
    const float MaxScale = 1f;


    private void Start()
    {
        RuneSpaceController runeSpaceController = FindAnyObjectByType<RuneSpaceController>(FindObjectsInactive.Include);
        if (runeSpaceController == null) 
        {
            Debug.LogError("No Rune Space Controller found");
            this.enabled = false;
            return;
        }

        _currentSignDecal = runeSpaceController.Decal;


        RunesManager runesManager = FindAnyObjectByType<RunesManager>();
        if (runesManager == null)
        {
            Debug.LogError("No Rune Manager found");
            this.enabled = false;
            return;
        }

        _runesDecals = runesManager.RunesDecals;

        if (_checkBox == null)
        {
            Debug.LogError("Check box was not assigned in Inspector");
            this.enabled = false;
            return;
        }

    }

    private void Update()
    {
        if (CheckRune())
        {
            _value += _speed * Time.deltaTime;
        }
        else
        {
            _value -= _speed * Time.deltaTime;
        }

        _value = Mathf.Clamp01(_value);

        UpdateTransform();
    }


    private bool CheckRune()
    {
        Texture currentRune = _currentSignDecal.material.GetTexture("_EmissiveTexture");

        foreach (var rune in _runesDecals)
        {
            if (_checkBox.bounds.Contains(rune.transform.position))
            {
                Texture runeText = rune.material.GetTexture("_EmissiveTexture");
                
                if (runeText == currentRune)
                    return true;
            }

        }
        return false;
    }


    private void UpdateTransform()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Lerp(MinScale, MaxScale, _value);
        transform.localScale = scale;

        transform.localPosition = new Vector3(_leftmostXPosition - (_leftmostXPosition * transform.localScale.x), transform.localPosition.y, transform.localPosition.z);
    }

}
