using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AntoCreateRankWindow : EditorWindow
{

    void OnEnable()
    {

    }

    void OnDestroy()
    {

    }

    void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Box("重复物体生成");
        GUILayout.Space(20);

        if (GUILayout.Button("制作", GUILayout.MinWidth(100), GUILayout.MaxWidth(120), GUILayout.MinHeight(45), GUILayout.MaxHeight(45)))
        {
            GameObject go = GameObject.Find("UIRoot(2D)/Camera/Anchor/menu/panel_legionsbuilds/panel_Ani/(GameObject)go_legionsLoyalty/(GameObject)go_ListRewardRule/panel_Ani/(GameObject)go_RewardRuleCon/(ScrollPanel)RewardRule/(GameObject)RuleGrid");
            if (go != null)
            {
                Vector3 vector3 = new Vector3(0, 0, 0);
                for (int i = 0; i < 30; i++)
                {
                    GameObject parent = AddEmptyGameObject(go, ("(GameObject)RuleInfo" + i), vector3);
                    Vector3 lblv3 = new Vector3(-138, 0, 0);
                    Vector3 lblscale = new Vector3(18, 18, 0);
                    Color32 color = new Color32(217, 198, 141, 255);
                    AddLabelObject(parent, ("(Label)lbl_LvRuleInfo" + (i + 1)), "font_num_default.prefab", lblv3, lblscale, 65, color);
                    lblv3 = new Vector3(0, 0, 0);
                    AddLabelObject(parent, ("(Label)lbl_PhysicalInfo" + (i + 1)), "font_num_default.prefab", lblv3, lblscale, 65, color);
                    lblv3 = new Vector3(132, 0, 0);
                    AddLabelObject(parent, ("(Label)lbl_PrestigeInfo" + (i + 1)), "font_num_default.prefab", lblv3, lblscale, 65, color);
                    lblv3 = new Vector3(0, -17, 0);
                    lblscale = new Vector3(320, 2, 0);
                    AddSpriteObject(parent, ("(Sprite)sp_ruleInfo" + (i + 1)), "activity/ui_activity.prefab", "ui_frame_title07", lblv3, lblscale, 55);
                }
            }
        }

    }

    /// <summary>
    /// 添加一个空的GameObject
    /// </summary>
    private GameObject AddEmptyGameObject(GameObject parent, string name, Vector3 vector3)
    {
        GameObject go = new GameObject();
        if (parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.name = name;
            t.localPosition = vector3;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    private UILabel AddLabelObject(GameObject parent, string name, string fontpath, Vector3 vector3, Vector3 scale, int depth, Color color)
    {
        GameObject go = new GameObject();
        UILabel lbl = go.AddComponent<UILabel>();
        if (parent != null)
        {
            Transform t = parent.transform;
            lbl.gameObject.transform.parent = parent.transform;
            lbl.gameObject.name = name;
            lbl.gameObject.transform.localPosition = vector3;
            lbl.gameObject.transform.localRotation = Quaternion.identity;
            lbl.gameObject.transform.localScale = scale;
            lbl.gameObject.layer = parent.layer;
            lbl.depth = depth;
            lbl.text = "12345";
            lbl.color = color;
            UIFont Font = AssetDatabase.LoadAssetAtPath("Assets/Resources/" + fontpath, typeof(UIFont)) as UIFont;
            if (Font != null)
            {
                lbl.font = Font;
            }
        }

        return lbl;
    }

    private UISprite AddSpriteObject(GameObject parent, string name, string atlaspath, string spritename, Vector3 vector3, Vector3 scale, int depth)
    {
        GameObject go = new GameObject();
        UISprite sp = go.AddComponent<UISprite>();
        if (parent != null)
        {
            Transform t = parent.transform;
            sp.gameObject.transform.parent = parent.transform;
            sp.gameObject.name = name;
            sp.gameObject.transform.localPosition = vector3;
            sp.gameObject.transform.localRotation = Quaternion.identity;
            sp.gameObject.transform.localScale = scale;
            sp.gameObject.layer = parent.layer;
            sp.depth = depth;
            sp.type = UISprite.Type.Sliced;
            sp.fillCenter = true;
            UIAtlas atlas = AssetDatabase.LoadAssetAtPath("Assets/my_ngui/atlasnew/" + atlaspath, typeof(UIAtlas)) as UIAtlas;
            if (atlas != null)
            {
                sp.atlas = atlas;
                sp.spriteName = spritename;
            }
        }
        return sp;
    }

}
