using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class GameUtil
{
    public static string GetPathWithoutExtension(string strPath)
    {
        int start = 0;
        int len = strPath.LastIndexOf(".") - start;
        string r = strPath.Substring(start, len);
        return r;
    }

    public static void CreateDir(string path)
    {
        string d = Path.GetDirectoryName(path);
        if (Directory.Exists(d) == false)
        {
            Directory.CreateDirectory(d);
        }
    }
    /// <summary>
    /// 获取指定目录的总大小
    /// </summary>
    public static long GetDirectorySize(DirectoryInfo dirInfo)
    {
        long num = 0L;
        foreach (FileInfo f in dirInfo.GetFiles())
        {
            num += f.Length;
        }
        foreach (DirectoryInfo d in dirInfo.GetDirectories())
        {
            num += GetDirectorySize(d);
        }
        return num;
    }
    //
    public static void DestroyAllChild(Transform tf)
    {
        while (tf.childCount > 0)
        {
            GameObject.Destroy(tf.GetChild(0).gameObject);
        }
    }
}
