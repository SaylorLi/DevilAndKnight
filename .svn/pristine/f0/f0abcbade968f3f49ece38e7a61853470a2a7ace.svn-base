using System;
using System.IO;
using System.Collections;
using UnityEngine;

public class BundleLoader
{
    public const float TIMEOUT_TIME = 5f;//超时时长
    public const float TIMEOUT_CONNECT = 5f;//连接超时最大时长
    //
    private WWW www;
    private AssetBundleCreateRequest memLoader;
    private AssetBundle bundle;
    //
    private byte[] data;
    private string text;
    private string errorDetail;
    //
    private eLoadResult loadResult;
    private float progress;//当前下载进度
    private bool isWWWDone;//www下载是否完成
    /// <summary>
    /// 从网络上下载，提供进度反馈和超时处理
    /// </summary>
    public IEnumerator Download(BundleUnit unit)
    {
        float time = 0;//超时计时器
        string url = BundleManager.urlServer + unit.vo.name + BundleManager.EXT;
        www = new WWW(url);
        while (true)
        {
            if (www.isDone || www.HasError())
                break;
            yield return 2;
            // 网络断开直接当超时处理
            //if (!NativePlugin.IsConnectNetwork())
            //{
            //    loadResult = eLoadResult.ERR_NET_DISCONNECT;
            //    break;
            //}
            // 进度没有发生变化(进度小于1)就要进行超时计时了
            if (progress == www.progress && www.progress < 1)
            {
                time += Time.deltaTime;
                if (time > TIMEOUT_TIME)
                {
                    loadResult = eLoadResult.ERR_TIMEOUT;
                    break;
                }
            }
            else
            {
                progress = www.progress;
                time = 0;
            }
        }

        if (loadResult != eLoadResult.ERR_TIMEOUT)
        {
            // 检测是否发生错误
            if (www.HasError())
            {
                errorDetail = www.error;
                loadResult = eLoadResult.ERR_OTHER;
            }
            else if (www.bytes == null || www.bytes.Length == 0)
            {
                errorDetail = "Error bytes null";
                loadResult = eLoadResult.ERR_OTHER;
            }
            else
            {
                if (unit.vo.IsNeed(ESaveType.Save))
                {
                    string localPath = BundleManager.pathLocal + unit.vo.name + BundleManager.EXT;
                    SaveFile(localPath);
                }
                // 这个时候才标注下载完毕，确保不会让BundleManager提前进行Unload
                isWWWDone = true;
            }
        }
    }

    public IEnumerator ForceDownload(string url, System.Action<UnityEngine.Object, string> callback)
    {
        www = new WWW(url);
        yield return www;
        UnityEngine.Object asset = null;
        string error = null;
        if (www.isDone && !www.HasError())
        {
            asset = www.assetBundle.mainAsset;
            www.assetBundle.Unload(false);
        }
        else
        {
            error = www.error;
        }
        // 引发回调
        if (callback != null)
            callback(asset, error);
    }
    public IEnumerator LoadWWW(string url)
    {
        //Log.Log_hjx("url url:" + url);
        www = new WWW(url);
        yield return www;
        UnityEngine.Object asset = null;

        if (www.isDone && !www.HasError())
        {
            isWWWDone = true;
            asset = www.assetBundle.mainAsset;
            // 引发回调
        }
    }
    /// <summary>
    /// 通过IO的方式读取字节并异步构建AssetBundle
    /// </summary>
    public IEnumerator LoadFile(string path)
    {
        byte[] data = File.ReadAllBytes(path);
        memLoader = AssetBundle.CreateFromMemory(data);
        yield return memLoader;
    }

    /// <summary>
    /// 从字节数组异步创建，用于加密/解密
    /// </summary>
    public IEnumerator LoadBinary(byte[] data)
    {
        memLoader = AssetBundle.CreateFromMemory(data);
        yield return memLoader;
    }

    public bool IsDone
    {
        get
        {
            if (www != null)
            {
                return isWWWDone && www.isDone;
            }
            if (memLoader != null)
            {
                return memLoader.isDone;
            }
            return false;
        }
    }

    public bool IsError
    {
        get
        {
            // wwwLoader在Download过程中自动汇报错误
            if (www != null)
            {
                return loadResult != eLoadResult.OK;
            }
            // memLoader采用isDone来尝试引发异常
            if (memLoader != null)
            {
                try
                {
                    bool flag = memLoader.isDone;
                }
                catch
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 获取加载结果枚举，前提需要在加载结束或出错后调用
    /// </summary>
    public eLoadResult LoadResult
    {
        get
        {
            return loadResult;
        }
    }

    /// <summary>
    /// 获取错误详情
    /// </summary>
    public string ErrorDetail
    {
        get
        {
            return errorDetail;
        }
    }

    /// <summary>
    /// 获取当前下载进度(仅下载时有效)
    /// </summary>
    public float Progress
    {
        get
        {
            return progress;
        }
    }

    /// <summary>
    /// 获取加载到的Bundle
    /// </summary>
    public AssetBundle Bundle
    {
        get
        {
            if (bundle == null)
            {
                if (www != null)
                {
                    if (www.HasError() || !isWWWDone)
                    {
                        return null;
                    }
                    bundle = www.assetBundle;
                }
                if (memLoader != null)
                {
                    if (!memLoader.isDone)
                    {
                        return null;
                    }
                    bundle = memLoader.assetBundle;
                }
            }
            return bundle;
        }
    }

    /// <summary>
    /// 获取wwwLoader的字节数组
    /// </summary>
    public byte[] Bytes
    {
        get
        {
            if (data == null)
            {
                if (www != null && www.isDone && !www.HasError())
                    data = www.bytes;
            }
            return data;
        }
    }

    /// <summary>
    /// 获取wwwLoader的文本内容
    /// </summary>
    public string Text
    {
        get
        {
            if (text == null)
            {
                if (www != null && www.isDone && !www.HasError())
                {
                    text = www.text;
                }
            }
            return text;
        }
    }
    /// <summary>
    /// 释放加载器的所有资源
    /// </summary>
    public void Unload(bool unloadAllLoadedObjects)
    {
        if (www != null)
        {
            if (www.assetBundle != null)
            {
                www.assetBundle.Unload(unloadAllLoadedObjects);
            }
            www = null;
        }
        if (memLoader != null)
        {
            if (memLoader.assetBundle != null)
            {
                memLoader.assetBundle.Unload(unloadAllLoadedObjects);
            }
            memLoader = null;
        }
        //
        data = null;
        text = null;
        errorDetail = null;
    }
    /// <summary>
    /// 发生错误时不能调用Unload，只能调用Dispose来施放www资源
    /// </summary>
    public void Dispose()
    {
        if (www != null)
        {
            www = null;
        }
        memLoader = null;
        //
        data = null;
        text = null;
        errorDetail = null;
    }

    /// <summary>
    /// 保存到本地，在确定完成后根据需要再调用
    /// </summary>
    public void SaveFile(string path)
    {
        byte[] bytes = Bytes;
        if (bytes == null)
            return;
        GameUtil.CreateDir(path);
        try
        {
            File.WriteAllBytes(path, bytes);
        }
        catch (Exception ex)
        {
            loadResult = eLoadResult.ERR_OTHER;
            Debug.LogError("写入文件出错：" + ex.Message);
        }
    }


}
