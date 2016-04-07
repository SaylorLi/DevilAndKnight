using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class Game : MonoBehaviour
{
    public static Game Ins;
    //
    //[HideInInspector]
    //public eGamePlatform platform;
    ////---
    //public VoidDelegate voidDelegateApplicationResume;
    //VoidDelegate voidDelegateApplicationPause;
    //[HideInInspector]
    //public string appVersionCode;
    [HideInInspector]
    //0 调试模式 1 内网服 2 外网测试服 3 外网验证服 4 外网正式服
    public int gameServer;
    ////
    List<BaseMediator> listUpdateOpen;
    //[HideInInspector]
    //public bool isRoleEntry;
    [HideInInspector]
    public TimeSystem timeSystem = new TimeSystem();
    ////Editor
    //public bool isSim = true;//模拟无服务器模式
    public bool isDebug;//出错打印log
    ////[HideInInspector]
    //public bool isSkipTutorial;
    [HideInInspector]
    public bool IsLAN;
    //[HideInInspector]
    public string Url;
    ////
    //public bool isTest;//
    public bool isEditorPrefab;//加载本地资源txt和prefab
    //public bool partnerAttack = true;
    //public bool monsterAttack = true;
    //public int WAVE_MAX;
    //[HideInInspector]
    //public int max_wave //最大波数
    //{
    //    get
    //    {
    //        return WAVE_MAX - RoleData.Ins.roleVo.treasureMaxWaveCut;
    //    }
    //}
    //public float Init_Gold_Value = 0.0f;
    //public int Init_Gold_Power = 0;
    //public int Init_Halidom = 0;
    //public int Init_Diamond = 0;
    //public bool isQuitSave = false;
    //bool isPause;
    [HideInInspector]
    public bool isBattle = false;

    internal void Publish()
    {
        //isPause = false;
        //isEditorPrefab = false;
        //monsterAttack = true;
        //partnerAttack = true;
        //isQuitSave = true;
        //WAVE_MAX = 10;
        //Init_Gold_Value = 0.0f;
        //Init_Gold_Power = 0;
        //Init_Halidom = 0;
        //Init_Diamond = 0;
        //if (isTest)
        //{
        //    Init_Gold_Value = 1f;
        //    Init_Gold_Power = 52;
        //}
    }
    void Awake()
    {
        Ins = this;
        //系统
        Application.runInBackground = true;
        Application.targetFrameRate = 30;
        //Screen.SetResolution(640, 960, false);
        DontDestroyOnLoad(gameObject);
        //Application.RegisterLogCallback(LogCallback);
        //
        Init();
        gameServer = 1;
        // 测试用
        Test();

        InitUpdateList();
    }
    public void Init()
    {
        SoundManager.Ins.InitBgm();
        MvcManager.Ins.Init();
        //voidDelegateApplicationPause = RoleData.Ins.SaveOnPauseTime;
        ////
        //RoleData.Ins.InitTime();
    }

    void OnApplicationPause(bool state)
    {
        //if (state)
        //{
        //    if (voidDelegateApplicationPause != null)
        //    {
        //        voidDelegateApplicationPause();
        //    }
        //    SoundManager.Ins.Pause();
        //    Application.targetFrameRate = 1;
        //}
        //else
        //{
        //    if (voidDelegateApplicationResume != null)
        //    {
        //        voidDelegateApplicationResume();
        //    }
        //    SoundManager.Ins.Resume();
        //    Application.targetFrameRate = 30;
        //}
    }

    void OnApplicationQuit()
    {
        //if (isQuitSave)
        //{
        //    LocalDataManager.Ins.SaveData();
        //}
    }

    void InitUpdateList()
    {
        listUpdateOpen = new List<BaseMediator>();
        //无角色
        listUpdateOpen.Add(MediatorManager.Ins.GetBaseMediator(StartMediator.NAME));
        listUpdateOpen.Add(MediatorManager.Ins.GetBaseMediator(LoadingMediator.NAME));
    }

    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Escape))
        //{
        //    if (isPause)
        //    {
        //        SetPause(false);
        //    }
        //    else
        //    {
        //        SetPause(true);
        //    }
        //    //
        //    if (Game.Ins.isDebug)
        //    {
        //        (MediatorManager.Ins.GetBaseMediator(FrontgroundMediator.NAME) as FrontgroundMediator).ShowF1();
        //    }
        //    if (MediatorManager.Ins.isBattle == false)
        //    {
        //        PlatformManager.Ins.Exit();
        //    }
        //}
        //if (isPause) { return; }
        float deltaTime = timeSystem.Update();
        TimerMgr.Ins.Update(deltaTime);
        //HttpManager.Ins.UpdateSelf();
        //Log.Log_hjx("Update"+ deltaTime);
        //
        try
        {
            //----------------ui
            BaseMediator baseMediator;
            for (int i = 0; i < listUpdateOpen.Count; i++)
            {
                baseMediator = listUpdateOpen[i];
                if (baseMediator.IsOpen)
                {
                    baseMediator.Update(deltaTime);
                }
            }
            //----------------------战斗
            if (MediatorManager.Ins.isBattle)
            {
                //FightScene.Ins.Update(deltaTime);
            }
        }
        catch (Exception e)
        {
            Log.LogError_sys("Update Exception! : " + e);
        }
    }

    //private void SetPause(bool v)
    //{
    //    Time.timeScale = v ? 0 : 1;
    //    isPause = v;
    //}

    //private void LogCallback(string errorString, string stackTrace, LogType type)
    //{
    //    if (type == LogType.Exception || type == LogType.Error)
    //    {
    //        string errors = "LogCallback 错误信息:" + errorString + "\n所在位置:" + stackTrace;
    //        Log.LogError_sys(errors);
    //    }
    //}
    ////

    //internal void Reset()
    //{
    //    isRoleEntry = false;
    //    voidDelegateApplicationResume = null;
    //}

    void Test()
    {
        // 测试 Mediator
        //MediatorManager.Ins.GetBaseMediator(TestMediator.NAME).Open();

        MediatorManager.Ins.GetBaseMediator(StartMediator.NAME).Open();
        // 测试 CSV config
        //Debug.Log(ConfigManager.Ins.Get<VersionConfigVo>(CSVFilter.resversion, "1").desc);
    }

    public void EntryBattle()
    {
        //PlatformManager.Ins.U2A_HiddeButton();
        SoundManager.Ins.bgmPlayer.StopAll();
        BundleManager.Ins.ReleaseAll(true);
        //SequenceManager.Ins.ChangeSequence(SequenceManager.Battle, true);
        SoundManager.Ins.bgmPlayer.Play(SoundConst.bgm_fight);
        isBattle = true;
    }

    public void ExitBattle(bool isForce = false)
    {
        //PlatformManager.Ins.U2A_ShowButton();
        isBattle = false;
        if (isForce)
        {
            //SequenceManager.Ins.ChangeSequence(SequenceManager.Main);
        }
    }
    ////
}

