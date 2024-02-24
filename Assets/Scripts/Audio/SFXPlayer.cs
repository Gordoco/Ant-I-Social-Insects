using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private static AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public static void PlaySFX(SFX sfx)
    {
        _audioSource.PlayOneShot(sfx.Audio, sfx.Volume);
    }
}
