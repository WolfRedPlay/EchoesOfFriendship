using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignsList : MonoBehaviour
{
    [SerializeField] List<Texture> _signs;

    public List<Texture> Signs => new List<Texture>(_signs);

    public Texture GetRandomSign()
    {
        int index = Random.Range(0, _signs.Count);

        return _signs[index];
    }

}
