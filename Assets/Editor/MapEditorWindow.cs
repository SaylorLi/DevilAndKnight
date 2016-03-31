using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MapEditorWindow : EditorWindow
{
    //
    private static Map mapName = Map.HuLaoGuan;
    private static string path = Application.streamingAssetsPath + "/asset/bundle/config/";
    //
    private Vector2 scrollPos = Vector2.zero;
    private string[] toolBarTitiles = new string[] { "地图编辑" };
    //
    static GameObject parantObj;

    void OnEnable()
    {
        InitData();
    }

    void OnDestroy()
    {
    }

    void OnGUI()
    {
        OnGUI_BattleData();
    }

    private void OnGUI_BattleData()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);
        GUILayout.Space(10);
        GUILayout.Box("地图编辑");
        GUILayout.Space(20);
        mapName = (Map)EditorGUILayout.EnumPopup(mapName);
        GUILayout.Space(10);

        EditorGUILayout.Separator();
        GUILayout.BeginHorizontal();
        GUILayout.Space(50);

        if (GUILayout.Button("ImportExcel", GUILayout.MinWidth(100), GUILayout.MaxWidth(120), GUILayout.MinHeight(45), GUILayout.MaxHeight(45)))
        {

            FindParantObject();
            ResetPanel();
            ReadData();
            CreateObject();
        }
        if (GUILayout.Button("ImportCacheData", GUILayout.MinWidth(100), GUILayout.MaxWidth(120), GUILayout.MinHeight(45), GUILayout.MaxHeight(45)))
        {
            FindParantObject();
            ResetPanel();
            ReadData();
            ReadCacheData();
            CreateObject();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("ResetPanel", GUILayout.MinWidth(100), GUILayout.MaxWidth(120), GUILayout.MinHeight(45), GUILayout.MaxHeight(45)))
        {
            ResetPanel();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Save", GUILayout.MinWidth(100), GUILayout.MaxWidth(120), GUILayout.MinHeight(45), GUILayout.MaxHeight(45)))
        {
            SaveConfigData();
        }

        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.EndScrollView();
    }

    private void InitData()
    {
        if (parantObj == null)
        {
            FindParantObject();
        }
    }

    private void ResetPanel()
    {
        if (parantObj != null)
        {
            Transform tf = parantObj.transform;
            int num = tf.childCount;
            Transform child = null;
            for (int i = num - 1; i >= 0; i--)
            {
                child = tf.GetChild(i);
                if (child != null)
                {
                    DestroyImmediate(child.gameObject);
                }
            }
        }
    }

    private void SaveConfigData()
    {
        //重新赋值
        //写入数据
        WriteConfigData();
        WriteCacheData();
    }

    private void WriteConfigData()
    {
        string str_text = "城堡,城堡名称,组件名,	城堡任务组,酒馆ID,前置任务,背景图片,城堡位置,城堡类型\r\n";
        str_text += "castle,castle_name,component,quest_id,tavern_id,lastquest_id,back_ground,position,castle_type\r\n";
        //foreach (BaseConfigVo vo in dic_castle.Values)
        //{
        //    CastleConfigVo castleConfigVo = vo as CastleConfigVo;
        //    str_text += castleConfigVo.WriteConfigVo();
        //}
        //string url = Application.dataPath + "/StreamingAssets/asset/bundle/config/" + CSVFilter.castle + ".txt";
        //File.WriteAllBytes(url, System.Text.Encoding.UTF8.GetBytes(str_text.ToString()));
    }

    private void WriteCacheData()
    {
        string strContent = "城堡名," + "城堡组件名," + "城堡位置\r\n"; ;
        //for (int i = 0; i < listCastleConfigVo.Count; i++)
        //{
        //    strContent += listCastleConfigVo[i].id + "," + listCastleConfigVo[i].castle_name + "," + listCastleConfigVo[i].componentname + "," + (int)listCastleConfigVo[i].position.x + ";" + (int)listCastleConfigVo[i].position.y + "\r\n";
        //}
        string positionPath = Application.dataPath + "/../map/" + mapName.ToString() + ".txt";
        File.WriteAllBytes(positionPath, System.Text.Encoding.UTF8.GetBytes(strContent.ToString()));
        //UITool.CreatePath(positionPath);
        //FileStream fileStream = File.Create(positionPath + filename + ".txt");
        //byte[] bs = System.Text.Encoding.UTF8.GetBytes(strContent.ToString());
        //fileStream.Write(bs, 0, bs.Length);
        //fileStream.Close();

    }

    private void ReadCacheData()
    {
        string filename = mapName.ToString();
        string positionPath = Application.dataPath + "/../map/" + filename + ".txt";
    }

    Vector2 GetVec(string pos)
    {
        Vector2 vec = Vector2.zero;
        if (pos != "0")
        {
            string[] arr = pos.Split(';');
            vec = new Vector2(float.Parse(arr[0]), float.Parse(arr[1]));
        }
        return vec;
    }

    private void ReadData()
    {
        //curMapConfig = ConfigManager.Ins.Get(CSVFilter.map, ((int)mapName).ToString()) as MapConfigVo;
        //listCastleConfigVo.Clear();
        //for (int i = 0; i < curMapConfig.arrCastle_id.Length; i++)
        //{
        //    if (curMapConfig.arrCastle_id[i] != "0")
        //    {
        //        CastleConfigVo castlevo = ConfigManager.Ins.Get(CSVFilter.castle, curMapConfig.arrCastle_id[i]) as CastleConfigVo;
        //        if (castlevo != null)
        //        {
        //            listCastleConfigVo.Add(castlevo);
        //        }
        //    }
        //}
    }

    private void CreateObject()
    {
        GameObject gameObj = Resources.Load("component/component_castle") as GameObject;
    }

    private void FindParantObject()
    {
        if (parantObj == null)
        {
            foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
                    {
                        if (obj.name == "(GameObject)go_CastleList")
                        {
                            parantObj = obj;
                        }
                    }
                }
            }
        }
    }

    public enum Map
    {
        HuLaoGuan = 10001,
        ChenLiu = 10002,
        YiLing = 10003,
        XiangYang = 10004,
        JianYe = 10005,
        XiaoYaoJin = 10006,
        NanPi = 10007,
        YingLianGe = 10008,
    }
}
