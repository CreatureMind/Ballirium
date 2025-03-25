using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> sounds;
    public static AudioManager instance;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

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

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
        DontDestroyOnLoad(gameObject);
        PlaySound("BackroundMusic");
        PlaySound("Collect");
    }

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(delegate { MasterVolume(masterSlider.value); });
        musicSlider.onValueChanged.AddListener(delegate { MusicVolume(musicSlider.value); });
        SFXSlider.onValueChanged.AddListener(delegate { SFXVolume(SFXSlider.value); });

        // Set initial volumes
        masterVolume = masterSlider.value;
        musicVolume = musicSlider.value;
        sfxVolume = SFXSlider.value;

        ApplyVolumes();
    }

    public void PlaySound(string soundName)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == soundName)
            {
                s.audioSource.Play();
            }
        }
    }

    public void MasterVolume(float value)
    {
        masterVolume = value;
        ApplyVolumes();
    }

    public void MusicVolume(float value)
    {
        musicVolume = value;
        ApplyVolumes();
    }

    public void SFXVolume(float value)
    {
        sfxVolume = value;
        ApplyVolumes();
    }

    private void ApplyVolumes()
    {
        foreach (Sound s in sounds)
        {
            if (s.type == MyAudioType.Music)
            {
                s.audioSource.volume = musicVolume * masterVolume;
            }
            else if (s.type == MyAudioType.SFX)
            {
                s.audioSource.volume = sfxVolume * masterVolume;
            }
        }
    }
}