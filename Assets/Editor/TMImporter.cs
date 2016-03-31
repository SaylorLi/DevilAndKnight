using UnityEngine;
using UnityEditor;

using System.IO;
using System.Collections;

/// <summary>
/// 资源自动导入脚本，自动化设计导入资源的一些设置信息，减少美术的工作量
/// </summary>
public class TMImporter : AssetPostprocessor
{
    /// <summary>
    /// 模型载入之前
    /// </summary>
    void OnPreprocessModel()
    {
        string fileName = Path.GetFileNameWithoutExtension(assetPath);
        // 角色、怪物、带动画工具模型
        if (fileName.StartsWith("ch_") ||
            fileName.StartsWith("en_") ||
            fileName.StartsWith("an_"))
        {
            ModelImporter importer = assetImporter as ModelImporter;
            importer.importMaterials = false;//不自动导入材质球
            //importer.animationType = ModelImporterAnimationType.Legacy;//默认动画类型为Legacy
            importer.isReadable = false;
        }
        else if (fileName.StartsWith("wp_"))
        {
            ModelImporter importer = assetImporter as ModelImporter;
            importer.importMaterials = false;//不自动导入材质球
            //importer.animationType = ModelImporterAnimationType.None;//默认动画类型为None
            importer.isReadable = false;
        }
        else
        {
            // 无动画设置
            ModelImporter mImporter = assetImporter as ModelImporter;
            //mImporter.animationType = ModelImporterAnimationType.None;
        }
    }

    /// <summary>
    /// 模型载入之后
    /// </summary>
    void OnPostprocessModel(GameObject root)
    {

    }

    /// <summary>
    /// 载入纹理之前
    /// </summary>
    void OnPreprocessTexture()
    {
        // || assetPath.ToLower().StartsWith("assets/art/weapon/")
        //if (assetPath.ToLower().StartsWith("assets/art/char/") ||
        //    assetPath.ToLower().StartsWith("assets/art/enemy/"))
        //{
        //    string tempAssetPath = "assets/art/_Template/CharaTex.png";
        //    TextureImporter prefabImporter = TextureImporter.GetAtPath(tempAssetPath) as TextureImporter;
        //    if (prefabImporter == null)
        //    {
        //        Debug.LogError("指定模板文件不存在---" + tempAssetPath);
        //        return;
        //    }
        //    TextureImporterSettings prefabSettings = new TextureImporterSettings();
        //    prefabImporter.ReadTextureSettings(prefabSettings);

        //    TextureImporterSettings settings = new TextureImporterSettings();
        //    prefabSettings.CopyTo(settings);

        //    TextureImporter importer = assetImporter as TextureImporter;
        //    importer.SetTextureSettings(settings);
        //}
    }

    /// <summary>
    /// 载入纹理之后
    /// </summary>
    /// <param name="texture">载入的纹理对象</param>
    void OnPostprocessTexture(Texture2D texture)
    {
        if (assetPath.StartsWith("Assets/Art/atlas/"))
        {
            if (assetPath.EndsWith("head_char.png")) return;
            TextureImporter importer = assetImporter as TextureImporter;
            importer.textureType = TextureImporterType.GUI;
            importer.filterMode = FilterMode.Bilinear;
            importer.maxTextureSize = GetTextureMaxSize(texture);
            //importer.textureFormat = TextureImporterFormat.AutomaticCompressed;
        }
    }

    public static int GetTextureMaxSize(Texture2D tex)
    {
        int width = tex.width;
        int height = tex.height;
        int maxValue = Mathf.Max(width, height);
        if (maxValue <= 32)
        {
            return 32;
        }
        else if (maxValue <= 64)
        {
            return 64;
        }
        else if (maxValue <= 128)
        {
            return 128;
        }
        else if (maxValue <= 256)
        {
            return 256;
        }
        else if (maxValue <= 512)
        {
            return 512;
        }
        else if (maxValue <= 1024)
        {
            return 1024;
        }
        else if (maxValue <= 2048)
        {
            return 2048;
        }
        return 4096;
    }

    /// <summary>
    /// 载入音频之前
    /// </summary>
    void OnPreprocessAudio()
    {
        AudioImporter importer = assetImporter as AudioImporter;
        importer.threeD = false;
        //importer.loopable = true;
    }

    /// <summary>
    /// 载入音频之后
    /// </summary>
    /// <param name="clip">载入的音频</param>
    void OnPostprocessAudio(AudioClip clip)
    {

    }

    void OnAssignMaterialModel(Material material, Renderer renderer)
    {

    }

    void OnPostprocessGameObjectWithUserProperties(GameObject go, string[] propNames, object[] values)
    {

    }
}
