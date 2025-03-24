using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound : MonoBehaviour
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1;
    [Range(1f, 3f)] public float pitch = 1;
    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;
}
