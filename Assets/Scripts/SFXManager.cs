using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioSource _audio2;

    public void Play(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.Play();
    }

    public void Play2(AudioClip clip)
    {
        _audio2.clip = clip;
        _audio2.Play();
    }
}