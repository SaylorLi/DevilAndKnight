using UnityEngine;
using System.Collections;
/// <summary>
/// panel中介类 处理交互逻辑和数据处理逻辑
/// </summary>
public abstract class BaseMediator
{
    //public const string NAME = "BaseMediator";
    //
    public enum eBaseMediatorState
    {
        Open,
        Close,
        Loading,
        NotLoad
    }
    public bool isNoOpen = true;
    public int depthOffset;
    protected bool isIn = false;
    //
    protected bool isInit = true;
    protected CPanel panel;
    protected BoxCollider collider;
    //public bool isAddColider = true;
    //
    bool isFixed;//是否被固定,不会改变深度
    bool isOpen = false;
    eBaseMediatorState state = eBaseMediatorState.NotLoad;
    public static int count = 0;
    public BaseMediator()
    {
    }
    #region public
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

    public virtual void Close(CButton source = null)
    {
        CloseWin();
    }

    protected virtual void UnloadDynimicDownload()
    {
    }

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
    public virtual void Update(float deltaTime)
    {
    }
    /// <summary>
    /// 更新视图.每次打开界面都调用.
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
        isNoOpen = true;
    }

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
    #endregion
    #region get-set
    public bool IsOpen
    {
        get
        {
            return state == eBaseMediatorState.Open;
        }
    }
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

    public virtual void LoadWin()
    {
        //state = eBaseMediatorState.Loading;
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
    public int GetDepthOffset()
    {
        return depthOffset;
    }
    public void LoadWinComplete(string name, UnityEngine.Object asset)
    {
        GameObject go = asset as GameObject;
        //go.name = name;
        //transform = o.transform;
        panel = CPanel.CreatePanel(go);
        UITool.AddChild(panel.Go, UIRootManager.Ins.goAnchor);
        //
        //if (isAddColider)
        //{
        //增加界面碰撞盒
        AddCollider();
        //}

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
        if (isNoOpen)
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

    #region protected

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
    private void DirectOpen()
    {
        SoundManager.Ins.sePlayer.PlaySE(SoundConst.se_window_open);
        state = eBaseMediatorState.Open;
        panel.SetActive(isOpen);
        if (isNoOpen)
        {
            isNoOpen = false;
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
