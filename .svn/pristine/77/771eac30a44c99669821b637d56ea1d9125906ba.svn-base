using UnityEngine;
using System.Collections.Generic;

public class ComponentManager
{
    public static readonly ComponentManager Ins = new ComponentManager();
    //
    private Dictionary<string, GameObject> dicPrefab = new Dictionary<string, GameObject>();
    //hunter
    public const string component_partnerinfo = "component_partnerinfo";//伙伴组件
    public const string component_treasureinfo = "component_treasureinfo";//神器组件
    public const string component_playerinfo = "component_playerinfo";//主角组件
    public const string component_achivementInfo = "component_achivementInfo";//成就组件
    public const string component_iteminfo = "component_iteminfo";//道具组件
    public const string component_reliveInfo = "component_reliveInfo";//道具组件

    public const string component_exchangeInfo = "component_exchangeInfo";//兑换组件
    public const string component_responsiveInfo = "component_responsiveInfo";//仓库组件


    public void Init()
    {
        //hunter
        Load(component_partnerinfo);//注册伙伴组件
        Load(component_treasureinfo);//注册神器组件
        Load(component_playerinfo);//注册主角组件
        Load(component_achivementInfo);//注册成就组件
        Load(component_iteminfo);//注册道具组件
        Load(component_reliveInfo);//注册复活信息组件
        Load(component_exchangeInfo);
        Load(component_responsiveInfo);
    }
    public void Load(string name)
    {
        LoadManager.Ins.LoadResourcesComponet(name, PathUtil.GetComponentRelativePath(name), LoadComplete);
    }
    private void LoadComplete(string interim, UnityEngine.Object asset)
    {
        if (dicPrefab.ContainsKey(interim))
        {
            Log.LogError_sys("ElementDynamicManager ElementDynamic name same error : " + interim);
            return;
        }
        //Log.Log_hjx("LoadComplete  " + interim);
        dicPrefab.Add(interim, (GameObject)asset);
    }
    public GameObject GetPrefabInstance(string name)
    {
        if (!dicPrefab.ContainsKey(name))
        {
            Log.LogError_sys("ComponentManager GetPrefabInstance not exist : " + name);
            return new GameObject();
        }
        GameObject go;
        go = MemPoolManager.Ins.GetPrefabInstance(name, dicPrefab[name]);
        return go;
    }
    public void PrePrefabInstance(string name, int max)
    {
        if (!dicPrefab.ContainsKey(name))
        {
            Log.LogError_sys("ComponentManager GetPrefabInstance not exist : " + name);
            return;
        }
        GameObject prefab = dicPrefab[name];
        MemPoolManager.Ins.PrePrefabInstance(name, dicPrefab[name], max);
    }

    public void RemovePrefabInstance(string name, GameObject go)
    {
        MemPoolManager.Ins.RemovePrefabInstance(name, go);
    }


}
