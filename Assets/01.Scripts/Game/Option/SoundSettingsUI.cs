using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public Slider musicSlider;
    public Slider sfxSlider;

    private OptionDataContainer _optionDataContainer;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => OptionDataContainer.Inst.soundSettingsData != null);

        _optionDataContainer = OptionDataContainer.Inst;
        
        musicSlider.value = _optionDataContainer.soundSettingsData.musicVolume;
        sfxSlider.value = _optionDataContainer.soundSettingsData.sfxVolume;
        
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnVFXVolumeChanged);

        ApplySoundSettings();
    }

    private void OnMusicVolumeChanged(float value)
    {
        _optionDataContainer.soundSettingsData.musicVolume = value;
        ApplySoundSettings();
        _optionDataContainer.Save();
    }
    
    private void OnVFXVolumeChanged(float value)
    {
        _optionDataContainer.soundSettingsData.sfxVolume = value;
        ApplySoundSettings();
        _optionDataContainer.Save();
    }

    private void ApplySoundSettings()
    {
        var musicVolume = _optionDataContainer.soundSettingsData.musicVolume;
        var musicMixerValue = musicVolume > 0.001f ? Mathf.Log10(musicVolume) * 20 : -80f;
        audioMixer.SetFloat("BGM", musicMixerValue);
        
        var sfxVolume = _optionDataContainer.soundSettingsData.sfxVolume;
        var sfxMixerValue = sfxVolume > 0.001f ? Mathf.Log10(sfxVolume) * 20 : -80f;
        audioMixer.SetFloat("SFX", sfxMixerValue);
    }
}
