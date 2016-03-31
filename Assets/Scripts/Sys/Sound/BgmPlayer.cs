using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
///音乐播放器
/// </summary>
public class BgmPlayer
{
    Dictionary<string, AudioClip> dic = new Dictionary<string, AudioClip>();
    private float _currentVolume = 1f;
    private float _userBGMVolume = 1f;
    //private bool _stop;
    private AudioSource audioIntro;
    private AudioSource audioMain;
    private Transform tf;
    public void Init(Transform transform)
    {
        tf = transform;
        //
        GameObject go;
        //
        go = new GameObject();
        go.name = "IntroAudioSource";
        go.transform.parent = transform;
        audioIntro = go.AddComponent<AudioSource>();
        audioIntro.playOnAwake = false;
        //
        go = new GameObject();
        go.name = "MainAudioSource";
        go.transform.parent = transform;
        audioMain = go.AddComponent<AudioSource>();
        audioMain.playOnAwake = false;
    }
    bool IsPlaying(string name, AudioSource audio)
    {
        return (audio.isPlaying && (audio.clip != null) && (audio.clip.name == name));
    }

    public void SetLoop(bool loop)
    {
        audioMain.loop = loop;
    }

    public void PauseAll()
    {
        audioMain.Pause();
        audioIntro.Pause();
    }
    public void StopAll()
    {
        audioMain.Stop();
        audioIntro.Stop();
    }
    public void ResumeAll()
    {
        audioMain.Play();
        audioIntro.Play();
    }

    public void Play(string fileName)
    {
        Play(string.Empty, fileName);
    }

    public void Play(string introName, string mainName)
    {
        if (IsPlaying(introName, audioIntro) || IsPlaying(mainName, audioMain))
        {
            return;
        }
        StopAll();

        AudioClip introClip = GetClip(introName);
        if (introClip == null)
        {
            introClip = LoadManager.Ins.LoadResourcesAudioClip(introName);
        }
        //
        AudioClip mainClip = GetClip(mainName);
        if (mainClip == null)
        {
            mainClip = LoadManager.Ins.LoadResourcesAudioClip(mainName);
        }
        //
        Play(introClip, mainClip);
    }

    private AudioClip GetClip(string name)
    {
        if (dic.ContainsKey(name))
        {
            return dic[name];
        }
        return null;
    }

    void Play(AudioClip introClip, AudioClip mainClip)
    {
        if (mainClip != null)
        {
            audioMain.clip = mainClip;
            audioMain.loop = true;
            if (introClip != null)
            {
                audioIntro.loop = false;
                audioIntro.clip = introClip;
                audioIntro.Play();
                audioMain.PlayDelayed(audioIntro.clip.length);
            }
            else
            {
                audioMain.Play();
            }
        }
    }

    public float UserBGMVolume
    {
        set
        {
            _userBGMVolume = value;
            SetVolume(_currentVolume);
        }
        get
        {
            return _userBGMVolume;
        }
    }
    public void SetVolume(float val)
    {
        audioMain.volume = _userBGMVolume * val;
        audioIntro.volume = _userBGMVolume * val;
        _currentVolume = val;
    }

    public void Release()
    {
        StopAll();
        audioMain.clip = null;
        audioIntro.clip = null;
    }

}
