using System;
using System.Collections.Generic;
using System.IO;


public static class HashManager
{
    // 每次启动时从文件服务器上拉取的Hash文件
    private static Dictionary<string, string> serverHash;
    // 本地的Hash文件内容
    private static Dictionary<string, string> localHash;

    private static int needToFlush = 0;

    // 本地Hash文件的路径
    private static readonly string localHashPath;

    static HashManager()
    {
        localHashPath = BundleManager.pathLocal + "files.txt";
        localHash = new Dictionary<string, string>();
        serverHash = new Dictionary<string, string>();
    }

    /// <summary>
    /// 处理服务器的files.sxd文件
    /// </summary>
    public static void ProcessServerHash(string hashText)
    {
        serverHash = ParseHash(ref hashText);
        LoadLocalHash();
    }

    /// <summary>
    /// 加载本地的Hash表
    /// </summary>
    private static void LoadLocalHash()
    {
        if (File.Exists(localHashPath))
        {
            string localText = File.ReadAllText(localHashPath, System.Text.Encoding.UTF8);
            localHash = ParseHash(ref localText);
        }
    }

    /// <summary>
    /// 更新本地的files.txt
    /// </summary>
    public static void UpdateLocalHash()
    {
        if (needToFlush > 0)
        {
            using (FileStream fs = new FileStream(localHashPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                using (StreamWriter writer = new StreamWriter(fs, System.Text.Encoding.UTF8))
                {
                    foreach (KeyValuePair<string, string> pair in localHash)
                    {
                        string line = pair.Key + ":" + pair.Value;
                        writer.WriteLine(line);
                    }
                    writer.Flush();
                    writer.Close();
                }
                fs.Close();
            }
            needToFlush = 0;
        }
    }

    /// <summary>
    /// 检测本地文件是否过期
    /// </summary>
    public static bool IsLocalHashExpired(string fileName)
    {
        //if (Game.Ins != null && Game.Ins.isIgnoreHash)
        //{
        //	return false;
        //}
        if (!serverHash.ContainsKey(fileName))
        {
            //Log.Log_hjx("IsLocalHashExpired 服务器文件列表里没有这个文件 " + fileName);
            return false;
        }
        if (localHash.ContainsKey(fileName))
        {
            //Log.Log_hjx("IsLocalHashExpired 本地文件列表里有这个文件 " + fileName + " 过期==" + (localHash[fileName] != serverHash[fileName]));
            return localHash[fileName] != serverHash[fileName];
        }
        //Log.Log_hjx("IsLocalHashExpired " + fileName + " 过期==true");
        return true;
    }

    /// <summary>
    /// 校验文件内容是否合法
    /// </summary>
    public static bool IsFileHashValid(string fileName, byte[] data)
    {
        // 从服务器上下载files.sxd文件时通过
        if (fileName.EndsWith("files") || fileName.EndsWith("files-hash")) return true;

        string hashText = CalcMD5.CheckData(data);
        if (localHash.ContainsKey(fileName) && localHash[fileName] == hashText)
        {
            return true;
        }
        if (serverHash.ContainsKey(fileName) && serverHash[fileName] == hashText)
        {
            if (localHash.ContainsKey(fileName))
            {
                localHash[fileName] = hashText;
            }
            else
            {
                localHash.Add(fileName, hashText);
            }
            needToFlush++;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 解析Hash数据
    /// </summary>
    private static Dictionary<string, string> ParseHash(ref string text)
    {
        text = text.Replace("\r\n", "\n");
        string[] lines = text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        char[] separator = new char[] { ':' };
        Dictionary<string, string> dic = new Dictionary<string, string>(lines.Length);
        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line))
                continue;
            string[] arr = line.Split(separator);
            if (arr.Length != 2)
                continue;
            if (dic.ContainsKey(arr[0])) dic[arr[0]] = arr[1];
            else dic.Add(arr[0], arr[1]);
        }
        return dic;
    }

    /// <summary>
    /// 获取服务器上的files.sxd文件内容
    /// </summary>
    public static Dictionary<string, string> ServerHash
    {
        get
        {
            return serverHash;
        }
    }
}
