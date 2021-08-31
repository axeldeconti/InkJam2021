using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] private AudioSource _audio;

    private void Start()
    {
        DontDestroyOnLoad(transform.parent);
    }

    public void Play(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.Play();
    }

    public void Stop()
    {
        _audio.Stop();
    }

    public float Volume
    {
        get => _audio.volume;
        set => _audio.volume = Mathf.Clamp01(value);
    }
}