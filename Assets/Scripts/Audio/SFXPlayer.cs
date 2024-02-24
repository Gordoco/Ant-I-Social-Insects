using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer
{
    private AudioSource _audioSource;

    public SFXPlayer(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }

    public void PlaySFX(SFX sfx)
    {
        _audioSource.PlayOneShot(sfx.Audio, sfx.Volume);
    }
}
