using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public delegate void OnTextDownloadHandle(string name, string text);
public delegate void OnBytesDownloadHandle(string name, byte[] data);

public class BundleManager : MonoBehaviour
{
    public static string urlServer;
    public static string pathData;//数据
    public static string pathLocal;
    public static string pathLocalWWW;
    //
    public static readonly string urlPersistent = Application.persistentDataPath + "/";
    public static readonly string pathStream = Application.streamingAssetsPath + "/";

    private string platform;
    private string resVersion = "1";
    public const string EXT = ".ab";  // Bundle的扩展名

    public const int MAX_NUM_RETRY = 3;//重试次数上限
    public static int MAX_NUM_UNIT =30;//最大并发上限

    private List<LoadRequestVo> loadQueue = new List<LoadRequestVo>();//请求列表
    private List<LoadRequestVo> retryQueue = new List<LoadRequestVo>();//重试列表,用于下载失败之后的重试和下载超时后的重试,如果此队列不为空则需要暂停新的请求
    private List<Bundle> bundleList = new List<Bundle>();//缓存列表
    private BundleUnit[] loadUnit = new BundleUnit[0];//下载单元
    //
    private static BundleManager ins;
    //
    public bool isPause;
    private bool isSystemWindow = false;//当前是否正有窗口弹出
    void OnDestroy()
    {
        foreach (BundleUnit unit in loadUnit)
        {
            if (unit != null && unit.loader != null)
            {
                unit.loader.Dispose();
                unit.loader = null;
            }
        }
        // 即使下载过程中出错,也可以得到及时更新本地的hash
        HashManager.UpdateLocalHash();
    }

    void Update()
    {
        foreach (BundleUnit unit in loadUnit)
        {
            UpdateLoadUnit(unit);
        }

        if (GetLoadRequestNum() == 0)
        {
            HashManager.UpdateLocalHash();
        }
    }
    public void Init()
    {
        InitLocal();
        InitUrl();
    }
    void InitLocal()
    {
        pathLocal = pathStream;
#if UNITY_ANDROID
                    platform = "android";
#elif UNITY_IPHONE
					platform = "ios";
#elif UNITY_STANDALONE
        platform = "win";
#endif

        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                {
                    pathData = Application.dataPath + "/../file/";
                    pathLocalWWW = "file:///" + pathLocal;
                }
                break;
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                {
                    Publish();
                }
                break;
        }

        //Log.Log_hjx("platform:" + platform + " pathLocalWWW=" + pathLocalWWW);
    }
    void Publish()
    {
        pathData = urlPersistent;
        pathLocalWWW = pathLocal;
        //
        Game.Ins.Publish();
    }
    void InitUrl()
    {
        if (Game.Ins.IsLAN)
        {
            urlServer = IpConfig.LAN_RES;
        }
        else
        {
            urlServer = IpConfig.NET_RES;
        }
        // 根据平台拼凑出路径
        urlServer = urlServer + platform + "/" + resVersion + "/";

        loadUnit = new BundleUnit[MAX_NUM_UNIT];
        for (int i = 0; i < MAX_NUM_UNIT; i++)
        {
            loadUnit[i] = new BundleUnit();
        }
    }
    /// <summary>
    /// 获取当前的请求数量
    /// </summary>
    public int GetLoadRequestNum()
    {
        return loadQueue.Count + retryQueue.Count + GetLoadingNum();
    }
    // 正在执行的数量
    private int GetLoadingNum()
    {
        int count = 0;
        foreach (BundleUnit unit in loadUnit)
        {
            if (unit.vo != null)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// 获取缓存对象
    /// </summary>
    public T GetObject<T>(string name) where T : UnityEngine.Object
    {
        return GetObjectInternal(name) as T;
    }

    /// <summary>
    /// 强行下载，直接下载，用于下载files-hash.sxd和files.sxd文件
    /// </summary>
    public void ForceDownload(string name, LoadDelegate callback)
    {
        LoadRequestVo req = new LoadRequestVo();
        req.Init(name, callback);
        EnqueueWithoutCheck(req);
    }

    public void ForceDownload(string name, OnTextDownloadHandle callback)
    {
        LoadRequestVo req = new LoadRequestVo();
        req.Init(name, null);
        req.textHandle = callback;
        EnqueueWithoutCheck(req);
    }

    IEnumerator PreDownloadIEnum(System.Action<int> callback)
    {
        yield return 0;
        int count = 0;
        Dictionary<string, string> serverHash = HashManager.ServerHash;
        foreach (KeyValuePair<string, string> pair in serverHash)
        {
            if (PreDownloadOne(pair.Key, (int)ESaveType.Save))
                count++;
        }

        if (callback != null)
            callback(count);
    }
    /// <summary>
    /// 请求下载指定元素
    /// </summary>
    private bool PreDownloadOne(string name, int saveType)
    {
        switch (GetStatus(name))
        {
            case BundleState.NotFound:
            case BundleState.FileOutOfDate:
                {
                    LoadRequestVo req = new LoadRequestVo();
                    req.Init(name, saveType);
                    Enqueue(req);
                    return true;
                }
        }
        return false;
    }
    /// <summary>
    /// 获取可更新的文件列表
    /// </summary>
    /// <param name="callback"></param>
    public void GetRenewableList(System.Action<List<string>> callback)
    {
        StartCoroutine(Inner_GetRenewableList(callback));
    }

    IEnumerator Inner_GetRenewableList(System.Action<List<string>> callback)
    {
        yield return 1;
        Dictionary<string, string> serverHash = HashManager.ServerHash;
        List<string> list = new List<string>(serverHash.Count);
        foreach (KeyValuePair<string, string> pair in serverHash)
        {
            //string localPath = urlLocal + "/" + pair.Key + EXT_BUNDLE;
            if (HashManager.IsLocalHashExpired(pair.Key))
                list.Add(pair.Key);
        }
        list.TrimExcess();
        callback(list);
    }


    public void LoadAtlas(string name, LoadDelegate loadDelegate, bool isOnlyLocal)
    {
        UnityEngine.Object obj = GetObjectInternal(name);
        if (obj != null)
        {
            //Log.Log_hjx("LoadAtlas 缓存: " + name);
            if (loadDelegate != null)
            {
                loadDelegate(name, obj);
            }
            return;
        }
        LoadRequestVo loadRequest = GetLoadRequest(name);
        if (loadRequest != null)
        {
            loadRequest.isOnlyLocal = isOnlyLocal;
            //Log.Log_hjx("LoadAtlas 网络重复加载: " + name);
            loadRequest.loadDelegate += loadDelegate;
            return;
        }
        //Log.Log_hjx("LoadAtlas 网络加载: " + name);
        LoadRequestVo request = new LoadRequestVo();
        request.isMusted = false;
        request.isOnlyLocal = isOnlyLocal;
        Enqueue(request);
    }

    // 请求加载，非静态
    public void Load(string name, int saveType)
    {
        Load(name, ELoadPriority.High, false, saveType, null);
    }

    public string Load(string name, ELoadPriority eLoadPriority, bool isStatic, int saveType, LoadDelegate loadDelegate)
    {
        if (GetObjectInternal(name) == null && IsLoadingObject(name) == false)
        {
            // Save参数在下载后会自动保存到本地
            // Cache参数在下载或者加载后会自动进行缓存
            LoadRequestVo request = new LoadRequestVo();
            request.Init(name, eLoadPriority, isStatic, saveType, loadDelegate);
            //Log.Log_hjx("namename " + request.isMusted + " " + name);
            Enqueue(request);
            return name;
        }
        return null;
    }

    // 释放所有资源
    public void ReleaseAll(bool force)
    {
        if (force)
        {
            bundleList.Clear();
            loadQueue.Clear();
            retryQueue.Clear();
        }
        else
        {
            // 倒序可以在遍历的时候改变集合
            for (int i = bundleList.Count - 1; i >= 0; i--)
            {
                Bundle bundle = bundleList[i];
                if (!bundle.isStatic)
                {
                    bundle.asset = null;
                    bundleList.Remove(bundle);
                }
            }

            object[] tmpArr = loadQueue.ToArray();
            loadQueue.Clear();
            for (int j = 0; j < tmpArr.Length; j++)
            {
                LoadRequestVo req = (LoadRequestVo)tmpArr[j];
                if (req.isStatic)
                {
                    Enqueue(req);
                }
                else
                {
                    string localPath = urlPersistent + req.name + EXT;
                    if (!File.Exists(localPath))
                    {
                        Enqueue(req);
                    }
                }
            }

            tmpArr = retryQueue.ToArray();
            retryQueue.Clear();
            for (int j = 0; j < tmpArr.Length; j++)
            {
                LoadRequestVo req = (LoadRequestVo)tmpArr[j];
                if (req.isStatic)
                {
                    Enqueue(req);
                }
                else
                {
                    string localPath = urlPersistent + req.name + EXT;
                    if (!File.Exists(localPath))
                    {
                        Enqueue(req);
                    }
                }
            }
        }

        foreach (BundleUnit unit in loadUnit)
        {
            if (unit.vo != null && (force || !unit.vo.isStatic))
            {
                unit.vo.reqRelease = true;
            }
        }

        Free();
    }

    // 获取状态
    private BundleState GetStatus(string name)
    {
        // 是否正在侯队中或者正在下载中
        if (IsLoadingObject(name))
        {
            return BundleState.Loading;
        }
        // 是否已经在缓存中
        if (GetObjectInternal(name) != null)
        {
            return BundleState.OnList;
        }
        // 检测本地是否存在
        string localPath = urlPersistent + name + EXT;
        if (!File.Exists(localPath))
        {
            return BundleState.NotFound;
        }
        // 检测本地文件是否已过期
        if (HashManager.IsLocalHashExpired(localPath))
        {
            return BundleState.FileOutOfDate;
        }
        return BundleState.FileExists;
    }

    // 侯队
    private void Enqueue(LoadRequestVo req)
    {
        if (!IsInLoading(req))
        {
            loadQueue.Add(req);
        }
    }

    // 侯队，无安检
    private void EnqueueWithoutCheck(LoadRequestVo req)
    {
        loadQueue.Add(req);
    }

    // 取出请求对象，按优先级来
    private LoadRequestVo Dequeue()
    {
        int num = GetLoadingNum();
        if (num == MAX_NUM_UNIT)
        {
            return null;
        }
        LoadRequestVo item = null;
        // 优先取出Retry里的内容
        foreach (LoadRequestVo r in retryQueue)
        {
            if (item == null || item.eLoadPriority < r.eLoadPriority)
            {
                item = r;
            }
        }
        if (item != null)
        {
            retryQueue.Remove(item);
            return item;
        }

        item = null;
        foreach (LoadRequestVo r in loadQueue)
        {
            if (item == null || item.eLoadPriority < r.eLoadPriority)
            {
                item = r;
            }
        }
        if (item != null)
        {
            loadQueue.Remove(item);
            return item;
        }

        return null;
    }

    /// <summary>
    /// 检测指定的请求是否正在加载中
    /// </summary>
    private bool IsInLoading(LoadRequestVo req)
    {
        foreach (BundleUnit unit in loadUnit)
        {
            if (unit.vo != null && unit.vo.name.Equals(req.name))
            {
                return true;
            }
        }
        foreach (LoadRequestVo r in retryQueue)
        {
            if (r != null && r.name.Equals(req.name))
            {
                return true;
            }
        }
        foreach (LoadRequestVo r in loadQueue)
        {
            if (r != null && r.name.Equals(req.name))
            {
                return true;
            }
        }
        return false;
    }

    private LoadRequestVo GetLoadRequest(string name)
    {
        foreach (LoadRequestVo req in retryQueue)
        {
            if (req.name == name)
            {
                return req;
            }
        }
        // 首先检测是否在候队中
        foreach (LoadRequestVo req in loadQueue)
        {
            //Log.Log_hjx("req.fileName:" + req.fileName + "name:" + name);
            if (req.name == name)
            {
                return req;
            }
        }
        // 再检测是否正在下载中
        foreach (BundleUnit unit in loadUnit)
        {
            if (unit.vo != null && unit.vo.name == name)
            {
                return unit.vo;
            }
        }
        return null;
    }
    /// <summary>
    /// 检测指定对象是否正在加载中
    /// </summary>
    private bool IsLoadingObject(string name)
    {
        return GetLoadRequest(name) != null;
    }

    /// <summary>
    /// 从缓存中获取满足指定条件的对象
    /// </summary>
    private UnityEngine.Object GetObjectInternal(string name)
    {
        foreach (Bundle bundle in bundleList)
        {
            if (bundle.fileName == name)
            {
                return bundle.asset;
            }
        }
        return null;
    }

    private void UpdateLoadUnit(BundleUnit unit)
    {
        if (unit.vo == null)
        {
            // 系统正在弹窗时暂停新任务进行
            if (isSystemWindow) return;
            unit.vo = Dequeue();
            if (unit.vo == null) return;
            //
            if (Game.Ins.isEditorPrefab)
            {
                EditorPrefab(unit);
            }
            else
            {
                StartLoader(unit);
            }
        }
        else
        {
            if (unit.loader.IsError)
            {
                ProcessError(unit);
            }
            else
            {
                if (unit.loader.IsDone)
                {
                    ProcessDone(unit);
                }
            }
        }
    }

    private void ProcessDone(BundleUnit unit)
    {
        // 完整性检验
        //if (unit.loader.Bytes != null && !HashManager.IsFileHashValid(unit.vo.name, unit.loader.Bytes))
        //{
        //    unit.loader.Unload(true);
        //    CheckErrorRetry(unit.vo);
        //    unit.loader.ClearError();
        //    unit.loader.Dispose();
        //    unit.vo = null;
        //    return;
        //}

        //  加载完成处理
        if (!unit.vo.reqRelease)
        {
            if (unit.vo.textHandle != null)
                unit.vo.textHandle(unit.vo.name, unit.loader.Text);
            // 缓存
            AssetBundle assetBundle = unit.loader.Bundle;
            if (assetBundle != null)
            {
                if (unit.vo.IsNeed(ESaveType.Cache))
                {
                    Bundle bundle = new Bundle(unit.vo.name, assetBundle.mainAsset, unit.vo.isStatic);
                    bundleList.Add(bundle);
                }
                if (unit.vo.loadDelegate != null)
                {
                    //Log.Log_hjx("加载完成 unit.request.fileName " + unit.request.fileName);
                    unit.vo.loadDelegate(unit.vo.name, assetBundle.mainAsset);
                }
            }
        }
        unit.vo = null;
        unit.loader.Unload(false);
        //销毁处理
        //System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(DisposeUnitAsync));
        //thread.Start(unit);
    }

    private void ProcessError(BundleUnit unit)
    {
        Debug.Log("加载(下载)出错啦---" + unit.loader.ErrorDetail + "\t---" + unit.vo.name + " urlServer:" + urlServer);
        string localPath = urlPersistent + unit.vo.name + EXT;
        if (File.Exists(localPath))
        {
            File.Delete(localPath);
        }

        if (unit.loader.LoadResult == eLoadResult.ERR_TIMEOUT)
        {
            CheckErrorTimeout(unit.vo);
        }
        else
        {
            CheckErrorRetry(unit.vo);
        }
        unit.loader.Dispose();
        unit.vo = null;
    }

    private void StartLoader(BundleUnit unit)
    {
        string localPath = pathLocalWWW + unit.vo.name + EXT;
        if (unit.vo.isOnlyLocal)
        {
            StartCoroutine(unit.loader.LoadWWW(localPath));
        }
        else
        {
            // 会尝试进行同步
            if (File.Exists(localPath) && !HashManager.IsLocalHashExpired(unit.vo.name))
            {
                // 本地加载
                StartCoroutine(unit.loader.LoadFile(localPath));
            }
            else
            {
                // 从网络中下载
                StartCoroutine(unit.loader.Download(unit));
            }
        }
    }

    private void EditorPrefab(BundleUnit unit)
    {
#if UNITY_EDITOR
        UnityEngine.Object prefab = UnityEditor.AssetDatabase.LoadAssetAtPath(pathLocal + unit.vo.name + ".prefab", typeof(UnityEngine.Object));
        Bundle bundle = new Bundle(unit.vo.name, prefab, unit.vo.isStatic);
        bundleList.Add(bundle);
        //
        if (unit.vo.loadDelegate != null)
        {
            unit.vo.loadDelegate(unit.vo.name, prefab);
        }
        //
        unit.loader.Unload(false);
        unit.vo = null;
#endif
    }

    private void DisposeUnitAsync(object unitArg)
    {
        BundleUnit unit = unitArg as BundleUnit;
        if (unit != null)
        {
            unit.loader.Unload(false);
        }
    }

    /// <summary>
    /// 处理retryQueue队伍,讲retry队伍中的元素Push到候队列表中
    /// </summary>
    private void CheckErrorRetry(LoadRequestVo req)
    {
        // 重试次数递增
        if (++req.retryNum < MAX_NUM_RETRY)
        {
            retryQueue.Add(req);//这种静默重试可以考虑放在队列尾部
        }
        else
        {
            if (req.isMusted)
            {
                // 对于必须资源,弹窗提醒,无限进行重试
                if (isSystemWindow)
                {
                    retryQueue.Add(req);
                }
                else
                {
                    System.Delegate[] list = OnRetry.GetInvocationList();
                    if (list != null && list.Length > 0)
                    {
                        isSystemWindow = true;
                        retryQueue.Insert(0, req);//插入队列头部
                        OnRetry(req);
                    }
                    else
                    {
                        Debug.Log("请务必订阅OnRetry事件");
                    }
                }
            }
            else
            {
                // 对于非必须资源,就不再进行重试了
                if (req.loadDelegate != null)
                {
                    //Log.Log_hjx("加载失败  重试次数到达上限 fileName:" + req.fileName + " 非必须文件直接放弃下载");
                    req.loadDelegate(req.name, null);
                }
            }
        }
    }

    /// <summary>
    /// 系统弹窗后确认重试按钮点击后调用此方法
    /// </summary>
    public void RetryAfterError()
    {
        isSystemWindow = false;
    }

    public event System.Action<LoadRequestVo> OnRetry;
    public event System.Action<LoadRequestVo> OnTimeout;

    private void CheckErrorTimeout(LoadRequestVo req)
    {
        if (req.isMusted)
        {
            if (isSystemWindow)
            {
                // 如果当前已弹窗,直接塞进Retry队列表
                retryQueue.Add(req);
            }
            else
            {
                System.Delegate[] list = OnTimeout.GetInvocationList();
                if (list != null && list.Length > 0)
                {
                    isSystemWindow = true;
                    retryQueue.Insert(0, req);//插在队列头部
                    OnTimeout(req);
                }
                else
                {
                    Debug.Log("请务必订阅OnTimeout事件");
                }
            }
        }
    }

    /// <summary>
    /// 对于必须资源,发生Timeout后会弹出窗口,弹出后点击确定调用此方法来做继续
    /// </summary>
    public void RetryAfterTimeout()
    {
        isSystemWindow = false;
    }

    public static BundleManager Ins
    {
        get
        {
            if (ins == null)
            {
                GameObject go = new GameObject("BundleManager");
                UnityEngine.Object.DontDestroyOnLoad(go);
                ins = go.AddComponent<BundleManager>();
            }
            return ins;
        }
    }

    /// <summary>
    /// 回收系统资源，引发系统GC
    /// </summary>
    public static void GC()
    {
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    public static void Free()
    {
        BaseMediator.count = 0;
        Resources.UnloadUnusedAssets();
    }

    public void StopCoroutines()
    {
        StopAllCoroutines();
        loadQueue.Clear();//清空请求队列
        retryQueue.Clear();//清空retry队列
        foreach (BundleUnit unit in loadUnit)
        {
            if (unit.loader != null)
                unit.loader.Dispose();
            unit.vo = null;
        }
    }
}
