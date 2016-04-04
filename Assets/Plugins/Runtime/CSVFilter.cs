using System;
using System.Collections.Generic;

public class CSVFilter
{
    //系统
    public const string version = "version";
    public const string error_code = "error_code";

    ////已使用
    //public const string tutorial = "tutorial";
    //public const string action = "action";
    //public const string monster = "monster";
    //public const string missile = "missile";
    //public const string role = "role";
    //public const string skill = "skill";
    //public const string stage = "stage";
    //public const string buff = "buff";
    //public const string hunter_treasure = "huntertreasure";
    //public const string hunter_partner = "partner";
    //public const string hunter_partner_atk = "partner_new_atk";
    //public const string hunter_partner_cost = "partner_new_cost";
    //public const string hunter_weapon = "hunterweapon";
    //public const string hunter_player = "hunterplayer";
    //public const string hunter_sign = "huntersign";
    //public const string hunter_skill_cost = "hunterskillcost";
    //public const string hunter_gift = "huntergift";
    //public const string hunter_goods = "huntergoods";
    //public const string monster_group = "monster_group";
    //public const string gift = "gift";
    //public const string dayQuest = "dayQuest";
    //public const string achievement = "achievement";
    //public const string boxReward = "boxReward";
    //public const string exchange = "exchange";

    //public const string syslimit = "syslimit";
    //public const string vip = "vip";
    //public const string randomname = "randomname";

    private static List<string> mConfigList = null;

    public static List<string> GetConfigList()
    {
        if (mConfigList == null)
        {
            mConfigList = new List<string>();
            mConfigList.Add(version);
            mConfigList.Add(error_code);
            //mConfigList.Add(randomname);
            //mConfigList.Add(tutorial);
            //mConfigList.Add(action);
            //mConfigList.Add(monster);
            //mConfigList.Add(missile);
            //mConfigList.Add(role);
            //mConfigList.Add(skill);
            //mConfigList.Add(stage);
            //mConfigList.Add(buff);
            //mConfigList.Add(hunter_treasure);
            //mConfigList.Add(hunter_partner);
            //mConfigList.Add(hunter_partner_atk);
            //mConfigList.Add(hunter_partner_cost);
            //mConfigList.Add(hunter_weapon);
            //mConfigList.Add(hunter_player);
            //mConfigList.Add(hunter_sign);
            //mConfigList.Add(hunter_skill_cost);
            //mConfigList.Add(hunter_goods);
            //mConfigList.Add(hunter_gift);
            //mConfigList.Add(monster_group);
            //mConfigList.Add(gift);
            //mConfigList.Add(dayQuest);
            //mConfigList.Add(achievement);
            //mConfigList.Add(boxReward);
            //mConfigList.Add(exchange);
        }
        return mConfigList;
    }
}
