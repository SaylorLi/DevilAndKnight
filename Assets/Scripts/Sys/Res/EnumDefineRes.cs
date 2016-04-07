public enum eLoadResult
{
    /// <summary>
    /// 成功
    /// </summary>
    OK = 0,
    /// <summary>
    /// 存储空间不足
    /// </summary>
    //ERR_STORAGE_SHORTAGE,
    /// <summary>
    /// 超时
    /// </summary>
    ERR_TIMEOUT,
    /// <summary>
    /// 连接超时
    /// </summary>
    //ERR_TIMEOUT_CON,
    /// <summary>
    /// 网络连接断开
    /// </summary>
    //ERR_NET_DISCONNECT,
    /// <summary>
    /// 重试次数到达上限
    /// </summary>
    //ERR_RETRY_FULL,
    /// <summary>
    /// 其他错误，有详细的错误信息
    /// </summary>
    ERR_OTHER
}

/// <summary>
/// 优先级
/// </summary>
public enum ELoadPriority
{
    Low,
    Normal,
    High
}

/// <summary>
/// 下载后的处理方式
/// </summary>
public enum ESaveType
{
    None,
    /// <summary>
    /// 下载后缓存
    /// </summary>
    Cache = 1,
    /// <summary>
    /// 下载后保存到本地
    /// </summary>
    Save = 2,
}


