using UnityEngine;
using System.Collections.Generic;

public class ComponentManager
{
    public static readonly ComponentManager Ins = new ComponentManager();
    //
    private Dictionary<string, GameObject> dicPrefab = new Dictionary<string, GameObject>();
    //hunter
    public const string component_partnerinfo = "component_partnerinfo";//������
    public const string component_treasureinfo = "component_treasureinfo";//�������
    public const string component_playerinfo = "component_playerinfo";//�������
    public const string component_achivementInfo = "component_achivementInfo";//�ɾ����
    public const string component_iteminfo = "component_iteminfo";//�������
    public const string component_reliveInfo = "component_reliveInfo";//�������

    public const string component_exchangeInfo = "component_exchangeInfo";//�һ����
    public const string component_responsiveInfo = "component_responsiveInfo";//�ֿ����


    public void Init()
    {
        //hunter
        Load(component_partnerinfo);//ע�������
        Load(component_treasureinfo);//ע���������
        Load(component_playerinfo);//ע���������
        Load(component_achivementInfo);//ע��ɾ����
        Load(component_iteminfo);//ע��������
        Load(component_reliveInfo);//ע�Ḵ����Ϣ���
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
