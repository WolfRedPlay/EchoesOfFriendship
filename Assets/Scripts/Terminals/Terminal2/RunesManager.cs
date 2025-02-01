using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RunesManager : MonoBehaviour
{
    List<Texture> _signs;
    List<DecalProjector> _decals;

    public List<DecalProjector> RunesDecals => _decals;

    private void Awake()
    {
        _signs = FindAnyObjectByType<SignsList>().Signs;

        if (_signs == null || _signs.Count == 0)
        {
            Debug.LogError("The _signs list is empty or not assigned in the inspector!");
            this.enabled = false;
            return;
        }

        _decals = transform.GetComponentsInChildren<DecalProjector>().ToList();
        if (_decals == null || _decals.Count == 0)
        {
            Debug.LogError("No DecalProjectors found!");
            this.enabled = false;
            return;
        }

        if (_signs.Count < _decals.Count)
        {
            Debug.LogWarning("Not enough signs for all DecalProjectors! Some projectors will not get a texture.");
        }
    }


    void Start()
    {
        for(int i = 0; i < _decals.Count; i++)
            _decals[i].material = new Material(_decals[i].material);

        CleanRunes();
    }


    public void CleanRunes()
    {
        for (int i = 0; i < _decals.Count; i++)
        {
            _decals[i].enabled = false;
        }
    }

    public void AssignSignsToRunes()
    {
        _signs.Shuffle();

        for (int i = 0; i < _decals.Count; i++)
        {
            if (_signs[i] == null)
            {
                Debug.LogError($"Sign at index {i} is null! Make sure all textures are assigned.");
                continue;
            }
            _decals[i].enabled = true;

            _decals[i].material.SetTexture("_EmissiveTexture", _signs[i]);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
