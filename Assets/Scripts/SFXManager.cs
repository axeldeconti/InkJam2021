using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] private AudioSource _audio;

    public void Play(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.Play();
    }
}