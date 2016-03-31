using UnityEngine;
using UnityEditor;

using System.IO;
using System.Collections.Generic;


public class TMMaker
{
    #region 角色

    public static void MakerCharaMaterial()
    {
        UnityEngine.Object[] selections = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (selections == null || selections.Length == 0) return;
        foreach (UnityEngine.Object obj in selections)
        {
            // 预制件的PrefabType是Prefab,Model在AssetDabase中是ModelPrefab
            if (obj is Texture)
            {
                string assetPath = AssetDatabase.GetAssetPath(obj).ToLower();
                string texName = obj.name;
                //目前只支持这两种
                if (texName.StartsWith("ch_") || texName.StartsWith("en_"))
                {
                    if (!MakerOneCharaMaterial(assetPath))
                    {
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 为指定模型生成材质,根据贴图来生成材质球
    /// </summary>
    private static bool MakerOneCharaMaterial(string texAssetPath)
    {
        // 计算材质球的文件路径
        string matPath = texAssetPath.Replace(Path.GetExtension(texAssetPath), ".mat");
        Material material = AssetDatabase.LoadAssetAtPath(matPath, typeof(Material)) as Material;
        Material prefabMaterial = AssetDatabase.LoadAssetAtPath("Assets/Art/_Template/Character.mat", typeof(Material)) as Material;
        if (prefabMaterial == null)
        {
            Debug.LogError("材质球模板丢失!(Assets/Art/_Template/Character)");
            return false;
        }
        material = new Material(prefabMaterial);
        AssetDatabase.CreateAsset(material, matPath);
        //if (material == null)
        //{
        //    // 从模板创建
        //    material = new Material(prefabMaterial);
        //    AssetDatabase.CreateAsset(material, matPath);
        //}
        //else
        //{
        //    // 重置参数
        //    material.CopyPropertiesFromMaterial(prefabMaterial);
        //}

        // 绑定贴图
        Texture tex = AssetDatabase.LoadAssetAtPath(texAssetPath, typeof(Texture)) as Texture;
        if (tex != null)
        {
            material.mainTexture = tex;
        }
        else
        {
            Debug.LogWarning("贴图未给定!(" + texAssetPath + ")");
            return false;
        }
        AssetDatabase.Refresh();
        return true;
    }

    public static void MakerCharaPrefab()
    {
        UnityEngine.Object[] selections = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (selections == null || selections.Length == 0) return;
        foreach (UnityEngine.Object obj in selections)
        {
            if (obj is Material)
            {
                string matAssetPath = AssetDatabase.GetAssetPath(obj).ToLower();
                string matName = Path.GetFileName(matAssetPath);
                //目前只支持这两种
                if (matName.StartsWith("ch_"))
                {
                    if (!MakerOneCharaPrefab(matAssetPath, matName, true))
                    {
                        break;
                    }
                }
                else if (matName.StartsWith("en_"))
                {
                    if (!MakerOneCharaPrefab(matAssetPath, matName, false))
                    {
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 为指定材质球生成预制件
    /// </summary>
    private static bool MakerOneCharaPrefab(string matAssetPath, string matName, bool isChar)
    {
        string fbxAssetPath = string.Empty;//FBX资源路径
        string fbxName = string.Empty;
        int tailIndex = matName.LastIndexOf("_");
        if (tailIndex > 2)
        {
            // 有尾缀
            fbxName = matName.Substring(0, tailIndex);
            fbxAssetPath = Path.GetDirectoryName(matAssetPath) + "/" + fbxName + ".fbx";
        }
        else
        {
            // 无尾缀
            fbxName = Path.GetFileNameWithoutExtension(matName);
            fbxAssetPath = matAssetPath.Replace(".mat", ".fbx");
        }
        // Prefab资源路径
        string prefabPath = "Assets/StreamingAssets/asset/bundle/enemy/" + matName.Replace(".mat", ".prefab");
        if (isChar)
        {
            prefabPath = "Assets/StreamingAssets/asset/bundle/char/" + matName.Replace(".mat", ".prefab");
        }
        //string prefabPath = Application.streamingAssetsPath + "/asset/bundle/char/" + fbxName + ".prefab";
        GameObject modelPrefab = AssetDatabase.LoadAssetAtPath(fbxAssetPath, typeof(GameObject)) as GameObject;
        GameObject go = UnityEngine.Object.Instantiate(modelPrefab) as GameObject;
        // 检测动画类型
        Animation anim = go.GetComponent<Animation>();
        if (anim == null)
        {
            Debug.LogError("动画组件不存在---" + fbxAssetPath);
            GameObject.DestroyImmediate(go);
            return false;
        }
        else
        {
            anim.playAutomatically = false;
            anim.cullingType = AnimationCullingType.AlwaysAnimate;
        }
        SkinnedMeshRenderer renderer = go.GetComponentInChildren<SkinnedMeshRenderer>();
        if (renderer != null)
        {
            renderer.receiveShadows = false;//不接受阴影
            renderer.castShadows = false;//不产生阴影
        }
        Material material = AssetDatabase.LoadAssetAtPath(matAssetPath, typeof(Material)) as Material;
        if (material != null)
        {
            renderer.material = material;
        }
        else
        {
            Debug.LogError("材质球未给定!(" + matName + ")");
            GameObject.DestroyImmediate(go);
            return false;
        }
        // Apply
        PrefabUtility.CreatePrefab(prefabPath, go, ReplacePrefabOptions.ConnectToPrefab);
        // Delete
        GameObject.DestroyImmediate(go);
        AssetDatabase.Refresh();
        return true;
    }

    public static void MakerCharaAnimation()
    {
        UnityEngine.Object[] selections = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (selections == null || selections.Length == 0) return;
        for (int i = 0; i < selections.Length; i++)
        {
            UnityEngine.Object obj = selections[i];
            if (obj == null) continue;
            // 预制件的PrefabType是Prefab,Model在AssetDabase中是ModelPrefab
            if (obj is GameObject && PrefabType.ModelPrefab == PrefabUtility.GetPrefabType(obj))
            {
                string assetPath = AssetDatabase.GetAssetPath(obj).ToLower();
                string fName = Path.GetFileNameWithoutExtension(assetPath);
                //目前只支持an_开头的文件
                if (fName.StartsWith("an_"))
                {
                    if (!MakerOneCharaAnimation(assetPath, fName))
                    {
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 根据配置剥离出指定模型中的所有动作并抽取出来存放在指定目录
    /// </summary>
    /// <param name="fbxPath"></param>
    private static bool MakerOneCharaAnimation(string fbxPath, string fbxShortName)
    {
        // 配置文件目录
        string configPath = Path.GetDirectoryName(fbxPath);
        if (fbxShortName.EndsWith("_l"))
        {
            configPath = configPath + "/MotionConfig_l.csv"; ;
        }
        else if (fbxShortName.EndsWith("_r"))
        {
            configPath = configPath + "/MotionConfig_r.csv"; ;
        }
        else
        {
            Debug.LogError("无法确定是左手还是右手");
            return false;
        }
        if (!File.Exists(configPath))
        {
            Debug.LogError("指定配置文件不存在!---" + configPath);
            return false;
        }
        // 读取配置文件
        List<ModelImporterClipAnimation> list = GetClipsByCSV(configPath);
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("配置文件没有记录!---" + configPath);
            return false;
        }
        // 输出目录，去掉an_human_r中的an_
        // string outFolder = Application.streamingAssetsPath + "/Asset/bundle/motion/" + fbxShortName.Substring(3);
        string outFolder = Application.dataPath + "/Resources/bundle/motion/" + fbxShortName.Substring(3);
        //string outFolder = Application.streamingAssetsPath + "/asset/bundle/motion/" + fbxShortName.Substring(3);
        if (!Directory.Exists(outFolder))
        {
            Directory.CreateDirectory(outFolder);
        }
        // 对模型进行动画切割
        ModelImporter mImporter = AssetImporter.GetAtPath(fbxPath) as ModelImporter;
        mImporter.clipAnimations = list.ToArray();
        AssetDatabase.Refresh();
        // 提取文件中所有的对象
        UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(fbxPath);
        // 有效文件，本次生成中的有效文件
        List<string> validList = new List<string>();
        foreach (UnityEngine.Object asset in assets)
        {
            if (asset is AnimationClip)
            {
                // 经探测，通常都会有一个__preview__Take 001这样一个文件保存全部的动作数据
                if (asset.name.StartsWith("__preview__Take"))
                    continue;
                string outFullPath = outFolder + "/" + asset.name + ".anim";
                string outAssetPath = outFullPath;
                if (File.Exists(outFullPath))
                {
                    // 如果存在就使用旧版的文件并替换内容
                    AnimationClip clip = AssetDatabase.LoadAssetAtPath(outAssetPath, typeof(AnimationClip)) as AnimationClip;
                    EditorUtility.CopySerialized(asset, clip);//拷贝字节
                }
                else
                {
                    // 如果不存在就新建并拷贝字节
                    AnimationClip clip = new AnimationClip();
                    EditorUtility.CopySerialized(asset, clip);//拷贝字节
                    AssetDatabase.CreateAsset(clip, outAssetPath);
                }
                // 记录新建
                validList.Add(outFullPath);
            }
        }
        // 删除垃圾文件
        string[] files = Directory.GetFiles(outFolder, "*.anim");
        foreach (string f in files)
        {
            string newF = f.Replace(@"\", "/");
            if (validList.Contains(newF))
                continue;
            // 删掉无效文件，无效的文件视为垃圾
            File.Delete(newF);
        }
        AssetDatabase.Refresh();
        return true;
    }

    private static List<ModelImporterClipAnimation> GetClipsByCSV(string csvPath)
    {
        List<ModelImporterClipAnimation> list = null;
        using (FileStream fs = new FileStream(csvPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 2048))
        {
            StreamReader reader = new StreamReader(fs, System.Text.Encoding.Default);
            // 读入CSV文件中所有的内容
            string text = reader.ReadToEnd().Replace("\r\n", "\n").Trim();
            string[] arr = text.Split('\n');
            if (arr == null || arr.Length < 1)
            {
                Debug.LogError("不合法的CSV文件:" + csvPath);
                return null;
            }
            list = new List<ModelImporterClipAnimation>(arr.Length - 1);
            for (int i = 1, len = arr.Length; i < len; i++)
            {
                // 过滤掉空行
                if (string.IsNullOrEmpty(arr[i]))
                    continue;
                string[] tmpArr = arr[i].Split(',');//拆分记录
                ModelImporterClipAnimation clip = new ModelImporterClipAnimation();
                // 绑定信息
                clip.name = tmpArr[0];
                clip.firstFrame = float.Parse(tmpArr[1]);
                clip.lastFrame = float.Parse(tmpArr[2]);
                clip.wrapMode = "1".Equals(tmpArr[3]) ? WrapMode.Loop : WrapMode.Default;
                list.Add(clip);
            }
            reader.Close();
        }
        return list;
    }

    #endregion

    public static void MakerWeaponMaterial()
    {
        UnityEngine.Object[] selections = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (selections == null || selections.Length == 0) return;
        foreach (UnityEngine.Object obj in selections)
        {
            if (obj is Texture)
            {
                string assetPath = AssetDatabase.GetAssetPath(obj).ToLower();
                string texName = obj.name;
                //目前只支持这两种
                if (texName.StartsWith("wp_"))
                {
                    if (!MakerOneWeaponMaterial(assetPath))
                    {
                        break;
                    }
                }
            }
        }
    }

    private static bool MakerOneWeaponMaterial(string texAssetPath)
    {
        // 计算材质球的文件路径
        string matPath = texAssetPath.Replace(Path.GetExtension(texAssetPath), ".mat");
        Material material = AssetDatabase.LoadAssetAtPath(matPath, typeof(Material)) as Material;
        Material prefabMaterial = AssetDatabase.LoadAssetAtPath("Assets/Art/_Template/Weapon.mat", typeof(Material)) as Material;
        if (prefabMaterial == null)
        {
            Debug.LogError("材质球模板丢失!(Assets/Editor/Template/Weapon)");
            return false;
        }
        //if (material == null)
        //{
        //    // 从模板创建
        //    material = new Material(prefabMaterial);
        //    AssetDatabase.CreateAsset(material, matPath);
        //}
        //else
        //{
        //    // 重置参数
        //    material.CopyPropertiesFromMaterial(prefabMaterial);
        //}

        // 从模板创建
        material = new Material(prefabMaterial);
        AssetDatabase.CreateAsset(material, matPath);

        // 绑定贴图
        Texture tex = AssetDatabase.LoadAssetAtPath(texAssetPath, typeof(Texture)) as Texture;
        if (tex != null)
        {
            material.mainTexture = tex;
        }
        else
        {
            Debug.LogWarning("贴图未给定!(" + texAssetPath + ")");
            return false;
        }
        AssetDatabase.Refresh();
        return true;
    }

    public static void MakerWeaponPrefab()
    {
        UnityEngine.Object[] selections = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (selections == null || selections.Length == 0) return;
        for (int i = 0; i < selections.Length; i++)
        {
            UnityEngine.Object obj = selections[i];
            if (obj is Material)
            {
                string assetPath = AssetDatabase.GetAssetPath(obj).ToLower();
                string fName = Path.GetFileNameWithoutExtension(assetPath);
                // 支持wp_开头
                if (fName.StartsWith("wp_"))
                {
                    if (!MakerOneWeaponPrefab(assetPath))
                    {
                        break;
                    }
                }
            }
        }
    }

    private static bool MakerOneWeaponPrefab(string matAssetPath)
    {
        string prefabPath = matAssetPath.Replace(".mat", ".prefab");
        prefabPath = prefabPath.Replace("/art/", "/StreamingAssets/asset/bundle/");

        string fullPath = Application.dataPath + "/../" + prefabPath;
        string dir = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        // 创建预制件
        Material material = AssetDatabase.LoadAssetAtPath(matAssetPath, typeof(Material)) as Material;
        if (material == null)
        {
            Debug.LogError("材质球不存在---" + matAssetPath);
            return false;
        }
        string fbxAssetPath = matAssetPath.Replace(".mat", ".fbx");
        // 设置预制件
        GameObject go = UnityEngine.Object.Instantiate(AssetDatabase.LoadAssetAtPath(fbxAssetPath, typeof(GameObject))) as GameObject;
        MeshRenderer renderer = go.GetComponentInChildren<MeshRenderer>();
        renderer.castShadows = false;
        renderer.receiveShadows = false;
        renderer.material = material;
        PrefabUtility.CreatePrefab(prefabPath, go, ReplacePrefabOptions.ConnectToPrefab);
        GameObject.DestroyImmediate(go);
        AssetDatabase.Refresh();
        return true;
    }

    public static void MakerNGUIAtlas()
    {
        UnityEngine.Object[] selections = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (selections.Length == 0)
            return;
        foreach (UnityEngine.Object obj in selections)
        {
            if (obj is Texture)
            {
                MakerOneNGUIAtlas((Texture)obj, null);
            }
        }
    }

    public static void MakerCardAtlas()
    {
        UnityEngine.Object[] selections = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        foreach (UnityEngine.Object obj in selections)
        {
            Texture tex = obj as Texture;
            if (tex != null)
            {
                string texAssetPath = AssetDatabase.GetAssetPath(tex);
                if (!texAssetPath.StartsWith("Assets/Art/atlas"))
                {
                    Debug.LogError("该文件不属于角色卡牌图集资源");
                    return;
                }
                // 由于小头像是全部打在一个图集中，所以不用批量生成
                if ("head_char".Equals(obj.name))
                {
                    continue;
                }
                // 读取模板TP内容
                string texFullPath = Application.dataPath + "/../" + texAssetPath;
                string texFullDir = Path.GetDirectoryName(texFullPath);
                StreamReader reader = new StreamReader(File.OpenRead(texFullDir + "/_template.txt"));
                string templateContent = reader.ReadToEnd();
                reader.Close();
                // 生成TP文件
                string tpContent = templateContent.Replace("--TEMPLATE--", tex.name);
                string tpAssetPath = texAssetPath.Replace(".png", ".txt");
                string tpFullPath = Application.dataPath + "/../" + tpAssetPath;
                StreamWriter writer = new StreamWriter(new FileStream(tpFullPath,
                    FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read, 8192), System.Text.Encoding.Default);
                writer.Write(tpContent);
                writer.Flush();
                writer.Close();
                AssetDatabase.Refresh();
                // 保证输出目录存在 
                string atlasAssetPath = texAssetPath.Replace("/Art/", "/StreamingAssets/asset/bundle/").Replace(".png", ".prefab");
                string atlasFullPath = Application.dataPath + "/../" + atlasAssetPath;
                string dir = Path.GetDirectoryName(atlasFullPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                // Texture和TP都准备好了，调用单个生成
                MakerOneNGUIAtlas((Texture)tex, atlasAssetPath);
            }
        }
    }

    private static bool MakerOneNGUIAtlas(Texture tex, string atlasAssetPath)
    {
        string nameWithoutExt = tex.name;
        string texAssetPath = AssetDatabase.GetAssetPath(tex).ToLower();
        // 第一步，找到对应的TP文件
        TextAsset tp = AssetDatabase.LoadAssetAtPath(texAssetPath.Replace(".png", ".txt"), typeof(TextAsset)) as TextAsset; ;
        if (tp == null)
        {
            Debug.LogError("没有找到对应的TP文件---" + texAssetPath);
            return false;
        }
        // 第二步，创建材质球绑定好材质球和纹理
        string matAssetPath = texAssetPath.Replace(".png", ".mat");
        Material mat = AssetDatabase.LoadAssetAtPath(matAssetPath, typeof(Material)) as Material;
        if (mat == null)
        {
            mat = new Material(Shader.Find("Unlit/Transparent Colored"));
            AssetDatabase.CreateAsset(mat, matAssetPath);
            AssetDatabase.Refresh();
        }
        mat.mainTexture = tex;
        // 第三步，创建GameObjec，设置好UIAtals组件
        GameObject go = new GameObject(nameWithoutExt);
        UIAtlas comp = go.AddComponent<UIAtlas>();
        comp.spriteMaterial = mat;

        //Saylor Mod 2016/3/31 S
        //NGUIEditorTools.ImportTexture(mat.mainTexture, false, false);
        NGUIEditorTools.ImportTexture(mat.mainTexture, false, false,true);
        //Saylor Mod 2016/3/31 E

        NGUIJson.LoadSpriteData(comp, tp);

        //Saylor Mod 2016/3/31 S
        //comp.MarkAsDirty();
        comp.MarkAsChanged();
        //Saylor Mod 2016/3/31 E

        // 第四步，生成预制件
        if (string.IsNullOrEmpty(atlasAssetPath))
        {
            // 本目录
            atlasAssetPath = texAssetPath.Replace(".png", ".prefab");
        }
        PrefabUtility.CreatePrefab(atlasAssetPath, go, ReplacePrefabOptions.ConnectToPrefab);
        // 第五步，销毁atlas
        GameObject.DestroyImmediate(go);
        // 第六步，刷新AssetDatabase
        AssetDatabase.ImportAsset(texAssetPath);
        AssetDatabase.Refresh();
        return true;
    }

}
