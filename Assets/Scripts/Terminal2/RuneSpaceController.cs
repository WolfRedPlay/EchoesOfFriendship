using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RuneSpaceController : MonoBehaviour
{
    SignsList _signs;
    [SerializeField] DecalProjector _decal;
    [SerializeField] float _defaultMinDelay = 5f;
    [SerializeField] float _defaultMaxDelay = 10f;

    PowerController _powerController;
    float _currentDelay = 0f;
    float _currentTime = 0f;
    DecalProjector _decalOnDiffTerminal;
    

    public DecalProjector Decal => _decal;

    private void Awake()
    {
        _signs = FindAnyObjectByType<SignsList>();
        if (_signs == null)
        {
            Debug.LogError("Not found signs list!");
            this.enabled = false;
            return;
        }

        if (_decal == null)
        {
            Debug.LogError("No DecalProjector found!");
            this.enabled = false;
            return;
        }

        _powerController = FindAnyObjectByType<PowerController>();
        if (_powerController == null)
        {
            Debug.LogError("No Power Controller found!");
            this.enabled = false;
            return;
        }


        RuneMovementController _runeMovementController = FindAnyObjectByType<RuneMovementController>(FindObjectsInactive.Include);
        if (_runeMovementController == null)
        {
            Debug.LogError("No Rune Movement Controller found!");
            this.enabled = false;
            return;
        }

        _decalOnDiffTerminal = _runeMovementController.GetComponentInChildren<DecalProjector>();
    }


    void Start()
    {
        



        _decal.material = new Material(_decal.material);
        _decalOnDiffTerminal.material = new Material(_decalOnDiffTerminal.material);
    }


    void SetRandomSign()
    {
        Texture newSign = _signs.GetRandomSign();

        _decal.material.SetTexture("_EmissiveTexture", newSign);
        _decalOnDiffTerminal.material.SetTexture("_EmissiveTexture", newSign);
    }

    void Update()
    {
       if (Mathf.Approximately(_currentDelay, _currentTime) || _currentTime > _currentDelay)
       {
            RestartDelay();
            SetRandomSign();
       }
       else
            _currentTime += Time.deltaTime;
       
    }


    public void RestartDelay()
    {
        _currentTime = 0f;
        _currentDelay = Random.Range(_defaultMinDelay + 5f * _powerController.Value, _defaultMaxDelay + 5f * _powerController.Value);
    }
}
