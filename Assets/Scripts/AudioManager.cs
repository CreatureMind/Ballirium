using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;
    public GameObject masterSlider;
    public GameObject musicSlider;
    public GameObject sfxSlider;
    public static AudioManager instance;

    public List<AudioSource> musicAudioSources = new List<AudioSource>();
    public List<AudioSource> sfxAudioSources = new List<AudioSource>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        PlaySound("Backroundmusic");
    }

    public void PlaySound(string soundName)
    {
        foreach (AudioSource source in musicAudioSources)
        {
            if (source.name == soundName)
            {
                source.Play();
            }
        }
        foreach (AudioSource source in sfxAudioSources)
        {
            if (source.name == soundName)
            {
                source.Play();
            }
        }
    }

    public void OnMasterSliderChanged(float value)
    {
        AudioListener.volume = value;
    }

    public void OnMusicSliderChanged(float value)
    {
        foreach (AudioSource source in musicAudioSources)
        {
            if (source != null)
                source.volume = value;
        }
    }
    public void OnSFXSliderChanged(float value)
    {
        foreach (AudioSource source in sfxAudioSources)
        {
            if (source != null)
                source.volume = value;
        }
    }
}
