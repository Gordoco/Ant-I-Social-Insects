using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SFX", order = 1)]
public class SFX : ScriptableObject
{
    [SerializeField] private AudioClip _audioClip;

    [SerializeField] [Range(0.0f, 1.0f)] private float _volume;
    public AudioClip Audio => _audioClip;
    public float Volume => _volume;
}
