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
}