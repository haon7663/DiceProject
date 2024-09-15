using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    public OptionDataContainer optionDataContainer;

    public AudioMixer audioMixer;
    
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = optionDataContainer.soundSettingsData.musicVolume;
        sfxSlider.value = optionDataContainer.soundSettingsData.sfxVolume;
        
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnVFXVolumeChanged);

        ApplySoundSettings();
    }

    private void OnMusicVolumeChanged(float value)
    {
        optionDataContainer.soundSettingsData.musicVolume = value;
        ApplySoundSettings();
        optionDataContainer.Save();
    }
    
    private void OnVFXVolumeChanged(float value)
    {
        optionDataContainer.soundSettingsData.sfxVolume = value;
        ApplySoundSettings();
        optionDataContainer.Save();
    }

    private void ApplySoundSettings()
    {
        var musicVolume = optionDataContainer.soundSettingsData.musicVolume;
        var musicMixerValue = musicVolume > 0.001f ? Mathf.Log10(musicVolume) * 20 : -80f;
        audioMixer.SetFloat("BGM", musicMixerValue);
        
        var sfxVolume = optionDataContainer.soundSettingsData.sfxVolume;
        var sfxMixerValue = sfxVolume > 0.001f ? Mathf.Log10(sfxVolume) * 20 : -80f;
        audioMixer.SetFloat("SFX", sfxMixerValue);
    }
}
