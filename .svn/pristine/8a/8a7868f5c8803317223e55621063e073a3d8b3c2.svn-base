//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using System.Runtime.Serialization;
//using UnityEditor;
//using UnityEngine;

////将数据包文件打包
//#if UNITY_EDITOR
//class BuildBundleConfig
//{
//		public const string LOCAL_RES_DIRECTORY = "Assets/StreamingAssets/asset/bundle/";
//	[MenuItem("[Build]/Build Config [Now]")]
//	public static void BuildConfigNow()
//	{
//		BuildConfig(UnityEditor.EditorUserBuildSettings.activeBuildTarget);
//	}
//	[MenuItem("[Build]/Empty0")]
//	public static void Empty0()
//	{
//	}
//	[MenuItem("[Build]/Build 的Config [Windows]")]
//	public static void BuildConfigWindows()
//	{
//		BuildConfig(BuildTarget.StandaloneWindows);
//	}
//	[MenuItem("[Build]/Empty1")]
//	public static void Empty1()
//	{
//	}
//	[MenuItem("[Build]/Build Config [Andriod]")]
//	public static void BuildConfigAndriod()
//	{
//		BuildConfig(BuildTarget.Android);
//	}
//	[MenuItem("[Build]/Empty2")]
//	public static void Empty2()
//	{
//	}
//	[MenuItem("[Build]/Build Config [IPhone]")]
//	public static void BuildConfigIPhone()
//	{
//		BuildConfig(BuildTarget.iPhone);
//	}
//	public static void BuildConfig(BuildTarget buildTarget)
//	{
//		UnityEngine.Object[] objects = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);

//		foreach (UnityEngine.Object obj in objects)
//		{
//			string path = AssetDatabase.GetAssetPath(obj);

//			if (!path.Contains(".csv"))
//			{
//				Debug.Log(path);
//				continue;
//			}
//			CsvReader csvReader = new CsvReader(path);
//			if (!csvReader.Read())
//			{
//				Debug.LogError("CsvReader error " + path);
//				continue;
//			}
//			string FileName = Path.GetFileNameWithoutExtension(path);
//			CsvScriptableObject csvSO = ScriptableObject.CreateInstance<CsvScriptableObject>();
//			csvSO.content = csvReader.content;
//			//
//			string p = "Assets" + Path.DirectorySeparatorChar + FileName + ".asset";
//			AssetDatabase.CreateAsset(csvSO, p);
//			UnityEngine.Object o = AssetDatabase.LoadAssetAtPath(p, typeof(CsvScriptableObject));
//			//
//			string dest = LOCAL_RES_DIRECTORY + "config/" + FileName + ".unity3d";
//			//dest = dest.ToLower();
//			Common.CreatePath(dest);
//			//o.name = Path.GetFileName(path);
//			//输出
//			if (BuildPipeline.BuildAssetBundle(o, null, dest
//				, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, buildTarget)
//				)
//			{
//				Debug.Log(buildTarget + " Path = " + dest);
//				//EncryptionAssetBundle.Encryption(dest);
//			}

//			AssetDatabase.DeleteAsset(p);
//		}
//	}
//	//----------------
//	[MenuItem("[Build]/Empty3")]
//	public static void Empty3()
//	{
//	}
//	[MenuItem("[Build]/Build Atlas [Windows]")]
//	public static void BuildAtlasWindows()
//	{
//		BuildAtlas(BuildTarget.StandaloneWindows);
//	}
//	[MenuItem("[Build]/Empty4")]
//	public static void Empty4()
//	{
//	}
//	[MenuItem("[Build]/Build Atlas [Andriod]")]
//	public static void BuildAtlasAndriod()
//	{
//		BuildAtlas(BuildTarget.Android);
//	}
//	[MenuItem("[Build]/Empty5")]
//	public static void Empty5()
//	{
//	}
//	[MenuItem("[Build]/Build Atlas [IPhone]")]
//	public static void BuildAtlasIPhone()
//	{
//		BuildAtlas(BuildTarget.iPhone);
//	}
//	static void BuildAtlas(BuildTarget buildTarget)
//	{
//		foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets))
//		{
//			string path = AssetDatabase.GetAssetPath(obj);
//			if (path.Contains(".prefab"))
//			{
//				//int index = path.LastIndexOf("/", path.Length - 1, path.Length);
//				//string FileName = Path.GetFileNameWithoutExtension(path);
//				string dest = path.Replace("Resources", "StreamingAssets").Replace(".prefab", ".atlas");
//				//string dest = LOCAL_RES_DIRECTORY + path.Substring(index, path.Length - 1) + FileName + ".atlas";
//				Common.CreatePath(dest);
//				//tmp.name = Path.GetFileName(path);
//				//Debug.Log("Path.GetFileName(path)  " + Path.GetFileName(path));
//				if (BuildPipeline.BuildAssetBundle(obj, null, dest, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, buildTarget))
//				{
//					Debug.Log("buildTarget " + buildTarget + " " + dest);
//					//EncryptionAssetBundle.Encryption(path);
//				}

//			}

//		}

//	}
//}




//#endif