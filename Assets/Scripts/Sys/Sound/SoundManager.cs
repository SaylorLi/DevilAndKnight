using UnityEngine;
using System.Collections;
using System;

public class SoundManager
{
    internal static readonly SoundManager Ins = new SoundManager();

    internal BgmPlayer bgmPlayer = new BgmPlayer();
    internal readonly SePlayer sePlayer = new SePlayer();
   
    internal void InitBgm()
    {
        Transform transform = Game.Ins.transform;
        GameObject go;
        //
        go = new GameObject();
        go.name = "BgmPlayer";
        go.transform.parent = transform;
        bgmPlayer.Init(go.transform);
    }
    internal void InitSe()
    {
        Transform transform = Game.Ins.transform;
        GameObject go;
        //
        go = new GameObject();
        go.name = "SePlayer";
        go.transform.parent = transform;
        sePlayer.Init(go.transform);
    }
    internal void Pause()
    {
        sePlayer.PauseAll();
        bgmPlayer.PauseAll();
    }

    internal void Resume()
    {
        sePlayer.ResumeAll();
        bgmPlayer.ResumeAll();
    }
}
