using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum Sound
{
    Bgm,
    Effect,
    MaxCount,
}

public class SoundManager : SingletonDontDestroyOnLoad<SoundManager>
{
    [SerializeField] private AudioSource[] audioSources = new AudioSource[(int)Sound.MaxCount];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private string _saveBGM;

    private void Start()
    {
        FindBGM();
    }

    private void FindBGM()
    {
        var saveBGM = _saveBGM;
        if (SceneManager.GetActiveScene().name == "Battle")
        {
            switch (GameManager.Inst.currentNodeType)
            {
                case NodeType.Boss:
                    _saveBGM = "BossBGM";
                    break;
                default:
                    _saveBGM = "BattleBGM";
                    break;
            }
        }
        else
        {
            _saveBGM = "LobbyBGM";
        }
        
        print(_saveBGM);
        
        if (saveBGM != _saveBGM)
            Play(_saveBGM, Sound.Bgm);
    }

    public void Play(AudioClip audioClip, Sound type = Sound.Effect)
    {
        if (audioClip == null)
            return;

        if (type == Sound.Bgm) 
        {
            AudioSource audioSource = audioSources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();
            
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = audioSources[(int)Sound.Effect];
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string path, Sound type = Sound.Effect)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type);
    }

    private AudioClip GetOrAddAudioClip(string path, Sound type = Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Sound.Bgm)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindBGM();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}