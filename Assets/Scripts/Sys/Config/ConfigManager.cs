using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EBindPoint
{
    none = -1,
    bind_zero = 0,//相对原点 （或靠前）
    bind_center = 1,//中心（或靠前）
    bind_lhand = 2,//左手
    bind_rhand = 3,//右手
    bind_head = 4,//头部
    bind_spine = 5,//腰上 
    bind_world = 6,//世界坐标
    bind_fire = 7,//子弹挂点
}
public class ConfigManager
{
    public static readonly ConfigManager Ins = new ConfigManager();
    Dictionary<string, Dictionary<string, BaseConfigVo>> dicNameDic = new Dictionary<string, Dictionary<string, BaseConfigVo>>();
    /// <summary>
    /// 挂接点名字表
    /// </summary>
    Dictionary<EBindPoint, string> dicBp_name = new Dictionary<EBindPoint, string>();
    public ConfigManager()
    {
        InitBp();
    }
    #region public
    public void Reset()
    {
        dicNameDic.Clear();
    }
    public void Add(string name, string content)
    {
        //if (name == "tutorial")
        //{
            //Log.Log_hjx(name+" content " + content);
        //}
        Dictionary<string, BaseConfigVo> dicIdConfigVo = new Dictionary<string, BaseConfigVo>();
        dicNameDic[name] = dicIdConfigVo;
        //
        string[] contents = content.Split("\r\n".ToCharArray());
        int numIntro = 2;
        foreach (string strLine in contents)
        {
            if (strLine.Length == 0) continue; //去除空行

            if (numIntro-- > 0) //去除两行
            {
                continue;
            }
            string[] arrField = strLine.Split(',');
            if (arrField.Length < 2)
            {
                //Log.Log_sys("strLine " + strLine);
            }
            try
            {
                if (arrField[1] == string.Empty)
                {
                    //空行处理
                    Debug.LogWarning("config null line: " + name);
                    //break;
                }
            }
            catch (System.Exception ex)
            {
                Log.LogError_sys("arrField lenth out");
                return;
            }

            List<string> list = new List<string>();
            string id = string.Empty;
            bool isId = true;
            foreach (string sField in arrField)
            {
                if (isId)
                {
                    isId = false;
                    id = sField;
                }
                else
                {
                    list.Add(sField);
                }
            }
            //
            BaseConfigVo vo = GetVo(name);
            if (id != string.Empty)
            {
                vo.id = id;
                vo.uid = uint.Parse(id);
                vo.Init(list);
                if (dicIdConfigVo.ContainsKey(id))
                {
                    Log.LogError_sys("表格有重复的id " + name + " id " + id);
                }
                else
                {
                    dicIdConfigVo.Add(id, vo);
                }
            }
        }
        //
        //if (name.Equals(CSVFilter.resversion))
        //{
        //    StartMediator startMediator = MediatorManager.Ins.GetBaseMediator(StartMediator.NAME) as StartMediator;
        //    if (startMediator != null) startMediator.ShowVersion();
        //}
    }
    public T Get<T>(string name, string id) where T : BaseConfigVo
    {
        return Get(name, id) as T;
    }
    public BaseConfigVo Get(string name, string id)
    {
        //Log.Log_hjx("name " + name + " id " + id);
        Dictionary<string, BaseConfigVo> dicIdConfigVo = dicNameDic[name];
        if (dicIdConfigVo.ContainsKey(id))
        {
            return dicIdConfigVo[id];
        }
        return null;
    }
    public List<T> GetList<T>(string name) where T : BaseConfigVo
    {
        List<T> list = new List<T>();
        foreach (BaseConfigVo vo in GetDic(name).Values)
            list.Add(vo as T);
        return list;
    }
    public Dictionary<string, BaseConfigVo> GetDic(string name)
    {
        Dictionary<string, BaseConfigVo> dicIdConfigVo = dicNameDic[name];
        return dicIdConfigVo;
    }
    #endregion

    private BaseConfigVo GetVo(string name)
    {
        BaseConfigVo vo;
        switch (name)
        {
            case CSVFilter.error_code:
                {
                    vo = new ErrorCodeConfigVo();
                    break;
                }
            case CSVFilter.version:
                {
                    vo = new VersionConfigVo();
                    break;
                }
            ////
            //case CSVFilter.missile:
            //    {
            //        vo = new MissileConfigVo();
            //        break;
            //    }
            //case CSVFilter.monster:
            //    {
            //        vo = new MonsterConfigVo();
            //        break;
            //    }
            //case CSVFilter.monster_group:
            //    {
            //        vo = new MonsterGroupConfigVo();
            //        break;
            //    }
            //case CSVFilter.role:
            //    {
            //        vo = new RoleConfigVo();
            //        break;
            //    }
            //case CSVFilter.skill:
            //    {
            //        vo = new SkillConfigVo();
            //        break;
            //    }
            //case CSVFilter.stage:
            //    {
            //        vo = new StageConfigVo();
            //        break;
            //    }
            //case CSVFilter.action:
            //    {
            //        vo = new ActionConfigVo();
            //        break;
            //    }
            //case CSVFilter.randomname:
            //    {
            //        vo = new RandomNameConfigVo();
            //        break;
            //    }
            //case CSVFilter.vip:
            //    {
            //        vo = new VipConfigVo();
            //        break;
            //    }
            //case CSVFilter.tutorial:
            //    {
            //        vo = new TutorialConfigVo();
            //        break;
            //    }
            //case CSVFilter.buff:
            //    {
            //        vo = new BuffConfigVo();
            //        break;
            //    }
            //case CSVFilter.hunter_treasure:
            //    {
            //        vo = new HunterTreasureConfigVo();
            //        break;
            //    }
            //case CSVFilter.hunter_partner:
            //    {
            //        vo = new HunterPartnerConfigVo();
            //        break;
            //    }
            //case CSVFilter.hunter_partner_atk:
            //    {
            //        vo = new HunterPartnerAtkConfigVo();
            //        break;
            //    }
            //case CSVFilter.hunter_partner_cost:
            //    {
            //        vo = new HunterPartnerCostConfigVo();
            //        break;
            //    }
            //case CSVFilter.hunter_weapon:
            //    {
            //        vo = new HunterWeaponConfigVo();
            //        break;
            //    }
            //case CSVFilter.hunter_player:
            //    {
            //        vo = new HunterPlayerConfigVo();
            //        break;
            //    }
            //case CSVFilter.hunter_sign:
            //    {
            //        vo = new HunterSignConfigVo();
            //        break;
            //    }
            //case CSVFilter.hunter_skill_cost:
            //    {
            //        vo = new HunterSkillCostConfigVo();
            //        break;
            //    }
            //case CSVFilter.hunter_gift:
            //    {
            //        vo = new HunterGiftConfigVo();
            //        break;
            //    }
            //case CSVFilter.hunter_goods:
            //    {
            //        vo = new HunterGoodsConfigVo();
            //        break;
            //    }
            //case CSVFilter.gift:
            //    {
            //        vo = new GiftConfigVo();
            //        break;
            //    }
            //case CSVFilter.dayQuest:
            //    {
            //        vo=new DayQuestConfigVo();
            //        break;
            //    }
            //case CSVFilter.achievement:
            //    {
            //        vo = new AchievementConfigVo();
            //        break;
            //    }
            //case CSVFilter.boxReward:
            //    {
            //        vo = new BoxRewardConfigVo();
            //        break;
            //    }
            //case CSVFilter.exchange:
            //    {
            //        vo = new ExchangeConfigVo();
            //        break;
            //    }
            default:
                {
                    Log.LogError_sys("ConfigManager: not new ConfigVo name: " + name);
                    return null;
                }
        }
        return vo;
    }
    void InitBp()
    {
        dicBp_name.Add(EBindPoint.bind_center, "bind_center");
        dicBp_name.Add(EBindPoint.bind_head, "bind_head");
        dicBp_name.Add(EBindPoint.bind_lhand, "bind_lhand");
        dicBp_name.Add(EBindPoint.bind_rhand, "bind_rhand");
        dicBp_name.Add(EBindPoint.bind_spine, "bind_spine");
        dicBp_name.Add(EBindPoint.bind_world, "bind_world");
        dicBp_name.Add(EBindPoint.bind_zero, "bind_zero");
        dicBp_name.Add(EBindPoint.bind_fire, "bind_fire");
    }
    public Dictionary<EBindPoint, string> GetBindPointMap()
    {
        return dicBp_name;
    }
}
