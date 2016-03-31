using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//
public delegate void VoidDelegate();
public class MvcManager
{
    public static readonly MvcManager Ins = new MvcManager();
    private int numConfig;
    private float numMax;
    private List<string> listLoaded;
    private bool IsFirst = true;

    public void LoadAllConfig()
    {
        //加载配置表
        listLoaded = CSVFilter.GetConfigList();
        numMax = numConfig = listLoaded.Count;
        if (Game.Ins.isEditorPrefab)
        {
            foreach (string name in listLoaded)
            {
                LoadConfigTxt(name);
            }
        }
        else
        {
            foreach (string name in listLoaded)
            {
                LoadConfigBundle(PathUtil.GetConfigRelativePath(name), LoadCompletedBundle);
            }
        }
    }

    private void LoadConfigTxt(string name)
    {
        string fullPath = "file:///" + Application.dataPath + "/config/txt/" + name + ".txt";
        Game.Ins.StartCoroutine(LoadConfigTxtIEnum(fullPath));
    }

    private IEnumerator LoadConfigTxtIEnum(string fullPath)
    {
        var www = new WWW(fullPath);
        yield return null;
        if (!string.IsNullOrEmpty(www.error))
        {
            Log.Log_hjx("www.error:" + www.error);
        }
        else
        {
            LoadCompletedTxt(fullPath, www.text);
        }
    }
    void LoadCompletedTxt(string interim, string asset)
    {
        ////Log.Log_hjx("LoadCompletedTxt " + numConfig + "  " + interim);
        //string fileNameNotExt = Path.GetFileNameWithoutExtension(interim);
        //ConfigManager.Ins.Add(fileNameNotExt, asset);
        //StartMediator startMediator = MediatorManager.Ins.GetBaseMediator(StartMediator.NAME) as StartMediator;
        //if (--numConfig == 0)
        //{
        //    startMediator.ShowProgress(1);
        //}
    }
    private void LoadConfigBundle(string name, LoadDelegate loadDelegate)
    {
        BundleManager.Ins.Load(name, ELoadPriority.High, false, 0, loadDelegate);
    }
    private void LoadCompletedBundle(string interim, UnityEngine.Object asset)
    {
        ////Log.Log_hjx("LoadCompletedBundle " + numConfig + "  " + interim);
        //string fileNameNotExt = Path.GetFileNameWithoutExtension(interim);
        //ConfigManager.Ins.Add(fileNameNotExt, asset.ToString());
        //StartMediator startMediator = MediatorManager.Ins.GetBaseMediator(StartMediator.NAME) as StartMediator;
        //if (--numConfig == 0)
        //{
        //    startMediator.ShowProgress(1);
        //}
        //else
        //{
        //    startMediator.ShowProgress(1 - numConfig / numMax);
        //}
    }

    public void Init()
    {
        if (IsFirst)
        {
            IsFirst = false;
            BundleManager.Ins.Init();
            UIRootManager.Ins.Init();
            //LocalDataManager.Ins.InitGameServer();
            //HttpManager.Ins.Init();
            InitMediator();
            InitData();
            InitConnect();
            //InitMediatorExclusion();
            ComponentManager.Ins.Init();
            MemPoolManager.Ins.Init();
            //
            //HttpManager.Ins.hostErrorDelegate = HostErrorDelegate;
            //HttpManager.Ins.timeOutDelegate = TimeOutDelegate;
        }
    }

    private void TimeOutDelegate()
    {
        PopUpMediator popUpMediator = MediatorManager.Ins.GetBaseMediator(PopUpMediator.NAME) as PopUpMediator;
        PopUpVo vo = new PopUpVo();
        vo.content = "连接超时!\n请在网络信号良好的地方重新尝试";
        vo.title = "提示";
        //vo.mSureDelegate = HttpManager.Ins.Retry;
        //vo.mCanelDelegate = popUpMediator.CloseWin;
        vo.mSureTitle = "重连";
        popUpMediator.OpenPupUp(vo);
    }
    private void HostErrorDelegate()
    {
        PopUpMediator popUpMediator = MediatorManager.Ins.GetBaseMediator(PopUpMediator.NAME) as PopUpMediator;
        PopUpVo vo = new PopUpVo();
        vo.content = "网络错误。\n服务器无法连接";
        vo.title = "提示";
        //vo.mOkDelegate = HttpManager.Ins.Retry;
        vo.mOkTitle = "重连";
        popUpMediator.OpenPupUp(vo);
    }
    private void InitMediator()
    {
        MediatorManager.Ins.AddBaseMediator(new LoadingMediator(), LoadingMediator.NAME);
        MediatorManager.Ins.AddBaseMediator(new TestMediator(), TestMediator.NAME);

    }
    private void ConnectDelegate(bool isWait)
    {
        if (isWait)
        {
            //MediatorManager.Ins.GetBaseMediator(ConnectMediator.NAME).Open();
        }
        else
        {
            //MediatorManager.Ins.GetBaseMediator(ConnectMediator.NAME).Close();
        }
    }

    private void InitData()
    {
        //new StartData();
        ////new CardData();
        //new RoleData();
        //new DayQuestData();
        //new ServerData();
        ////
        //new AchievementData();
        //new HunterTreasureData();
        //new HunterPartnerData();
        //new HunterMallData();
        //new ExchangeData();
        //new ResponsiveData();
    }

    private void InitConnect()
    {
        //ReceiveManager.Ins.AddReceive(new GetAppKeyReceive());
        //ReceiveManager.Ins.AddReceive(new LoginReceive());
        //ReceiveManager.Ins.AddReceive(new RegisterReceive());
        //ReceiveManager.Ins.AddReceive(new RoleCreateReceive());
        //ReceiveManager.Ins.AddReceive(new ExchangeReceive());
        //ReceiveManager.Ins.AddReceive(new ResponsiveReceive());
        //ReceiveManager.Ins.AddReceive(new OutLineGoldReceive());
        //ReceiveManager.Ins.AddReceive(new LotteryReceive());
        //new GetAppVersionReceive();
        //new RoleEntryReceive();
        //new BattleEntryRecieve();
        //new BattleResultRecieve();
        //new CardSellRecieve();
        //new RoleRenameReceive();
    }



}
