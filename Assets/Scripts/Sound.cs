using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyAudioType
{
    Music,
    SFX
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public MyAudioType type;

    [Range(0f, 1f)] public float volume = 1;
    [Range(-3f, 3f)] public float pitch = 1;
    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;
}