using UnityEngine;
using System.Collections;
/// <summary>
/// panel中介类 处理交互逻辑和数据处理逻辑
/// </summary>
public abstract class BaseMediator
{
    public enum eBaseMediatorState
    {
        Open,
        Close,
        Loading,
        NotLoad
    }
    /// <summary>
    /// 是否没有打开过
    /// </summary>
    public bool neverOpen = true;
    /// <summary>
    /// 界面深度
    /// </summary>
    public int depthOffset;
    protected bool isIn = false;
    protected bool isInit = true;
    protected CPanel panel;
    protected BoxCollider collider;
    bool isFixed;//是否被固定,不会改变深度
    bool isOpen = false;
    eBaseMediatorState state = eBaseMediatorState.NotLoad;
    public static int count = 0;

    public BaseMediator()
    {
    }


    #region public


    /// <summary>
    /// 打开界面
    /// </summary>
    public virtual void Open()
    {
        isOpen = true;
        if (IsOpen)
        {
            UpdateWin();
            return;
        }
        if (state == eBaseMediatorState.NotLoad)
        {
            if (panel == null)
            {
                LoadWin();
            }
            else
            {
                if (isOpen)
                {
                    DirectOpen();
                }
                else
                {
                    state = eBaseMediatorState.Close;
                }
            }
        }
        else if (state == eBaseMediatorState.Close)
        {
            DirectOpen();
        }
    }
    /// <summary>
    /// 关闭界面
    /// </summary>
    /// <param name="source">预留按钮</param>
    public virtual void Close(CButton source = null)
    {
        CloseWin();
    }
    /// <summary>
    /// 设置界面深度
    /// </summary>
    /// <param name="z">界面深度</param>
    public void SetFix(int z)
    {
        IsFixed = true;
        SetDepthOffset(z);
        if (z > 0)
        {
            Position = new Vector3(0, 0, -z / 10);
        }
        else
        {
            Position = new Vector3(0, 0, -z);
        }
    }
    public virtual void SetDepthOffset(int offset)
    {
        if (depthOffset == offset)
        {
            return;
        }
        depthOffset = offset;
        UITool.SetAllDepthOffset(offset, panel.Go);
    }
    /// <summary>
    /// 切换开关状态.
    /// </summary>
    public void Reversal()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    /// <summary>
    /// 更新界面，界面打开时每一帧调用，需要加入Game的listUpdateOpen列表
    /// </summary>
    /// <param name="deltaTime"></param>
    public virtual void Update(float deltaTime)
    {
    }
    /// <summary>
    /// 更新窗口，每次打开界面调用一次
    /// </summary>
    public virtual void UpdateWin()
    {
    }
    /// <summary>
    /// 销毁窗口
    /// </summary>
    public virtual void DestoryWin()
    {
        CloseWin();
        state = eBaseMediatorState.NotLoad;
        neverOpen = true;
    }
    /// <summary>
    /// 加载窗口
    /// </summary>
    public virtual void LoadWin()
    {
        //state = eBaseMediatorState.Loading;
    }
    /// <summary>
    /// 加载窗口完成
    /// </summary>
    /// <param name="name">界面名称</param>
    /// <param name="asset">界面预设</param>
    public void LoadWinComplete(string name, UnityEngine.Object asset)
    {
        GameObject go = asset as GameObject;
        //go.name = name;
        //transform = o.transform;
        panel = CPanel.CreatePanel(go);
        UITool.AddChild(panel.Go, UIRootManager.Ins.goAnchor);
        
        //增加界面碰撞盒
        AddCollider();

        if (isOpen)
        {
            DirectOpen();
        }
        else
        {
            state = eBaseMediatorState.Close;
        }
    }
    /// <summary>
    /// 每次打开淡入淡出
    /// </summary>
    public virtual void OpenIn()
    {
        isIn = true;
        MediatorManager.Ins.OpenLoading(eLoadingMediatorType.loading_in, Open);
    }
    /// <summary>
    /// 第一次打开淡入淡出
    /// </summary>
    public virtual void OpenInFirst()
    {
        if (neverOpen)
        {
            isIn = true;
            MediatorManager.Ins.OpenLoading(eLoadingMediatorType.loading_in, Open);
        }
        else
        {
            Open();
        }
    }


    #endregion


    #region get-set


    /// <summary>
    /// 界面是否开启
    /// </summary>
    public bool IsOpen
    {
        get
        {
            return state == eBaseMediatorState.Open;
        }
    }
    /// <summary>
    /// 初始位置
    /// </summary>
    public Vector3 Position
    {
        set
        {
            panel.Go.transform.localPosition = value;
        }
        get
        {
            return panel.Go.transform.localPosition;
        }
    }
    /// <summary>
    /// 界面深度是否固定不变
    /// </summary>
    public bool IsFixed
    {
        set
        {
            isFixed = value;
        }
        get
        {
            return isFixed;
        }
    }
    /// <summary>
    /// 获得界面深度
    /// </summary>
    /// <returns></returns>
    public int GetDepthOffset()
    {
        return depthOffset;
    }

    #endregion

    #region protected
    protected virtual void UnloadDynimicDownload()
    {
    }
    protected virtual void OpenOut()
    {
        if (isIn)
        {
            isIn = false;
            MediatorManager.Ins.OpenLoading(eLoadingMediatorType.loading_out, null);
        }
    }
    /// <summary>
    /// 第一次打开界面的时候调用 只调一次
    /// </summary>
    protected virtual void FirstOpen()
    {
    }
    #endregion

    #region private
    private void CloseWin()
    {
        isOpen = false;
        if (IsOpen)
        {
            SoundManager.Ins.sePlayer.PlaySE(SoundConst.se_window_close);
            UnloadDynimicDownload();
            if (++count > 20)
            {
                count = 0;
                Resources.UnloadUnusedAssets();
            }
            //MediatorManager.Ins.CloseWin(this);
            panel.SetActive(isOpen);
            state = eBaseMediatorState.Close;
        }
    }
    /// <summary>
    /// 增加界面碰撞盒
    /// </summary>
    private void AddCollider()
    {
        GameObject o = panel.Go;
        collider = o.GetComponent<BoxCollider>();
        if (null == collider)
        {
            collider = o.AddComponent<BoxCollider>();
        }
        collider.center = new Vector3(0, 0, 0);
        collider.size = new Vector3(2000, 2000, 0);
        collider.isTrigger = true;
    }
    /// <summary>
    /// 直接打开
    /// </summary>
    private void DirectOpen()
    {
        SoundManager.Ins.sePlayer.PlaySE(SoundConst.se_window_open);
        state = eBaseMediatorState.Open;
        panel.SetActive(isOpen);
        if (neverOpen)
        {
            neverOpen = false;
            FirstOpen();
        }
        //MediatorManager.Ins.OpenWin(this);
        UpdateWin();
        OpenOut();
        if (isInit)
        {
            isInit = false;
        }
    }
    #endregion



}
