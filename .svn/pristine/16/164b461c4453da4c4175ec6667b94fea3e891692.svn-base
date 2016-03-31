public class LoadRequestVo
{
    public LoadDelegate loadDelegate;
    public OnTextDownloadHandle textHandle;
    //
    public string name;
    /// <summary>
    /// 是否为静态常驻内存
    /// </summary>
    public bool isStatic;
    /// <summary>
    /// 请求放弃，在下载后检测
    /// </summary>
    public bool reqRelease = false;
    /// <summary>
    /// 优先级
    /// </summary>
    public ELoadPriority eLoadPriority = ELoadPriority.Normal;
    /// <summary>
    /// 当前对象已重复请求次数
    /// </summary>
    public int retryNum;
    /// <summary>
    /// 是否为必须，如果是则会一直提示，提示后再retry
    /// </summary>
    public bool isMusted = true;
    /// <summary>
    /// 仅加载本地,本地如果没有就跳过,采用默认资源
    /// </summary>
    public bool isOnlyLocal = true;
    //
    private int saveType;

    public LoadRequestVo()
    {
    }
    public void Init(string name, ELoadPriority eLoadPriority, bool isStatic, int saveType, LoadDelegate loadDelegate)
    {
        this.name = name;
        this.isStatic = isStatic;
        this.eLoadPriority = eLoadPriority;
        this.saveType = saveType;
        this.loadDelegate = loadDelegate;
    }
    /// <summary>
    /// 预先下载使用
    /// </summary>
    public void Init(string name, int saveType)
    {
        Init(name, false, null);
        this.saveType = saveType;
    }
    /// <summary>
    ///下载file文件用
    /// </summary>
    public void Init(string name, LoadDelegate loadDelegate)
    {
        Init(name, false, loadDelegate);
    }
    public void Init(string name, bool isStatic, LoadDelegate loadDelegate)
    {
        this.name = name;
        this.isStatic = isStatic;
        this.loadDelegate = loadDelegate;
    }
    /// <summary>
    /// 下载后的处理方式
    /// </summary>
    public bool IsNeed(ESaveType e)
    {
        int type = (int)e;
        return (type & saveType) == type;
    }
}

/// <summary>
/// 缓存对象
/// </summary>
public class Bundle
{
    /// <summary>
    /// 缓存对象
    /// </summary>
    public UnityEngine.Object asset;
    /// <summary>
    /// 相对路径
    /// </summary>
    public string fileName;
    /// <summary>
    /// 是否为静态
    /// </summary>
    public bool isStatic;

    public Bundle(string name, UnityEngine.Object obj, bool isStatic)
    {
        asset = obj;
        fileName = name;
        this.isStatic = isStatic;
    }
}
/// <summary>
/// 下载单元，负责为加载器指配请求对象，这样做可以很方便的进行并发控制
/// </summary>
public class BundleUnit
{
    public BundleLoader loader = new BundleLoader();
    public LoadRequestVo vo;

}
/// <summary>
/// Bundle状态
/// </summary>
public enum BundleState
{
    /// <summary>
    /// 资源在本地没找到
    /// </summary>
    NotFound,
    /// <summary>
    /// 资源在本地存在
    /// </summary>
    FileExists,
    /// <summary>
    /// 资源在本地存在，但是已过期
    /// </summary>
    FileOutOfDate,
    /// <summary>
    /// 资源正在加载中
    /// </summary>
    Loading,
    /// <summary>
    /// 资源加载完毕，在内存中
    /// </summary>
    OnList
}