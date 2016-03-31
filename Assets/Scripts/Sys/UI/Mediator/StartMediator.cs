using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class StartMediator : BaseMediator
{
    public const string NAME = "StartMediator";
    //
    private CInput inputIp;
    private CCheckbox cbLan;
    private CGameObject go_Loading;
    private CButton btn_Touch;
    private CProgressBar pb_LoadingBar;

    private float counter = 0f;
    private bool isDynimic;
    private CLabel lbl_ver;
    private CGameObject goDebug;
    private CSprite sp_bg;
    private string strServerName = string.Empty;

    public override void LoadWin()
    {
        LoadManager.Ins.LoadResourcesPanel("panel/panel_start", LoadWinComplete);
        //LoadWinComplete(NAME, UIRootManager.Ins.goAnchor.transform.Find("panel_start").gameObject);
    }
    protected override void FirstOpen()
    {
        IsFixed = true;
        //anim = panel.Go.GetComponent<Animation>();
        go_Loading = panel.GetCom("(GameObject)go_Loading");
        btn_Touch = panel.GetCom("(Button)btn_Touch") as CButton;
        btn_Touch.SetClickDelegate(ClickDelegateTouchScreen);
        btn_Touch.SetStringSound(SoundConst.SE_FIX);
        pb_LoadingBar = panel.GetCom("(ProgressBar)pb_LoadingBar") as CProgressBar;
        //
        cbLan = panel.GetCom("(Checkbox)remeber") as CCheckbox;
        cbLan.SetStateChange(StateChangeCbLan);
        lbl_ver = panel.GetCom("(Label)ver") as CLabel;
        //
        sp_bg = panel.GetCom("(Sprite)sp_Bg") as CSprite;
        //
        LoadManager.Ins.LoadResourcesAtlas(PathUtil.GetStartBgRelativePath("ui_start_newbg"), LoadDelegateNewStartGlow);
        //
        inputIp = panel.GetCom("(Input)ip") as CInput;
        goDebug = panel.GetCom("(GameObject)debug");
        //
        goDebug.SetActive(false);
        //0 调试模式 1 内网服 2 外网测试服 3 外网验证服 4 外网正式服 5商务服
        int gameServer = Game.Ins.gameServer;

        if (gameServer == 0)
        {
            strServerName = "调试模式";
        }
        else if (gameServer == 1)
        {
            cbLan.IsChecked = true;
            StateChangeCbLan(null, true);
            strServerName = "内网服";
        }
        else if (gameServer == 2)
        {
            inputIp.Text = Game.Ins.Url = IpConfig.NET_TEST;
            strServerName = "外网测试服";
        }
        else if (gameServer == 4)
        {
            inputIp.Text = Game.Ins.Url = IpConfig.NET;
            //str = "外网正式服";
        }
        else if (gameServer == 5)
        {
            inputIp.Text = Game.Ins.Url = IpConfig.NET_SW;
            strServerName = "外网商务服";
        }
        //
        //Game.Ins.platform = eGamePlatform.UC;
        //lbl_ver.Text = strServerName + " 游戏版本:" + PlatformManager.Ins.GetAppVersionName();
    }

    private void LoadDelegateNewStartGlow(string interim, UnityEngine.Object asset)
    {
        if (asset == null)
        {
            return;
        }
        UIAtlas atlas = (asset as GameObject).GetComponent<UIAtlas>();
        sp_bg.uiSprite.atlas = atlas;
    }

    private void DownloadRetry(LoadRequestVo obj)
    {
        string str = "文件:" + obj.name + "下载失败!可能是网络问题";
        PopUpMediator popUpMediator = MediatorManager.Ins.GetBaseMediator(PopUpMediator.NAME) as PopUpMediator;
        PopUpVo vo = new PopUpVo();
        vo.content = str;
        vo.mSureTitle = "重试";
        vo.OkDelegate = BundleManager.Ins.RetryAfterError;
        popUpMediator.OpenPupUp(vo);
    }

    private void DownloadTimeout(LoadRequestVo req)
    {
        string str = "文件:" + req.name + "下载超时!可能是网络问题";
        PopUpMediator popUpMediator = MediatorManager.Ins.GetBaseMediator(PopUpMediator.NAME) as PopUpMediator;
        PopUpVo vo = new PopUpVo();
        vo.content = str;
        vo.mSureTitle = "重试";
        vo.OkDelegate = BundleManager.Ins.RetryAfterTimeout;
        popUpMediator.OpenPupUp(vo);
    }

    private void StateChangeCbLan(CCheckbox checkbox, bool state)
    {
        Game.Ins.IsLAN = state;
        if (state)
        {
            inputIp.Text = Game.Ins.Url = IpConfig.LAN;
        }
        else
        {
            inputIp.Text = Game.Ins.Url = IpConfig.NET_TEST;
        }
        //Log.Log_hjx("inputIp.TextinputIp.TextinputIp.Text " + inputIp.Text);
    }
    public override void UpdateWin()
    {
        ShowBg(true);
        btn_Touch.SetActive(false);
        go_Loading.SetActive(false);
        //
        SoundManager.Ins.bgmPlayer.Play(SoundConst.bgm_start);
        isDynimic = true;
    }



    private void ClickDelegateTouchScreen(CButton source)
    {
        //Log.Log_hjx("go_Loading.Go.activeInHierarchy " + go_Loading.Go.activeInHierarchy);
        if (go_Loading.Go.activeInHierarchy == false)
        {
            btn_Touch.SetActive(false);
            //if (Game.Ins.isSim == false)
            //{
            //    StartData.Ins.GetKey();
            //    ReceiveManager.Ins.GetReceive<GetAppKeyReceive>(Protocol.app_key).receiveDelegate = OnGetKey;
            //}
            //else
            //{
                OnGetKey();
            //}
        } 
    }

    private void OnGetKey()
    {
        //Log.Log_hjx("Game.Ins.Url" + Game.Ins.Url);
        Game.Ins.Url = inputIp.Text;
        //PlatformManagerUmeng.Ins.LoadStart();
        go_Loading.SetActive(true);
        ShowProgress(0);
        //goDebug.SetActive(false);
        //GetAppVersion();
        //linshi
        MvcManager.Ins.LoadAllConfig();
    }


    ///// <summary>
    ///// 取得服务器反馈的版本信息
    ///// </summary>
    //public void GetAppVersion()
    //{
    //    StartData.Ins.GetAppVersion();
    //}

    //// 获取逻辑服务器反馈的版本信息回调
    //public void OnAppVersionCallback()
    //{
    //    //上报
    //    //PlatformManagerUmeng.Ins.Init();
    //    //
    //    // 检测APK是否需要升级
    //    BundleManager.Ins.OnRetry += DownloadRetry;
    //    BundleManager.Ins.OnTimeout += DownloadTimeout;
    //    //APK自动升级
    //    if (Application.platform == RuntimePlatform.Android
    //        //|| Application.platform == RuntimePlatform.WindowsEditor
    //        )
    //    {
    //        int curCode = int.Parse(Game.Ins.appVersionCode);
    //        if (curCode < StartData.Ins.minVersion)
    //        {
    //            ReceiveManager.Ins.AppVersionLow();
    //            return;
    //        }
    //    }

    //    if (Game.Ins.isEditorPrefab)
    //    {
    //        MvcManager.Ins.LoadAllConfig();
    //    }
    //    else
    //    {
    //        // 获取服务器上的files.sxd的MD5 Hash
    //        BundleManager.Ins.ForceDownload("files-hash", OnFilesHashCallback);
    //    }
    //}

    private void OnFilesHashCallback(string assetName, string hash)
    {
        // 如果本地的files-server.txt存在并且Hash值和hash一样就不需要下载,否则都需要下载
        string filesServerLocalPath = BundleManager.pathLocal + "files-server.txt";
        if (File.Exists(filesServerLocalPath))
        {
            string filesServerLocalText = File.ReadAllText(filesServerLocalPath);
            string h = CalcMD5.CheckFile(filesServerLocalPath);
            if (h == hash)
            {
                HashOver(hash);
                return;
            }
        }
        BundleManager.Ins.ForceDownload("files", OnFilesCallback);
    }


    private void OnFilesCallback(string assetName, UnityEngine.Object textObject)
    {
        string text = (textObject as TextAsset).text;

        // 写入本地
        FileStream fs = null;
        try
        {
            string filesServerLocalPath = BundleManager.pathLocal + "files-server.txt";
            {
                string dir = Path.GetDirectoryName(filesServerLocalPath);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            }
            fs = new FileStream(filesServerLocalPath, FileMode.Create, FileAccess.Write, FileShare.Read, 2048);
            StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
            writer.Write(text);
            writer.Flush();
            writer.Close();
        }
        catch
        {
            if (fs != null) fs.Close();
        }

        HashOver(text);
    }

    private void HashOver(string text)
    {
        // 初始化到HashManager
        HashManager.ProcessServerHash(text);
        BundleManager.Ins.ReleaseAll(false);
        MvcManager.Ins.LoadAllConfig();
    }

    public override void Update(float deltaTime)
    {
        if (isDynimic)
        {
            if (counter > 0)
            {
                counter -= deltaTime;
            }
            else
            {
                isDynimic = false;
                btn_Touch.SetActive(true);
            }
        }
    }

    public void ShowProgress(float persent)
    {
        //Log.Log_hjx("persent "+ persent);
        pb_LoadingBar.SetValue(persent);

        if (persent == 1.00f)
        {
            //Close();
            ShowBg(false);
            //PlatformManager.Ins.LoginSdk();
            //LocalDataManager.Ins.InitData();
        }
    }
    void ShowBg(bool isShow)
    {
        btn_Touch.SetActive(isShow);
        go_Loading.SetActive(isShow);
    }

    internal void ShowVersion()
    {
        int c = ConfigManager.Ins.GetDic(CSVFilter.resversion).Count;
        //Log.Log_hjx("ccccccc " + c);
        VersionConfigVo vo = ConfigManager.Ins.Get<VersionConfigVo>(CSVFilter.resversion, c.ToString());
        if (vo != null)
        {
            lbl_ver.Text = strServerName + " 游戏版本:" /*+ PlatformManager.Ins.GetAppVersionName()*/ + "\n" + "配置表版本:" + vo.version;
        }
    }

    protected override void UnloadDynimicDownload()
    {
        sp_bg.uiSprite.atlas = null;
    }


}

