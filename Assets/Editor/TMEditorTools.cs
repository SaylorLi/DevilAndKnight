using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TMEditorTools
{
    public static readonly string PLATFORM_FOLDER = string.Empty;
    public static readonly BuildTarget BUILD_TARGET = BuildTarget.StandaloneWindows;

    static TMEditorTools()
    {
#if UNITY_IPHONE
            //PLATFORM_FOLDER = "ios";
            BUILD_TARGET = BuildTarget.iPhone;
#elif UNITY_ANDROID
        //PLATFORM_FOLDER = "android";
        BUILD_TARGET = BuildTarget.Android;
#else
            //PLATFORM_FOLDER = "win";
            BUILD_TARGET = BuildTarget.StandaloneWindows;
#endif
    }

    public static bool ButtonNormal(string text)
    {
        return GUILayout.Button(text,
            GUILayout.MinWidth(80), GUILayout.MaxWidth(100),
            GUILayout.MinHeight(35), GUILayout.MaxHeight(45));
    }

    public static void SpaceNormal()
    {
        GUILayout.Space(10);
    }

    public static string GetFullPath(string assetPath)
    {
        return Application.dataPath + "/../" + assetPath;
    }
    /// <summary>
    /// Bundle输出路径
    /// </summary>
    public static string GetBundlePath(string assetPath)
    {
        string dir = Application.streamingAssetsPath + "/asset/bundle/";
        return dir + assetPath + TMExporter.EXT; 
    }


}
