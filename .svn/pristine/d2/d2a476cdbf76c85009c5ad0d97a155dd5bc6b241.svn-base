using UnityEngine;
using System.Collections;
//鉴于数据安全问题 代码中除了特殊的类(如editor) 不得调用
//Debug.LogError和Debug.Log
//普通log调用自己的Log.Log_player
//错误统一调用Log.LogError_sys
//真机调试时调用Log.Log_debug 并且勾选game的isDebug !!!正式版不得勾选!!!
public class Log
{
    const int sys = 1;//系统
    const int hjx = 2;
    const int fight = 4;//战斗
    const int debug = 8;//调试bug用 发包时必须删掉
    const int sl = 16;//世乐
    const int zl = 32;//周亮
    const int all = sys | hjx | sl | fight | debug;//开关 这里有的才会输出 自己修改 提交时候忽略这个类
    public static void LogError_sys(string str, bool isMust = false)
    {
#if UNITY_EDITOR
        Debug.LogError(str);
#else
		if (Game.Ins.isDebug == false && isMust == false)
		{
			return;
		}
		Debug.LogError(str);
#endif
    }
    public static void PrintError(string str)
    {
        //if (Game.Ins != null && Game.Ins.isDebug == false)
        //{
        //    return;
        //}
        //FrontgroundMediator frontgroundMediator = MediatorManager.Ins.GetBaseMediator(FrontgroundMediator.NAME) as FrontgroundMediator;
        //if (frontgroundMediator == null)
        //{
        //    return;
        //}
        //LogVo vo = new LogVo();
        //vo.isError = true;
        //vo.str = "Error!!!: " + str;
        //frontgroundMediator.AddLog(vo);
    }
    public static void Print(string str)
    {
        //if (Game.Ins != null && Game.Ins.isDebug == false)
        //{
        //    return;
        //}
        //FrontgroundMediator frontgroundMediator = MediatorManager.Ins.GetBaseMediator(FrontgroundMediator.NAME) as FrontgroundMediator;
        //if (frontgroundMediator == null)
        //{
        //    return;
        //}
        //LogVo vo = new LogVo();
        //vo.str = str;
        //frontgroundMediator.AddLog(vo);
    }
    /// <summary>
    /// 调试bug专用 用完尽快删除代码
    /// </summary>
    public static void Log_debug(string str)
    {
        if (Game.Ins != null && Game.Ins.isDebug == false)
        {
            return;
        }
        if ((all & debug) == debug)
        {
            Debug.Log(str);
        }
    }

    public static void Log_player(string str, int player)
    {
#if UNITY_EDITOR
        if (Game.Ins != null && Game.Ins.isDebug == false)
        {
            return;
        }
        if ((all & player) == player)
        {
            Debug.Log(str);
        }
#endif
    }

    public static void Log_sys(string str)
    {
        Log_player(str, sys);
    }

    public static void Log_fight(string str)
    {
        Log_player(str, fight);
    }
    public static void Log_hjx(string str)
    {
        Log_player(str, hjx);
        Print(str);
    }
    public static void Log_sl(string str)
    {
        Log_player(str, sl);
    }

    public static void Log_zl(string str)
    {
        Log_player(str, zl);
    }


}
