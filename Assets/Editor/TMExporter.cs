using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Security.Cryptography;


public class TMExporter
{
    public const string EXT = ".ab";
    private static string PATH_HASH_EDITOR = Application.streamingAssetsPath + "/asset/files_editor.txt";
    private static Dictionary<string, string> dicHash = new Dictionary<string, string>();

    public static void BuildSelection()
    {
        UnityEngine.Object obj = Selection.activeObject;
        if (obj == null)
        {
            Debug.LogError("没有选择任何项");
            return;
        }
        string path = EditorUtility.SaveFilePanel("SaveFile", "", obj.name, EXT.Substring(1));
        if (string.IsNullOrEmpty(path))
            return;
        BuildOneAssetBundle(obj, path);
    }
    public static void BuildPrefab()
    {
        string dir = Application.dataPath + "/Resources/prefab/";
        string[] files = Directory.GetFiles(dir, "*.prefab", SearchOption.AllDirectories);
        List<BuildEntity> list = new List<BuildEntity>(files.Length);
        foreach (string f in files)
        {
            string shortName = GameUtil.GetPathWithoutExtension(f.Replace(dir, string.Empty).Replace("\\", "/"));
            BuildEntity entity = new BuildEntity();
            entity.Init("prefab/" + shortName, "Assets/prefab/" + shortName + ".prefab");
            //entity.Init("prefab/" + shortName, "Resources/prefab/" + shortName + ".prefab");
            list.Add(entity);
        }
        BuildList(list);
    }
    /// <summary>
    /// 导出所有的配置数据
    /// </summary>
    public static void BuildAllConfig()
    {
        string dir = Application.dataPath + "/config/csv/";
        string[] files = Directory.GetFiles(dir, "*.csv", SearchOption.AllDirectories);
        List<BuildEntity> list = new List<BuildEntity>(files.Length);
        List<string> listFilter = CSVFilter.GetConfigList();
        foreach (string f in files)
        {
            // 判定是否需要打包
            string fileNameNotExt = Path.GetFileNameWithoutExtension(f);
            if (listFilter.Contains(fileNameNotExt))
            {
                list.Add(BuildOneConfig(f));
            }
        }
        AssetDatabase.Refresh();
        BuildList(list);
        //foreach (BuildEntity entity in list)
        //{
        //	// 第四步、删除临时文件
        //File.Delete(entity.assetFullPath);
        //}
        AssetDatabase.Refresh();
    }

    private static BuildEntity BuildOneConfig(string fullPath)
    {
        // 第一步，读取CSV内容，得到UTF-8编码的字节数组
        string text = File.ReadAllText(fullPath, Encoding.Default);
        byte[] buff = Encoding.UTF8.GetBytes(text);
        // 第二步、将第一步中得到的字节数组写入utf-8编码的文本文件中
        string shortName = Path.GetFileNameWithoutExtension(fullPath);
        string path = Application.dataPath + "/config/txt/" + shortName + ".txt";
        GameUtil.CreateDir(path);
        FileStream fileStream = File.Create(path);
        fileStream.Write(buff, 0, buff.Length);
        fileStream.Close();
        // 第三步、用utf-8编码的临时文件进行AssetBundle生成
        BuildEntity entity = new BuildEntity();
        entity.Init("config/" + shortName, "Assets/config/txt/" + shortName + ".txt");
        return entity;
    }
    private static bool BuildOne(BuildEntity entity)
    {
        UnityEngine.Object mainAsset = AssetDatabase.LoadAssetAtPath(entity.assetPath, typeof(UnityEngine.Object));
        //entity.mainAsset = mainAsset;
        if (mainAsset == null)
        {
            Log.LogError_sys("加载不到:" + entity.assetPath);
            return false;
        }
        else
        {
            return BuildOneAssetBundle(mainAsset, entity.outPath);
        }
    }

    public static void BuildAll()
    {
        //BuildAllPlayer();
        //BuildAllEnemy();
        //BuildAllMap();
        //BuildAllVoice();
        //BuildAllWeapon();
        //BuildAllAtlas();
        BuildAllConfig();

        BuildFiles();

        //// 首先，清理本地的PATH_HASH_EDITOR
        //if (File.Exists(PATH_HASH_EDITOR))
        //    File.Delete(PATH_HASH_EDITOR);
        //// 编译差异项
        //BuildDifferences();

        // 拷贝新手资源
        //BuildTutRes(false);
    }

    public static void BuildTutRes(bool forceGen)
    {
        // 解析tutfiles.txt
        List<string> list = new List<string>();
        {
            string txtPath = Application.streamingAssetsPath + "/asset/tutfiles.txt";
            if (!File.Exists(txtPath))
            {
                Debug.LogError("指定文件不存在:" + txtPath);
                return;
            }
            // 读取所有的tut记录
            string content = File.ReadAllText(txtPath).Replace("\r\n", "\n");
            string[] lines = content.Split('\n');
            list = new List<string>(lines.Length);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                list.Add(line);
            }
            list.TrimExcess();
        }
        // 解析files.txt
        List<string> listAll = new List<string>();
        {
            string txtPath = Application.dataPath + "/../file/" + TMEditorTools.PLATFORM_FOLDER + "/files.txt";
            if (!File.Exists(txtPath))
            {
                Debug.LogError("指定文件不存在:" + txtPath);
                return;
            }
            // 读取所有的tut记录
            string content = File.ReadAllText(txtPath).Replace("\r\n", "\n");
            string[] lines = content.Split('\n');
            listAll = new List<string>(lines.Length);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                listAll.Add(line.Split(':')[0]);
            }
            listAll.TrimExcess();
        }

        // 源目录
        string srcDicPath = Application.dataPath + "/../file/" + TMEditorTools.PLATFORM_FOLDER + "/";
        // 目标目录
        string dicPath = Application.dataPath + "/../file/" + TMEditorTools.PLATFORM_FOLDER + "tut/";
        string dicOtherPath = Application.dataPath + "/../file/" + TMEditorTools.PLATFORM_FOLDER + "update/";
        // 清理目标目录
        if (Directory.Exists(dicPath))
            Directory.Delete(dicPath, true);
        Directory.CreateDirectory(dicPath);
        if (Directory.Exists(dicOtherPath))
            Directory.Delete(dicOtherPath, true);
        Directory.CreateDirectory(dicOtherPath);

        StringBuilder sbTut = new StringBuilder();
        StringBuilder sbUpload = new StringBuilder();
        bool isFirstTut = true;
        bool isFirstUpload = true;

        if (forceGen)
        {
            // 生成
        }
        else
        {
            foreach (string shortNameNotExt in listAll)
            {
                bool isTut = list.Contains(shortNameNotExt);

                string srcPath = srcDicPath + shortNameNotExt + EXT;
                string destPath = dicPath + shortNameNotExt + EXT;
                if (!isTut) destPath = dicOtherPath + shortNameNotExt + EXT;

                if (!File.Exists(srcPath))
                {
                    Debug.LogError("目标文件不存在---" + srcPath);
                }
                else
                {
                    // 确保目录存在
                    string dic = Path.GetDirectoryName(destPath);
                    if (!Directory.Exists(dic))
                        Directory.CreateDirectory(dic);

                    File.Copy(srcPath, destPath);

                    string md5Code = CalcMD5.CheckFile(destPath);
                    if (isTut)
                    {
                        if (!isFirstTut) sbTut.Append("\n");
                        sbTut.Append(string.Format("{0}:{1}", shortNameNotExt, md5Code));
                        isFirstTut = false;
                    }
                    else
                    {
                        if (!isFirstUpload) sbUpload.Append("\n");
                        sbUpload.Append(string.Format("{0}:{1}", shortNameNotExt, md5Code));
                        isFirstUpload = false;
                    }
                }
            }
        }

        // 在tut中写入files.txt
        {
            string filesTxtPath = dicPath + "files.txt";
            FileStream fileStream = File.Create(filesTxtPath);
            byte[] buff = Encoding.UTF8.GetBytes(sbTut.ToString());
            fileStream.Write(buff, 0, buff.Length);
            fileStream.Close();
        }
        // 拷贝出files-server.txt到tut目录
        {
            if (File.Exists(dicPath + "files-server.txt"))
                File.Delete(dicPath + "files-server.txt");
            if (File.Exists(srcDicPath + "files.txt"))
                File.Copy(srcDicPath + "files.txt", dicPath + "files-server.txt");
        }
        // 拷贝出files-server.txt到upload目录
        {
            if (File.Exists(dicOtherPath + "files-server.txt"))
                File.Delete(dicOtherPath + "files-server.txt");
            if (File.Exists(srcDicPath + "files.txt"))
                File.Copy(srcDicPath + "files.txt", dicOtherPath + "files-server.txt");
        }
    }

    public static void BuildFiles()
    {
        string filesSxdPath = Application.dataPath + "/../file/" + TMEditorTools.PLATFORM_FOLDER + "/files.sxd";
        if (File.Exists(filesSxdPath))
        {
            File.Delete(filesSxdPath);
        }
        string fileHashPath = Application.dataPath + "/../file/" + TMEditorTools.PLATFORM_FOLDER + "/files-hash.sxd";
        if (File.Exists(fileHashPath))
        {
            File.Delete(fileHashPath);
        }

        // 第一步、生成files.txt
        string dir = Application.dataPath + "/../file/" + TMEditorTools.PLATFORM_FOLDER + "/";
        string[] strArray = Directory.GetFiles(dir, "*.sxd", SearchOption.AllDirectories);
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < strArray.Length; i++)
        {
            string fullPath = strArray[i];
            string shortName = fullPath.Replace(dir, string.Empty).Replace("\\", "/").Replace(EXT, string.Empty);
            string line = string.Format("{0}:{1}", shortName, CalcMD5.CheckFile(fullPath));
            if (i == (strArray.Length - 1))
            {
                builder.Append(line);
            }
            else
            {
                builder.AppendLine(line);
            }
        }
        // 写入files.txt
        string filesTxtPath = Application.streamingAssetsPath + "/asset/" + "files.txt";
        using (FileStream fs = new FileStream(filesTxtPath, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.Write(builder.ToString());
                writer.Close();
            }
            fs.Close();
        }
        builder = null;
        AssetDatabase.Refresh();
        // 第二步，将fils.txt文件导出为sxd文件到对应平台目录下
        UnityEngine.Object mainAsset = AssetDatabase.LoadAssetAtPath("Assets/StreamingAssets/asset/files.txt", typeof(UnityEngine.Object));
        if (mainAsset == null)
        {
            Debug.LogError("资源为空，暂时还没有刷新资源库");
        }
        else
        {
            if (!BuildOneAssetBundle(mainAsset, filesSxdPath))
            {
                Debug.LogError("生成files.sxd文件失败");
            }
        }
        // 拷贝到平台目录下去
        string destFilesTxtPath = filesSxdPath.Replace(EXT, ".txt");
        if (File.Exists(destFilesTxtPath))
            File.Delete(destFilesTxtPath);
        File.Copy(filesTxtPath, filesSxdPath.Replace(EXT, ".txt"));
        File.Delete(filesTxtPath);
        AssetDatabase.Refresh();

        // 为files.sxd文件生成一个md5 hash文件
        using (FileStream fs = new FileStream(fileHashPath, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.Write(CalcMD5.CheckFile(filesSxdPath.Replace(EXT, ".txt")));
                writer.Close();
            }
            fs.Close();
        }
    }

    private static bool BuildList(List<BuildEntity> list)
    {
        bool ret = true;
        foreach (BuildEntity entity in list)
        {
            if (!BuildOne(entity))
            {
                // 一旦出错，就停止继续生成
                ret = false;
                break;
            }
        }
        return ret;
    }

    private static bool BuildOneAssetBundle(UnityEngine.Object mainAsset, string outPath)
    {
        return BuildPipeline.BuildAssetBundle(mainAsset, null, outPath,
             BuildAssetBundleOptions.CollectDependencies |
             BuildAssetBundleOptions.CompleteAssets |
             BuildAssetBundleOptions.DeterministicAssetBundle,
             TMEditorTools.BUILD_TARGET);
    }

    /// <summary>
    /// 为目录生成Hash
    /// </summary>
    public static void BuildHashFile(string folder = null)
    {
        if (folder == null)
        {
            // 选择一个目录
            folder = EditorUtility.OpenFolderPanel("请选择一个目录", null, null);
        }
        if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
        {
            Debug.LogError("指定目录不合法---" + folder);
            return;
        }
        // 遍历这个目录下所有的文件并计算它的短名称和MD5 Hash码
        string[] strArray = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
        StringBuilder builder = new StringBuilder();
        string parentFolder = new DirectoryInfo(folder).Parent.ToString().Replace("\\", "/") + "/";
        for (int i = 0; i < strArray.Length; i++)
        {
            string fullPath = strArray[i];
            string shortName = fullPath.Replace(parentFolder, string.Empty).Replace("\\", "/").Replace(Path.GetExtension(fullPath), string.Empty);
            string line = string.Format("{0}:{1}", shortName, CalcMD5.CheckFile(fullPath));
            if (i == (strArray.Length - 1))
            {
                builder.Append(line);
            }
            else
            {
                builder.AppendLine(line);
            }
        }
        // 写入files.txt
        string filesTxtPath = folder + "/../files.txt";
        using (FileStream fs = new FileStream(filesTxtPath, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.Write(builder.ToString());
                writer.Close();
            }
            fs.Close();
        }
        Debug.Log("生成完毕");
    }

    /// <summary>
    /// 导出差异性
    /// </summary>
    public static void BuildDifferences()
    {
        DateTime dtStart = DateTime.UtcNow;

        List<string> allAssetsList = new List<string>(200);
        FindAssets(allAssetsList, Application.streamingAssetsPath + "/asset/bundle/");
        FindAssets(allAssetsList, Application.streamingAssetsPath + "/asset/csv/");
        if (allAssetsList.Count == 0)
            return;
        // 读取本地已有的Hash文件表;
        ReadHash();
        // 新的Hash表，用来确保自动刷新files_editor，会及时清理没有资源的记录
        Dictionary<string, string> newHashMap = new Dictionary<string, string>(dicHash.Count == 0 ? 200 : dicHash.Count * 2);
        // 筛选出需要导出的差异项，记得对依赖项进行判断
        List<BuildEntity> list = new List<BuildEntity>(allAssetsList.Count);
        foreach (string fullPath in allAssetsList)
        {
            bool hasChanged = false;
            string key = fullPath.Replace(Application.dataPath, "Assets");//AssetPath
            // 抽取所有的依赖项(包括自身文件)
            string[] dependencies = AssetDatabase.GetDependencies(new string[] { key });
            // 对所有依赖项进行变更检测
            foreach (string dep in dependencies)
            {
                // FullPath
                string depFullPath = Application.dataPath + ("/" + dep).Replace("/Assets/", "/");
                string depNewHash = CalcMD5.CheckFile(depFullPath);
                string depOldHash = null;
                dicHash.TryGetValue(dep, out depOldHash);//请注意，depOldHash可能会为null
                // 检测是否有任何一个依赖项改变过
                if (!depNewHash.Equals(depOldHash))
                    hasChanged = true;
                // 考虑到某些依赖项被多个项所依赖，所以这里需要判定一下
                if (!newHashMap.ContainsKey(dep))
                    newHashMap.Add(dep, depNewHash);
            }
            // 如果没有任何改变，就继续下一次循环
            if (!hasChanged)
                continue;

            // 生成BuildEntity
            if (Path.GetExtension(key).ToLower().Trim().Equals(".csv"))
            {
                // 需要对csv进行特殊处理，生成临时的txt文件
                BuildEntity entity = BuildOneConfig(fullPath);
                list.Add(entity);
            }
            else
            {
                BuildEntity entity = new BuildEntity();
                list.Add(entity);
            }
        }
        if (list.Count == 0)
        {
            Debug.Log("没有任何资源变动");
        }
        else
        {
            // 对list进行遍历导出
            bool ret = true;
            foreach (BuildEntity entity in list)
            {
                if (!BuildOne(entity))
                {
                    Debug.LogError("导出资源项失败---" + entity.assetPath + "---导出工作中断");
                    ret = false;
                    break;
                }
            }
            if (ret)
            {
                SaveHash(newHashMap);
                newHashMap.Clear();
                BuildFiles();
                Debug.Log("导出差异项完毕！");
            }
        }
        AssetDatabase.Refresh();
        DateTime dtEnd = DateTime.UtcNow;
        TimeSpan span = dtEnd - dtStart;
        Debug.Log(string.Format("本次导出共耗时:{0}分{1}秒", span.Minutes, span.Seconds));
    }

    /// <summary>
    /// 查找指定目录下的所有资产，自动去掉.meta文件
    /// </summary>
    private static void FindAssets(List<string> list, string folder)
    {
        DirectoryInfo info = new DirectoryInfo(folder);
        if (info.Exists)
        {
            FileInfo[] files = info.GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo f in files)
            {
                string extName = Path.GetExtension(f.Name).ToLower();
                if (extName.Equals(".prefab") || extName.Equals(".png") ||
                    extName.Equals(".wav") || extName.Equals(".anim"))
                {
                    list.Add(f.FullName.Replace("\\", "/"));
                }
                else if (extName.Equals(".csv"))
                {
                    if (CSVFilter.GetConfigList().Contains(Path.GetFileNameWithoutExtension(f.Name)))
                        list.Add(f.FullName.Replace("\\", "/"));
                }
            }
        }
    }

    private static void ReadHash()
    {
        dicHash.Clear();
        if (File.Exists(PATH_HASH_EDITOR))
        {
            using (FileStream fs = new FileStream(PATH_HASH_EDITOR, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                {
                    string content = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        string[] lines = content.Replace("\r\n", "\n").Split('\n');
                        foreach (string line in lines)
                        {
                            if (string.IsNullOrEmpty(line))
                                continue;
                            string[] datas = line.Split(':');
                            dicHash.Add(datas[0], datas[1]);
                        }
                    }
                }
            }
        }
    }

    private static void SaveHash(Dictionary<string, string> map)
    {
        StringBuilder builder = new StringBuilder();
        int i = 0;
        foreach (KeyValuePair<string, string> pair in map)
        {
            if (++i == dicHash.Count)
            {
                builder.Append(pair.Key + ":" + pair.Value);
            }
            else
            {
                builder.Append(pair.Key + ":" + pair.Value + "\n");
            }
        }
        using (FileStream fs = new FileStream(PATH_HASH_EDITOR, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            fs.Write(data, 0, data.Length);
            fs.Close();
        }
    }

    private static bool TryChangeHash(string assetsPath)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] buff = md5.ComputeHash(File.OpenRead(assetsPath));
        string hashValue = Encoding.UTF8.GetString(buff);
        if (dicHash.ContainsKey(assetsPath))
        {
            if (hashValue.Equals(dicHash[assetsPath]))
            {
                return false;
            }
            else
            {
                dicHash[assetsPath] = hashValue;
                return true;
            }
        }
        else
        {
            dicHash.Add(assetsPath, hashValue);
            return true;
        }
    }

    private class BuildEntity
    {
        public BuildEntity()
        {
        }
        //public UnityEngine.Object mainAsset;
        string name;
        public string assetPath;
        public string outPath;

        internal void Init(string name, string assetPath)
        {
            this.name = name;
            this.assetPath = assetPath;
            outPath = TMEditorTools.GetBundlePath(name);
        }
    }

}
