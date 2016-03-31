using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public delegate void LoadDelegate(string interim, UnityEngine.Object asset);

public class LoadManager
{
    public static readonly LoadManager Ins = new LoadManager();
    /// <summary>
    /// 加载图集 
    /// onlyLocal false:下载 true:只用本地文件
    /// isNone false:什么也不做 true:先用默认文件
    /// </summary>
    public void LoadAtlas(string path, LoadDelegate loadDelegate, bool onlyLocal, bool isNone)
    {
        if (isNone)
        {
            GameObject asset;
            asset = MemPoolManager.Ins.GetAlwas(path);
            //Log.Log_hjx((asset == null) + "  " + str);
            if (asset != null)
            {
                loadDelegate(path, asset);
            }
        }
        BundleManager.Ins.LoadAtlas(path, loadDelegate, onlyLocal);
    }

    public GameObject LoadResourcesPrefab(string path)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        return obj;
    }
    public GameObject LoadResourcesGoInstantiate(string path)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        if (obj == null)
        {
            Debug.Log("GameObject none: " + path);
            return null;
        }
        else
        {
            //return obj;
            return GameObject.Instantiate(obj) as GameObject;
        }
    }
    public AudioClip LoadResourcesAudioClip(string path)
    {
        AudioClip mainClip = Resources.Load<AudioClip>("sound/bgm/" + path);
        return mainClip;
    }
    public void LoadResourcesPanel(string interim, LoadDelegate loadDelegate)
    {
        GameObject obj = Resources.Load<GameObject>(interim);
        loadDelegate(interim, GameObject.Instantiate(obj));
    }
    public void LoadResourcesComponet(string interim, string path, LoadDelegate loadDelegate)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        loadDelegate(interim, obj);
    }
    public void LoadResourcesAtlas(string interim, LoadDelegate loadDelegate)
    {
        GameObject obj = Resources.Load<GameObject>(interim);
        loadDelegate(interim, obj);
    }
    //-------------IEnumerator------------------
    //public IEnumerator DownloadTutBundles(System.Action callback)
    //{
    //    // 第一步，下载platform下的tutfiles.sxd文件
    //    StartData data = DataManager.Ins.GetBaseData<StartData>();
    //    string url = BundleManager.urlServer.Replace("/" + data.resVersion + "/", "/") + "tutfiles.sxd";
    //    WWW www = new WWW(url);
    //    yield return www;
    //    if (www.isDone && !www.HasError())
    //    {
    //        AssetBundle bundle = www.assetBundle;
    //        TextAsset asset = bundle.mainAsset as TextAsset;
    //        string content = asset.text;
    //        // 及时施放资源
    //        bundle.Unload(true);
    //        // 解析tutfiles
    //        string[] rows = content.Replace("\r\n", "\n").Split('\n');
    //        foreach (string s in rows)
    //        {
    //            if (string.IsNullOrEmpty(s) == false)
    //                BundleManager.Ins.Load(s, (int)ESaveType.Save);
    //        }
    //    }
    //    else
    //    {
    //        Log.LogError_sys("下载tutfiles出错:" + www.error + "url " + url);
    //    }
    //    // 等待下载完毕
    //    while (true)
    //    {
    //        yield return 1;
    //        if (BundleManager.Ins.GetLoadRequestNum() <= 0)
    //        {
    //            break;
    //        }
    //    }
    //    // GC
    //    BundleManager.GC();
    //    // 引发回调
    //    if (callback != null)
    //        callback();
    //}

}
