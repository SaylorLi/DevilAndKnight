using UnityEngine;
using System.Collections;
/// <summary>
/// panel�н��� �������߼������ݴ����߼�
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
    /// �Ƿ�û�д򿪹�
    /// </summary>
    public bool neverOpen = true;
    /// <summary>
    /// �������
    /// </summary>
    public int depthOffset;
    protected bool isIn = false;
    protected bool isInit = true;
    protected CPanel panel;
    protected BoxCollider collider;
    bool isFixed;//�Ƿ񱻹̶�,����ı����
    bool isOpen = false;
    eBaseMediatorState state = eBaseMediatorState.NotLoad;
    public static int count = 0;

    public BaseMediator()
    {
    }


    #region public


    /// <summary>
    /// �򿪽���
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
    /// �رս���
    /// </summary>
    /// <param name="source">Ԥ����ť</param>
    public virtual void Close(CButton source = null)
    {
        CloseWin();
    }
    /// <summary>
    /// ���ý������
    /// </summary>
    /// <param name="z">�������</param>
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
    /// �л�����״̬.
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
    /// ���½��棬�����ʱÿһ֡���ã���Ҫ����Game��listUpdateOpen�б�
    /// </summary>
    /// <param name="deltaTime"></param>
    public virtual void Update(float deltaTime)
    {
    }
    /// <summary>
    /// ���´��ڣ�ÿ�δ򿪽������һ��
    /// </summary>
    public virtual void UpdateWin()
    {
    }
    /// <summary>
    /// ���ٴ���
    /// </summary>
    public virtual void DestoryWin()
    {
        CloseWin();
        state = eBaseMediatorState.NotLoad;
        neverOpen = true;
    }
    /// <summary>
    /// ���ش���
    /// </summary>
    public virtual void LoadWin()
    {
        //state = eBaseMediatorState.Loading;
    }
    /// <summary>
    /// ���ش������
    /// </summary>
    /// <param name="name">��������</param>
    /// <param name="asset">����Ԥ��</param>
    public void LoadWinComplete(string name, UnityEngine.Object asset)
    {
        GameObject go = asset as GameObject;
        //go.name = name;
        //transform = o.transform;
        panel = CPanel.CreatePanel(go);
        UITool.AddChild(panel.Go, UIRootManager.Ins.goAnchor);
        
        //���ӽ�����ײ��
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
    /// ÿ�δ򿪵��뵭��
    /// </summary>
    public virtual void OpenIn()
    {
        isIn = true;
        MediatorManager.Ins.OpenLoading(eLoadingMediatorType.loading_in, Open);
    }
    /// <summary>
    /// ��һ�δ򿪵��뵭��
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
    /// �����Ƿ���
    /// </summary>
    public bool IsOpen
    {
        get
        {
            return state == eBaseMediatorState.Open;
        }
    }
    /// <summary>
    /// ��ʼλ��
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
    /// ��������Ƿ�̶�����
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
    /// ��ý������
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
    /// ��һ�δ򿪽����ʱ����� ֻ��һ��
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
    /// ���ӽ�����ײ��
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
    /// ֱ�Ӵ�
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
