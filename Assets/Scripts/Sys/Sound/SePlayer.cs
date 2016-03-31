using UnityEngine;
using System.Collections;

/// <summary>
/// 音效播放器
/// </summary>
public class SePlayer
{
    private bool isPause;
    private Transform tf;
    public AudioClip[] arrAc;
    private float _userSEVolume = 1f;

    public void Init(Transform transform)
    {
        tf = transform;
        isPause = false;
        // 预读取
        arrAc = Resources.LoadAll<AudioClip>("sound/se/");
    }

    AudioClip GetSE(string name)
    {
        if (arrAc == null) return null;
        int length = arrAc.Length;
        for (int i = 0; i < length; i++)
        {
            if (arrAc[i].name == name)
            {
                return arrAc[i];
            }
        }
        return null;
    }

    private AudioSource PlayAudioClip(AudioClip clip, Vector3 position, float volume, bool loop, float pitch)
    {
        if (clip == null)
        {
            return null;
        }
        GameObject go = new GameObject("One shot audio");
        go.transform.parent = tf;
        go.transform.localPosition = position;
        go.name = clip.name;
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.minDistance = 10000f;
        source.maxDistance = 100000f;
        source.pitch = pitch;
        source.Play();
        if (!loop)
        {
            Object.Destroy(go, clip.length);
        }
        return source;
    }

    public float PlaySE(string name)
    {
        return PlaySE(name, Vector3.zero, 1f, false, 1f);
    }

    float PlaySE(string name, Vector3 position, float volume, bool loop, float pitch)
    {
        float num = volume * _userSEVolume;
        if (num <= 0f)
        {
            return 0f;
        }
        AudioClip sE = GetSE(name);
        if (sE == null)
        {
            return 0f;
        }
        PlayAudioClip(sE, position, num, loop, pitch);
        return sE.length;
    }
    //-----------------------------
    void OperAll(bool isPause)
    {
        if (tf == null)
        {
            return;
        }
        int num = tf.childCount;
        Transform child = null;
        AudioSource component = null;
        for (int i = 0; i < num; i++)
        {
            child = tf.GetChild(i);
            if (child != null)
            {
                component = child.GetComponent<AudioSource>();
                if (isPause)
                {
                    component.Pause();
                }
                else
                {
                    component.Play();
                }
            }
        }
    }
    public void PauseAll()
    {
        OperAll(true);
    }

    public void ResumeAll()
    {
        OperAll(false);
    }
    public void Stop(string name)
    {
        int num = tf.childCount;
        Transform child = null;
        AudioSource component = null;
        for (int i = 0; i < num; i++)
        {
            child = tf.GetChild(i);
            if (child.name == name)
            {
                component = child.GetComponent<AudioSource>();
                component.Stop();
                Object.Destroy(child.gameObject);
                break;
            }
        }
    }

    public void StopAll()
    {
        int num = tf.childCount;
        Transform child = null;
        AudioSource component = null;
        for (int i = num - 1; i >= 0; i--)
        {
            child = tf.GetChild(i);
            //
            component = child.GetComponent<AudioSource>();
            component.Stop();
            Object.Destroy(child.gameObject);
        }
    }

    //-----------------------------
    public float UserSEVolume
    {
        set
        {
            _userSEVolume = value;
        }
        get
        {
            return _userSEVolume;
        }
    }
}
