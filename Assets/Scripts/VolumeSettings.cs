using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeSettings : MonoBehaviour
{
    private Volume _volume;

    private float _vignetteIntensity;
    private float _vignetteSmoothness;
    private bool _colorAdjustmentsActive;
    private bool _channelMixerActive;
    private bool _chromaticAberrationActive;

    private void Awake()
    {
        _volume = GetComponent<Volume>();
        SetDefaultValues();
    }

    private void SetDefaultValues()
    {
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            _vignetteIntensity = vignette.intensity.value;
            _vignetteSmoothness = vignette.smoothness.value;
        }
        if(_volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            _colorAdjustmentsActive = colorAdjustments.active;
        if(_volume.profile.TryGet(out ChannelMixer channelMixer))
            _colorAdjustmentsActive = channelMixer.active;
        if(_volume.profile.TryGet(out ChromaticAberration chromaticAberration))
            _chromaticAberrationActive = chromaticAberration.active;
    }

    public void SetVolume()
    {
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.value = 0.5f;
            vignette.smoothness.value = 0.5f;
        }
        if(_volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            colorAdjustments.active = true;
        if(_volume.profile.TryGet(out ChannelMixer channelMixer))
            channelMixer.active = true;
        if(_volume.profile.TryGet(out ChromaticAberration chromaticAberration))
            chromaticAberration.active = true;
    }
    
    public void ResetVolume()
    {
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.value = _vignetteIntensity;
            vignette.smoothness.value = _vignetteSmoothness;
        }
        if(_volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            colorAdjustments.active = _colorAdjustmentsActive;
        if(_volume.profile.TryGet(out ChannelMixer channelMixer))
            channelMixer.active = _channelMixerActive;
        if(_volume.profile.TryGet(out ChromaticAberration chromaticAberration))
            chromaticAberration.active = _chromaticAberrationActive;
    }
}
