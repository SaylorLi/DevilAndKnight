using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MemPoolInfo
{
	//public string name;
	public List<GameObject> list = new List<GameObject>();
	public int max;
}
public class MemPoolManager
{
	public static readonly MemPoolManager Ins = new MemPoolManager();
	Dictionary<string, GameObject> dicAlwas = new Dictionary<string, GameObject>();
	private Dictionary<string, MemPoolInfo> dicPrefab = new Dictionary<string, MemPoolInfo>();
	Transform transformGo;
	Transform transformComponent;

	internal void Init()
	{
		GameObject go = new GameObject();
		GameObject.DontDestroyOnLoad(go);
		go.SetActive(false);
		go.name = "MemPoolManager";
		transformGo = go.transform;
		transformGo.parent = transformGo;
		//
		SetAlwasAtlas();

		go = new GameObject();
		go.name = "Component";
		transformComponent = go.transform;
		transformComponent.parent = transformGo;
	}

	internal GameObject GetPrefabInstance(string name, GameObject prefab)
	{
		MemPoolInfo memPoolInfo = GetMemPoolInfo(name);
		int len = memPoolInfo.list.Count;
		GameObject go;
		if (len > 0)
		{
			go = memPoolInfo.list[len - 1];
			memPoolInfo.list.RemoveAt(len - 1);
		}
		else
		{
			go = (GameObject)GameObject.Instantiate(prefab);
		}
		go.SetActive(true);
		return go;
	}
	internal MemPoolInfo GetMemPoolInfo(string name)
	{
		MemPoolInfo memPoolInfo;
		if (dicPrefab.ContainsKey(name) == false)
		{
			dicPrefab[name] = new MemPoolInfo();
		}
		memPoolInfo = dicPrefab[name];
		return memPoolInfo;
	}

	internal void PrePrefabInstance(string name, GameObject prefab, int max)
	{
		Transform transformContain = GetTransform(name);
		int len = max - transformContain.childCount;
		for (int i = 0; i < len; i++)
		{
			GameObject go = (GameObject)GameObject.Instantiate(prefab);
			go.transform.parent = transformContain;
			//go.name = name;
			MemPoolInfo memPoolInfo = GetMemPoolInfo(name);
			memPoolInfo.max = max;
			if (memPoolInfo.list.Count < memPoolInfo.max)
			{
				memPoolInfo.list.Add(go);
			}
		}
	}
	internal void RemovePrefabInstance(string name, GameObject go)
	{
		go.SetActive(false);
		MemPoolInfo memPoolInfo = GetMemPoolInfo(name);
		if (memPoolInfo.list.Count < memPoolInfo.max)
		{
			memPoolInfo.list.Add(go);
			go.transform.parent = GetTransform(name);
		}
		else
		{
			GameObject.Destroy(go);
		}
	}
	Transform GetTransform(string name)
	{
		Transform transform = transformComponent.FindChild(name);
		if (transform == null)
		{
			GameObject go = new GameObject();
			go.name = name;
			transform = go.transform;
			transform.parent = transformComponent;
		}
		return transform;
	}
	void SetAlwasAtlas()
	{
		//SetAlwasAtlasOne(PathUtil.GetHeadPartyRelativePath(string.Empty));
		//SetAlwasAtlasOne(PathUtil.GetEventBgRelativePath(string.Empty));
		//SetAlwasAtlasOne(PathUtil.GetMapBgRelativePath(string.Empty));
	}
	void SetAlwasAtlasOne(string str)
	{
		GameObject asset = Resources.Load<GameObject>(str + "ch_none");
		//Log.Log_hjx((asset == null) + " SetAlwasAtlasOne  " + str);
		dicAlwas.Add(str, asset);
	}

	internal GameObject GetAlwas(string str)
	{
		string path;
		path = str.Remove(str.LastIndexOf("/")) + "/";
		if (dicAlwas.ContainsKey(path))
		{
			return dicAlwas[path];
		}
		return null;
	}


}
