using UnityEngine;
using UnityEditor;

using System.IO;
using System.Collections.Generic;

public class TMMenus
{
    [MenuItem("TM/Open Res Window")]
    public static void OpenTMExporterWindow()
    {
        EditorWindow.GetWindow<TMResEditorWindow>(false, "Res Window", true).Show(true);
    }

    [MenuItem("TM/Open Battle Window")]
    public static void OpenTMBattleWindow()
    {
        EditorWindow.GetWindow<TMBattleEditorWindow>(false, "Battle Window", true).Show(true);
    }

    [MenuItem("TM/Open Map Window")]
    public static void OpenMapWindow()
    {
        EditorWindow.GetWindow<MapEditorWindow>(false, "Map Window", true).Show(true);
    }

    [MenuItem("TM/Open SameObject Create")]
    public static void OpenSameObjectCreate()
    {
        EditorWindow.GetWindow<AntoCreateRankWindow>(false, "SameObject Create", true).Show(true);
    }

    [MenuItem("Assets/TM/GenMaterial")]
    public static void GenMaterialBySelection()
    {
        TMMaker.MakerCharaMaterial();
    }

    [MenuItem("Assets/TM/GenPrefab")]
    public static void GenPrefabBySelection()
    {
        TMMaker.MakerCharaPrefab();
    }

    [MenuItem("Assets/TM/GenAnimation")]
    public static void GenAnimationBySelection()
    {
        TMMaker.MakerCharaAnimation();
    }

    [MenuItem("Assets/TM/GenWeaponPrefab")]
    public static void GenWeapon()
    {
        TMMaker.MakerWeaponPrefab();
    }

    [MenuItem("Assets/TM/GenNGUIAtlas")]
    public static void GenNGUIAtlas()
    {
        TMMaker.MakerNGUIAtlas();
    }


    [MenuItem("Assets/TM/AutoCardAtlas")]
    public static void AutoCardAtlas()
    {
        TMMaker.MakerCardAtlas();
    }
}
