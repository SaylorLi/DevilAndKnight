using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MediatorManager
{
    public static readonly MediatorManager Ins = new MediatorManager();
    //
    private const int Z_OFFSET = 10;
    private const int D_OFFSET = 100;//UI之间深度最低差像素,保证UI不会穿插
    //
    private int curMaxDepthOffset = 0;
    private Dictionary<string, BaseMediator> dicBaseMediator = new Dictionary<string, BaseMediator>();
    public bool isBattle;
    //------------
    public void AddBaseMediator(BaseMediator baseMediator, string name)
    {
        if (!dicBaseMediator.ContainsKey(name))
        {
            dicBaseMediator.Add(name, baseMediator);
        }
    }
    public T GetBaseMediator<T>(string winName) where T : BaseMediator
    {
        return GetBaseMediator(winName) as T;
    }
    public BaseMediator GetBaseMediator(string winName)
    {
        BaseMediator bm = null;
        dicBaseMediator.TryGetValue(winName, out bm);
        return bm;
    }

    public void DestoryAllWin()
    {
        foreach (BaseMediator bm in dicBaseMediator.Values)
        {
            if (bm.isNoOpen == false)
            {
                bm.DestoryWin();
            }
        }
    }
    /// <summary>
    /// 重排深度,互斥窗口
    /// </summary>
    public void OpenWin(BaseMediator bm)
    {
        WindowLayoutManager.Ins.ExclusionGroup(bm);
        if (bm.IsFixed)
        {
            return;
        }
        curMaxDepthOffset += D_OFFSET;
        //Log.Log_hjx("curMaxDepthOffset " + curMaxDepthOffset);
        bm.SetDepthOffset(curMaxDepthOffset);
        Vector3 v = bm.Position;
        bm.Position = new Vector3(v.x, v.y, -curMaxDepthOffset / Z_OFFSET);
    }

    //public void OpenExclusion(BaseMediator bm)
    //{
    //	for (int i = 0; i < list_frame.Count; i++)
    //	{
    //		if (bm != GetBaseMediator(list_frame[i]) && GetBaseMediator(list_frame[i]).IsOpen)
    //		{
    //			GetBaseMediator(list_frame[i]).Close();
    //		}
    //	}
    //}
    /// <summary>
    /// 重排深度
    /// </summary>
    public void CloseWin(BaseMediator bm)
    {
        if (bm.IsFixed)
        {
            return;
        }
        int depthOffset = bm.GetDepthOffset();
        BaseMediator baseMediator;
        foreach (KeyValuePair<string, BaseMediator> kvp in dicBaseMediator)
        {
            baseMediator = kvp.Value;
            if (!baseMediator.IsOpen)
            {
                continue;
            }
            if (baseMediator.IsFixed)
            {
                continue;
            }
            int baseMediatorDepthOffset = baseMediator.GetDepthOffset();
            //Log.Log_hjx("baseMediatorDepthOffset " + baseMediatorDepthOffset + "  depthOffset" + depthOffset);
            if (baseMediatorDepthOffset > depthOffset)
            {
                baseMediator.SetDepthOffset(-D_OFFSET);
            }
            //Log.Log_hjx("baseMediatorDepthOffset " + baseMediatorDepthOffset + "  depthOffset" + depthOffset);
        }
        //
        curMaxDepthOffset -= D_OFFSET;
        bm.SetDepthOffset(-bm.GetDepthOffset());
        Vector3 v = bm.Position;
        bm.Position = new Vector3(v.x, v.y, 0);
        //Log.Log_hjx("close curMaxDepthOffset" + curMaxDepthOffset);
    }

    //public void BringToFont(BaseMediator bm)
    //{
    //if (bm.IsFixed)
    //{
    //    return;
    //}

    //float fDepth = bm.Depth;
    //foreach (KeyValuePair<string, BaseMediator> kvp in dicBaseMediator)
    //{
    //    if (!kvp.Value.IsOpen)
    //    {
    //        continue;
    //    }
    //    if (kvp.Value.IsFixed)
    //    {
    //        continue;
    //    }
    //    if (kvp.Value.Depth > fDepth)
    //    {
    //        kvp.Value.Depth -= OFFSET;
    //    }
    //}

    //bm.Depth = m_fCurrDepth;
    //}
    //--------------------------------------------------------------
    public void CloseMainUi()
    {
        //Log.Log_hjx("CloseMainUi");
        //GetBaseMediator(MenuMediator.NAME).Close();
    }

    //internal void RoleEntry()
    //{
    //    GetBaseMediator(StartMediator.NAME).Close();
    //    GetBaseMediator(LoginMediator.NAME).Close();
    //    //DataManager.Ins.GetBaseData<ServerData>().RoleEntry();
    //    OpenMainUi();
    //    GetBaseMediator(HunterMenuMediator.NAME).Open();
    //    GetBaseMediator(HunterFightMediator.NAME).Open();
    //    EntryBattle();
    //}

    //public void OpenMainUi()
    //{
    //    //Log.Log_hjx("OpenMainUi");
    //    //GetBaseMediator(MenuMediator.NAME).Open();
    //}
    ////---
    //public void EntryBattle()
    //{
    //    PlatformManager.Ins.U2A_HiddeButton();
    //    SoundManager.Ins.bgmPlayer.StopAll();
    //    BundleManager.Ins.ReleaseAll(true);
    //    SequenceManager.Ins.ChangeSequence(SequenceManager.Battle, true);
    //    SoundManager.Ins.bgmPlayer.Play(SoundConst.bgm_fight);
    //    isBattle = true;
    //}

    //public void ExitBattle(bool isForce = false)
    //{
    //    PlatformManager.Ins.U2A_ShowButton();
    //    isBattle = false;
    //    //FightScene.Ins.Clear();
    //    //
    //    if (isForce)
    //    {
    //        SequenceManager.Ins.ChangeSequence(SequenceManager.Main);
    //    }
    //    else
    //    {
    //        //SoundManager.GetBgmPlayer().Play(SoundConstants.BGM_MAIN);
    //    }
    //}
    ////
    public void OpenLoading(eLoadingMediatorType type, VoidDelegate voidDelegate)
    {
        LoadingMediator loadingMediator = GetBaseMediator(LoadingMediator.NAME) as LoadingMediator;
        loadingMediator.OpenLoading(type, voidDelegate);
    }
    ////
    ///// <summary>
    ///// 一个确定按钮 无回调
    ///// </summary>
    //public void OpenPopUpMediator(string content)
    //{
    //    PopUpMediator popUpMediator = MediatorManager.Ins.GetBaseMediator(PopUpMediator.NAME) as PopUpMediator;
    //    PopUpVo vo = new PopUpVo();
    //    vo.content = content;
    //    vo.title = "提示";
    //    vo.OkDelegate = popUpMediator.CloseWin;
    //    popUpMediator.OpenPupUp(vo);
    //}
    ///// <summary>
    ///// 两个按钮 有回调
    ///// </summary>
    //public void OpenPopUpMediatorTwo(string content, VoidDelegate mSureDelegate)
    //{
    //    PopUpMediator popUpMediator = MediatorManager.Ins.GetBaseMediator(PopUpMediator.NAME) as PopUpMediator;
    //    PopUpVo vo = new PopUpVo();
    //    vo.content = content;
    //    vo.title = "提示";
    //    vo.CanelDelegate = popUpMediator.CloseWin;
    //    vo.mSureDelegate = mSureDelegate;
    //    popUpMediator.OpenPupUp(vo);
    //}
    ///// <summary>
    ///// 一个确定按钮 有回调
    ///// </summary>
    //public void OpenPopUpMediatorOne(string content, VoidDelegate mSureDelegate)
    //{
    //    PopUpMediator popUpMediator = MediatorManager.Ins.GetBaseMediator(PopUpMediator.NAME) as PopUpMediator;
    //    PopUpVo vo = new PopUpVo();
    //    vo.content = content;
    //    vo.title = "提示";
    //    vo.OkDelegate = mSureDelegate;
    //    popUpMediator.OpenPupUp(vo);
    //}

    //public void OpenPopUpMediatorTwo(string content, VoidDelegate mCancelDelegate, VoidDelegate mSureDelegate)
    //{
    //    PopUpMediator popUpMediator = MediatorManager.Ins.GetBaseMediator(PopUpMediator.NAME) as PopUpMediator;
    //    PopUpVo vo = new PopUpVo();
    //    vo.content = content;
    //    vo.title = "提示";
    //    vo.CanelDelegate = mCancelDelegate;
    //    vo.mSureDelegate = mSureDelegate;
    //    popUpMediator.OpenPupUp(vo);
    //}
    //public void TutorialProcess()
    //{
    //    //Log.Log_hjx("TutorialProcess ");
    //    if (TutorialData.Ins.eStep == TutorialStep.IntroDrawCard1_save
    //        || TutorialData.Ins.eStep == TutorialStep.IntroCompose0
    //        || TutorialData.Ins.eStep == TutorialStep.IntroCastle_after2
    //        || TutorialData.Ins.eStep == TutorialStep.IntroFreeQuest1
    //            || TutorialData.Ins.eStep == TutorialStep.Clean1
    //        )
    //    {
    //        UpdateTutorialMediator();
    //    }
    //}
    //internal void UpdateTutorialMediator()
    //{
    //    //Log.Log_hjx("UpdateTutorialMediator");
    //    MediatorManager.Ins.GetBaseMediator(TutorialMediator.NAME).Open();
    //}
    //internal void OpenTipsMediator(string str)
    //{
    //    AutoTipsMediator mediator = GetBaseMediator<AutoTipsMediator>(AutoTipsMediator.NAME);
    //    mediator.OpenTips(str);
    //}

}
