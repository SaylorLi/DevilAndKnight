using System;
using System.Text;
using UnityEngine;

public class Const
{
    #region 系统底层
    public static readonly Color ColorIcon = new Color(0.22f, 0.24f, 0.314f, 1f);
    public static readonly Color ColorYellow = new Color(255 / 255f, 255 / 255f, 0f);
    public static readonly Color ColorRed = new Color(255 / 255f, 0f, 0f);
    public static readonly Color ColorGray = new Color(96 / 255f, 96 / 255f, 96 / 255f);
    public static Color GetColor(int r, int g, int b, int a = 255)
    {
        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    public const string LAYER_NGUI = "Ngui";
    public const string LAYER_PLAYER = "Player";
    #endregion

    #region 自定义
    //主角最大等级
    public const int MAX_PLAYER_LEVEL = 2000;
    //武器最大等级
    public const int MAX_WEAPON_LEVEL = 100;
    //伙伴最大等级
    public const int MAX_PARTNER_LEVEL = 2000;

    //主角技能个数
    public const int PLAYER_SKILL_COUNT = 6;
    // 主角转生等级
    public const int PLAYER_RESTART_LEVEL = 20;
    //伙伴技能个数
    public const int PARTNER_SKILL_COUNT = 4;
    // 伙伴转生等级
    public const int PARTNER_RESTART_LEVEL = 20;
    //最大伙伴技能等级
    public const int MAX_PARTNER_SKILL_LEVEL = 10;
    //最大上阵伙伴数目
    public const int MAX_GO_BATTLE_PARTNER_COUNT = 5;

    // 主角后退最大距离
    public const float MAX_BACKWARD_DISTANCE = 15.0f;
    // 主角向左最大角度
    public const float MAX_LEFT_EULER_ANGLE = 45.0F;
    // 主角向右最大角度
    public const float MAX_RIGHT_EULER_ANGLE = 45.0F;
    public const uint CANGQIONG_POJI = 18027;
    public const int SHAGUAI_HUIXUE = 8255;

    // 按钮颜色SpriteName
    public const string PARTNER_BUTTON_GREY = "anniu_b_grey";
    public const string PARTNER_BUTTON_GREEN = "anniu_b_green";
    public const string PARTNER_BUTTON_YELLOW = "anniu_b_yellow";
    public const string PARTNER_BUTTON_BLUE = "anniu_b_blue";
    public const string PARTNER_BUTTON_RED = "anniu_b_red";
    public const string SKILL_BUTTON_GREY = "anniu_s_grey";
    public const string SKILL_BUTTON_GREEN = "anniu_s_green";
    public const string SKILL_BUTTON_YELLOW = "anniu_s_yellow";
    //伙伴星级SpriteName
    public const string STAR_Y = "star";
    public const string STAR_N = "starao";

    public const string FOCOMBOEFFECT = "eff_char_ready";
    #endregion
}