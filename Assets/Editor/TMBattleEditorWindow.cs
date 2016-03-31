using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class TMBattleEditorWindow : EditorWindow
{
    ////private static CharaEditorItem[] listPlayer = new CharaEditorItem[7];
    //private static eBattleMode battleMode;//PVE or PVP
    //private static int stageID = 10001;//PVE用的关卡
    ////private static CharaEditorItem[] listRivaler = new CharaEditorItem[6];//PVP用的对手

    //void OnEnable()
    //{
    //    //BattleLauncher.PATH_CONFIG = Application.dataPath + "/../BattleEditorData.txt";

    //    ReadConfigData();

    //    if (mRoleDic == null)
    //    {
    //        ResetLoad();
    //    }
    //}

    //void OnDestroy()
    //{

    //}

    //void OnGUI()
    //{
    //    TMEditorTools.SpaceNormal();
    //    tmpIndex = GUILayout.Toolbar(toolBarIndex, toolBarTitiles);
    //    if (tmpIndex != toolBarIndex)
    //    {
    //        EditorPrefs.SetInt(KEY_BATTLE_EDITOR_INDEX, tmpIndex);
    //        toolBarIndex = tmpIndex;
    //    }
    //    switch (tmpIndex)
    //    {
    //        case 0:
    //            OnGUI_BattleData();
    //            break;
    //        case 1:
    //            OnGUI_SkillEditor();
    //            break;
    //    }
    //}

    //private void OnGUI_BattleData()
    //{
    //    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);
    //    EditorGUILayout.Separator();
    //    // Player(MainForm and SubForm)
    //    for (int i = 0; i < 4; i++)
    //    {
    //        Unit_BattleData(string.Format("Main Form({0})", i + 1), ref listPlayer[i]);
    //    }
    //    for (int i = 4; i < 6; i++)
    //    {
    //        Unit_BattleData(string.Format("Sub Form({0})", i - 3), ref listPlayer[i]);
    //    }
    //    // PVE or PVP
    //    EditorGUILayout.Separator();
    //    battleMode = (eBattleMode)EditorGUILayout.EnumPopup(battleMode);
    //    if (battleMode == eBattleMode.Quest || battleMode == eBattleMode.KylinBoss)
    //    {
    //        // Help
    //        Unit_BattleData("Helper:", ref listPlayer[6], true);
    //        // 关卡
    //        EditorGUILayout.Separator();
    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label("关卡ID:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //        stageID = EditorGUILayout.IntField(stageID);
    //        GUILayout.EndHorizontal();
    //    }
    //    else if (battleMode == eBattleMode.PVP)
    //    {
    //        GUILayout.BeginHorizontal();
    //        GUILayout.Space(10);
    //        GUILayout.Label("Hp Scale:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //        BattleData.HpScale = EditorGUILayout.FloatField(BattleData.HpScale);
    //        GUILayout.EndHorizontal();

    //        // Rivaler(MainForm and SubForm)
    //        for (int i = 0; i < 4; i++)
    //        {
    //            Unit_BattleData(string.Format("Main Form({0})", i + 1), ref listRivaler[i]);
    //        }
    //        for (int i = 4; i < 6; i++)
    //        {
    //            Unit_BattleData(string.Format("Sub Form({0})", i - 3), ref listRivaler[i]);
    //        }
    //    }

    //    if (battleMode == eBattleMode.Show)
    //    {
    //        listPlayer[0] = GetPlayerItemByName("ch_nanzhujue");
    //        listPlayer[1] = GetPlayerItemByName("ch_nvzhujue");
    //        listPlayer[2] = GetPlayerItemByName("ch_zhugeliang");
    //        listPlayer[3] = GetPlayerItemByName("ch_guanyu");
    //        listPlayer[4] = GetPlayerItemByName("ch_lvbu");
    //        listPlayer[5] = GetPlayerItemByName("ch_caocao");
    //    }
    //    // Apply and Reset
    //    EditorGUILayout.Separator();
    //    GUILayout.BeginHorizontal();
    //    GUILayout.Space(50);
    //    if (GUILayout.Button("Apply", GUILayout.MinWidth(100), GUILayout.MaxWidth(120), GUILayout.MinHeight(45), GUILayout.MaxHeight(45)))
    //    {
    //        WritConfigData();
    //    }
    //    GUILayout.Space(10);
    //    if (GUILayout.Button("Reset", GUILayout.MinWidth(100), GUILayout.MaxWidth(120), GUILayout.MinHeight(45), GUILayout.MaxHeight(45)))
    //    {
    //        ResetData();
    //    }
    //    GUILayout.Space(10);
    //    if (GUILayout.Button("ReLoad", GUILayout.MinWidth(100), GUILayout.MaxWidth(120), GUILayout.MinHeight(45), GUILayout.MaxHeight(45)))
    //    {
    //        ResetLoad();
    //    }
    //    GUILayout.EndHorizontal();
    //    GUILayout.Space(20);
    //    EditorGUILayout.EndScrollView();
    //}

    //private CharaEditorItem GetPlayerItemByName(string name)
    //{
    //    RoleCardConfigVo vo = mRoleDic[name];
    //    CharaEditorItem item = new CharaEditorItem();
    //    item.isActive = true;
    //    item.chName = vo.res_id;
    //    item.attack = vo.initialatk;
    //    item.hp = vo.initialhp;
    //    item.ability1 = vo.ability1_id;
    //    item.ability2 = vo.ability2_id;
    //    item.breakLv = 0;
    //    item.lv = 1;
    //    item.skill1 = vo.skill_id1;
    //    item.skill2 = vo.skill_id2;
    //    item.isHelper = false;
    //    item.isFriend = false;
    //    return item;
    //}

    //private CharaEditorItem GetRivialItemByName(string name)
    //{
    //    return null;
    //}

    //private void OnGUI_SkillEditor()
    //{

    //}

    //// 绘制一个角色的信息
    //private void Unit_BattleData(string title, ref CharaEditorItem userData, bool isHelper = false)
    //{
    //    if (userData == null) return;

    //    EditorGUILayout.Separator();
    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label(title, GUILayout.MinWidth(70), GUILayout.MaxWidth(90));
    //    userData.isActive = GUILayout.Toggle(userData.isActive, "激活", GUILayout.MinWidth(50), GUILayout.MaxWidth(80));
    //    if (userData.isActive)
    //    {
    //        GUILayout.Space(10);
    //        if (GUILayout.Toggle(false, "表格数值"))
    //        {
    //            RoleCardConfigVo vo = null;
    //            if (!mRoleDic.TryGetValue(userData.chName, out vo))
    //            {
    //                // 如果填写的名称不存在就随机一个(3星以上)
    //                List<RoleCardConfigVo> list = new List<RoleCardConfigVo>(mRoleDic.Count);
    //                IEnumerator<RoleCardConfigVo> erator = mRoleDic.Values.GetEnumerator();
    //                while (erator.MoveNext())
    //                {
    //                    if (erator.Current.card_quality >= 3)
    //                    {
    //                        list.Add(erator.Current);
    //                    }
    //                }
    //                int indexRand = UnityEngine.Random.Range(0, list.Count);
    //                vo = list[indexRand];
    //            }
    //            userData.chName = vo.res_id;
    //            userData.lv = 80;
    //            userData.breakLv = 9;
    //            userData.hp = vo.initialhp;
    //            userData.attack = vo.initialatk;

    //            BreachConfigVo breachVo = null;
    //            if (mBreachDic.TryGetValue(userData.breakLv.ToString(), out breachVo))
    //            {
    //                userData.attack = vo.initialatk + Mathf.FloorToInt((((vo.normalgrowth_atk * userData.lv) / 80) * breachVo.param));
    //                userData.hp = vo.initialhp + Mathf.FloorToInt((((vo.normalgrowth_hp * userData.lv) / 80) * breachVo.param));
    //            }
    //            userData.skill1 = vo.skill_id1;
    //            userData.skill2 = vo.skill_id2;
    //            userData.ability1 = vo.ability1_id;
    //            userData.ability2 = vo.ability2_id;
    //            userData.ability3 = vo.ability3_id;
    //        }
    //    }
    //    GUILayout.EndHorizontal();
    //    if (userData.isActive)
    //    {
    //        GUILayout.BeginHorizontal();
    //        GUILayout.Space(10);
    //        GUILayout.Label("Name:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //        userData.chName = GUILayout.TextField(userData.chName);
    //        GUILayout.EndHorizontal();

    //        GUILayout.BeginHorizontal();
    //        GUILayout.Space(10);
    //        GUILayout.Label("LV:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //        int lv = EditorGUILayout.IntField(userData.lv);
    //        GUILayout.EndHorizontal();

    //        GUILayout.BeginHorizontal();
    //        GUILayout.Space(10);
    //        GUILayout.Label("Break LV:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //        int bLv = EditorGUILayout.IntField(userData.breakLv);
    //        GUILayout.EndHorizontal();

    //        // 由lv和blv进行血量和攻击的计算
    //        if ((lv != userData.lv || bLv != userData.breakLv) && mRoleDic != null && mBreachDic != null)
    //        {
    //            RoleCardConfigVo roleVo = null;
    //            BreachConfigVo breachVo = null;
    //            if (mRoleDic.TryGetValue(userData.chName, out roleVo) &&
    //                mBreachDic.TryGetValue(bLv.ToString(), out breachVo))
    //            {
    //                userData.attack = roleVo.initialatk + Mathf.FloorToInt((((roleVo.normalgrowth_atk * lv) / 80) * breachVo.param));
    //                userData.hp = roleVo.initialhp + Mathf.FloorToInt((((roleVo.normalgrowth_hp * lv) / 80) * breachVo.param));
    //            }
    //        }
    //        userData.lv = lv;
    //        userData.breakLv = bLv;

    //        // 攻击和血量默认做隐藏
    //        GUILayout.BeginHorizontal();
    //        GUILayout.Space(10);
    //        GUILayout.Label("Attack:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //        userData.attack = EditorGUILayout.IntField(userData.attack);
    //        GUILayout.EndHorizontal();

    //        GUILayout.BeginHorizontal();
    //        GUILayout.Space(10);
    //        GUILayout.Label("Hp:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //        userData.hp = EditorGUILayout.IntField(userData.hp);
    //        GUILayout.EndHorizontal();

    //        // Helper的技能必须要在它成为朋友后才能激活
    //        if (isHelper)
    //        {
    //            userData.isHelper = true;
    //            GUILayout.BeginHorizontal();
    //            GUILayout.Space(10);
    //            userData.isFriend = GUILayout.Toggle(userData.isFriend, "Is Friend");
    //            GUILayout.EndHorizontal();
    //        }

    //        if (!userData.isHelper || userData.isFriend)
    //        {
    //            GUILayout.BeginHorizontal();
    //            GUILayout.Space(10);
    //            GUILayout.Label("Ability1:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //            userData.ability1 = EditorGUILayout.IntField(userData.ability1);
    //            GUILayout.EndHorizontal();

    //            GUILayout.BeginHorizontal();
    //            GUILayout.Space(10);
    //            GUILayout.Label("Ability2:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //            userData.ability2 = EditorGUILayout.IntField(userData.ability2);
    //            GUILayout.EndHorizontal();

    //            GUILayout.BeginHorizontal();
    //            GUILayout.Space(10);
    //            GUILayout.Label("Ability3:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //            userData.ability3 = EditorGUILayout.IntField(userData.ability3);
    //            GUILayout.EndHorizontal();

    //            GUILayout.BeginHorizontal();
    //            GUILayout.Space(10);
    //            GUILayout.Label("Skill1:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //            userData.skill1 = EditorGUILayout.IntField(userData.skill1);
    //            GUILayout.EndHorizontal();

    //            GUILayout.BeginHorizontal();
    //            GUILayout.Space(10);
    //            GUILayout.Label("Skill2:", GUILayout.MinWidth(40), GUILayout.MaxWidth(60));
    //            userData.skill2 = EditorGUILayout.IntField(userData.skill2);
    //            GUILayout.EndHorizontal();
    //        }
    //        GUILayout.Space(10);
    //    }
    //}

    //private int toolBarIndex = 0;
    //private int tmpIndex = 0;
    //private Vector2 scrollPos = Vector2.zero;
    //private string[] toolBarTitiles = new string[] { "数据模拟", "技能编辑" };
    //private const string KEY_BATTLE_EDITOR_INDEX = "key_battle_editor_index";


    //private void ReadConfigData()
    //{
    //    if (!File.Exists(BattleLauncher.PATH_CONFIG))
    //    {
    //        for (int i = 0; i < listPlayer.Length; i++)
    //        {
    //            listPlayer[i] = new CharaEditorItem();
    //        }
    //        for (int i = 0; i < listRivaler.Length; i++)
    //        {
    //            listRivaler[i] = new CharaEditorItem();
    //        }
    //        battleMode = eBattleMode.Quest;
    //        stageID = 10001;
    //        // 保存一次数据
    //        WritConfigData();
    //    }
    //    else
    //    {
    //        // 读取本地配置
    //        string txtContent = null;
    //        using (FileStream fs = new FileStream(BattleLauncher.PATH_CONFIG, FileMode.Open, FileAccess.Read, FileShare.Read))
    //        {
    //            using (StreamReader reader = new StreamReader(fs, System.Text.Encoding.UTF8))
    //            {
    //                txtContent = reader.ReadToEnd();
    //                reader.Close();
    //            }
    //            fs.Close();
    //        }
    //        // 解析
    //        string[] txtContentRow = txtContent.Replace("\r\n", "\n").Split('\n');
    //        // 1-6行是Player Main 和Player Sub 
    //        for (int i = 0; i < 6; i++)
    //        {
    //            listPlayer[i] = new CharaEditorItem(txtContentRow[i].Trim());
    //        }
    //        // 7行是BattleModel标识
    //        battleMode = (eBattleMode)System.Enum.Parse(typeof(eBattleMode), txtContentRow[6].Trim(), true);
    //        // 8行是Helper信息
    //        listPlayer[6] = new CharaEditorItem(txtContentRow[7].Trim());
    //        // 9行是关卡信息
    //        stageID = int.Parse(txtContentRow[8].Trim());
    //        // 10-15行是Rivaler信息
    //        for (int i = 9; i < 15; i++)
    //        {
    //            listRivaler[i - 9] = new CharaEditorItem(txtContentRow[i].Trim());
    //        }
    //        // 最后一行是血量缩放比例
    //        if (txtContentRow.Length > 15)
    //            BattleData.HpScale = float.Parse(txtContentRow[15].Trim());
    //    }
    //}

    //private void WritConfigData()
    //{
    //    System.Text.StringBuilder builder = new System.Text.StringBuilder();
    //    for (int i = 0; i < 6; i++)
    //    {
    //        builder.AppendLine(listPlayer[i].ToString());
    //    }
    //    builder.AppendLine(battleMode.ToString());
    //    builder.AppendLine(listPlayer[6].ToString());
    //    builder.AppendLine(stageID.ToString());
    //    for (int i = 0; i < 6; i++)
    //    {
    //        builder.AppendLine(listRivaler[i].ToString());
    //    }
    //    if (battleMode != eBattleMode.PVP) BattleData.HpScale = 1;
    //    builder.AppendLine(BattleData.HpScale.ToString());
    //    using (FileStream fs = new FileStream(BattleLauncher.PATH_CONFIG, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
    //    {
    //        byte[] buff = System.Text.Encoding.UTF8.GetBytes(builder.ToString());
    //        fs.Write(buff, 0, buff.Length);
    //        fs.Close();
    //    }
    //}

    //private void ResetData()
    //{
    //    foreach (CharaEditorItem item in listPlayer)
    //    {
    //        item.Reset();
    //    }
    //    foreach (CharaEditorItem item in listRivaler)
    //    {
    //        item.Reset();
    //    }
    //    BattleData.HpScale = 1.0f;
    //    stageID = 0;
    //    battleMode = eBattleMode.Quest;
    //    // 写入
    //    WritConfigData();
    //}

    //// 重新载入RoleCard表格
    //private void ResetLoad()
    //{
    //    if (mRoleDic != null)
    //    {
    //        mRoleDic.Clear();
    //        mRoleDic = null;
    //    }
    //    if (mBreachDic != null)
    //    {
    //        mBreachDic.Clear();
    //        mBreachDic = null;
    //    }
    //    mRoleDic = LoadCsv<RoleCardConfigVo>("rolecard", vo =>
    //    {
    //        if (string.IsNullOrEmpty(vo.res_id))
    //        {
    //            Debug.LogError(string.Format("数据存在问题---{0},res_id为null", vo.id));
    //            return null;
    //        }

    //        return vo.res_id;
    //    });
    //    mBreachDic = LoadCsv<BreachConfigVo>("breach", vo =>
    //    {
    //        return vo.id;
    //    });
    //    System.GC.Collect();
    //}

    //private Dictionary<string, T> LoadCsv<T>(string shortName, System.Func<T, string> iterator) where T : BaseConfigVo
    //{
    //    string dir = Application.streamingAssetsPath + "/asset/bundle/config/";
    //    string[] roleCardCSVFileArr = Directory.GetFiles(dir, shortName + ".txt", SearchOption.TopDirectoryOnly);
    //    if (roleCardCSVFileArr == null || roleCardCSVFileArr.Length == 0)
    //    {
    //        Debug.LogError(string.Format("没有在{0}目录下找到{1}.txt文件", dir, shortName));
    //        return null;
    //    }
    //    string path = roleCardCSVFileArr[0];
    //    string textContent = File.ReadAllText(path, Encoding.UTF8);
    //    string[] contents = textContent.Replace("\r\n", "\n").Split('\n');
    //    // 根据记录数建立Dic的Capacity
    //    int len = contents.Length;
    //    if (len < 3)
    //    {
    //        Debug.LogError("表格没有数据记录");
    //        return null;
    //    }
    //    // 动态构建T
    //    Dictionary<string, T> dic = new Dictionary<string, T>(len - 2);
    //    for (int i = 2; i < len; i++)
    //    {
    //        if (string.IsNullOrEmpty(contents[i]))
    //            continue;
    //        string[] fields = contents[i].Split(',');
    //        if (fields.Length == 0 || string.IsNullOrEmpty(fields[0]))
    //            continue;
    //        T vo = System.Activator.CreateInstance<T>();
    //        vo.id = fields[0];
    //        List<string> list = new List<string>(fields.Length - 1);
    //        for (int j = 1; j < fields.Length; j++)
    //        {
    //            list.Add(fields[j]);
    //        }
    //        // 解析对象
    //        vo.Init(list);
    //        list.Clear();
    //        string key = iterator(vo);
    //        if (key != null)
    //        {
    //            if (dic.ContainsKey(key))
    //            {
    //                // Debug.LogError("重复存在相同记录---:" + vo.id);
    //            }
    //            else
    //            {
    //                dic.Add(key, vo);
    //            }
    //        }
    //    }
    //    return dic;
    //}

    //private Dictionary<string, RoleCardConfigVo> mRoleDic;
    //private Dictionary<string, BreachConfigVo> mBreachDic;
}