using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class TMResEditorWindow : EditorWindow
{
    private const string KEY_TOOLBAR_INDEX = "KEY_TMEDITORWINDOW_TOOLBARINDEX";

    void OnEnable()
    {
        toolBarIndex = EditorPrefs.GetInt(KEY_TOOLBAR_INDEX, 0);
    }

    void OnDestroy()
    {
        EditorPrefs.SetInt(KEY_TOOLBAR_INDEX, toolBarIndex);
    }

    void OnGUI()
    {
        TMEditorTools.SpaceNormal();
        tmpIndex = GUILayout.Toolbar(toolBarIndex, toolBarTitles);
        if (tmpIndex != toolBarIndex)
        {
            EditorPrefs.SetInt(KEY_TOOLBAR_INDEX, tmpIndex);
            toolBarIndex = tmpIndex;
        }
        TMEditorTools.SpaceNormal();
        switch (tmpIndex)
        {
            case 0:
                OnGUI_Maker();
                break;
            case 1:
                OnGUI_Exporter();
                break;
        }
    }

    void OnGUI_Maker()
    {
        GUILayout.Label("角色：");
        GUILayout.BeginHorizontal();
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("生成材质"))
        {
            // 生成材质球
            TMMaker.MakerCharaMaterial();
        }
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("生成预制"))
        {
            // 生成预制件
            TMMaker.MakerCharaPrefab();
        }
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("剥离动作"))
        {
            // 剥离动作
            TMMaker.MakerCharaAnimation();
        }
        GUILayout.EndHorizontal();

        TMEditorTools.SpaceNormal();
        GUILayout.Label("武器：");
        GUILayout.BeginHorizontal();
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("生成材质"))
        {
            // 生成武器预制件
            TMMaker.MakerWeaponMaterial();
        }
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("生成预制"))
        {
            // 生成武器预制件
            TMMaker.MakerWeaponPrefab();
        }
        GUILayout.EndHorizontal();

        TMEditorTools.SpaceNormal();
        GUILayout.Label("图集制作：");
        GUILayout.BeginHorizontal();
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("制作图集"))
        {
            // 制作NGUI图集
            TMMaker.MakerNGUIAtlas();
        }
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("制作卡牌图集"))
        {
            // 制作卡牌图集
            TMMaker.MakerCardAtlas();
        }
        GUILayout.EndHorizontal();
    }

    void OnGUI_Exporter()
    {
        GUILayout.BeginHorizontal();
        //TMEditorTools.SpaceNormal();
        //if (TMEditorTools.ButtonNormal("导出Player"))
        //{
        //    // 导出全部Player
        //    TMExporter.BuildAllPlayer();
        //}
        //TMEditorTools.SpaceNormal();
        //if (TMEditorTools.ButtonNormal("导出Enemy"))
        //{
        //    // 导出全部Enemy
        //    TMExporter.BuildAllEnemy();
        //}
        //TMEditorTools.SpaceNormal();
        //if (TMEditorTools.ButtonNormal("导出Map"))
        //{
        //    // 导出全部Map
        //    TMExporter.BuildAllMap();
        //}
        //GUILayout.EndHorizontal();

        //TMEditorTools.SpaceNormal();

        //GUILayout.BeginHorizontal();
        //TMEditorTools.SpaceNormal();
        //if (TMEditorTools.ButtonNormal("导出Motion"))
        //{
        //    // 导出全部Motion
        //    TMExporter.BuildAllMotion();
        //}
        //TMEditorTools.SpaceNormal();
        //if (TMEditorTools.ButtonNormal("导出Voice"))
        //{
        //    // 导出全部Voice
        //    TMExporter.BuildAllVoice();
        //}
        //TMEditorTools.SpaceNormal();
        //if (TMEditorTools.ButtonNormal("导出Weapon"))
        //{
        //    // 导出全部Weapon
        //    TMExporter.BuildAllWeapon();
        //}
        //GUILayout.EndHorizontal();

        //TMEditorTools.SpaceNormal();

        //GUILayout.BeginHorizontal();
        //TMEditorTools.SpaceNormal();
        //if (TMEditorTools.ButtonNormal("导出Atlas"))
        //{
        //    // 导出全部Atlas
        //    TMExporter.BuildAllAtlas();
        //}
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("导出Config"))
        {
            // 导出全部Config
            TMExporter.BuildAllConfig();
        }
        //TMEditorTools.SpaceNormal();
        //if (TMEditorTools.ButtonNormal("导出测试"))
        //{
        //    TMExporter.BuildAllCharacter();
        //}
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        //TMEditorTools.SpaceNormal();
        //if (TMEditorTools.ButtonNormal("生成Hash"))
        //{
        //    // 为指定目录生成files.txt
        //    TMExporter.BuildHashFile();
        //}
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("全部导出"))
        {
            // 导出全部
            //TMExporter.BuildAll();
        }
        GUILayout.EndHorizontal();

        TMEditorTools.SpaceNormal();

        GUILayout.BeginHorizontal();
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("导出选择项"))
        {
            //TMExporter.BuildSelection();
        }
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("导出差异项"))
        {
            //TMExporter.BuildDifferences();
        }
        TMEditorTools.SpaceNormal();
        if (TMEditorTools.ButtonNormal("导出TUT"))
        {
            //TMExporter.BuildTutRes(false);
        }
        GUILayout.EndHorizontal();
    }

    void OnGUI_Setting()
    {

    }

    private int toolBarIndex = 0;
    private int tmpIndex = 0;
    private string[] toolBarTitles = new string[] { "资源制作", "资源导出"};
}
