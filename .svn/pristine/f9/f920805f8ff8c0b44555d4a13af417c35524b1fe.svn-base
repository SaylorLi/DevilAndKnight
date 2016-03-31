using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class UITool
{
    public static Vector3 GetCfgVector3(string str)
    {
        string[] arr = str.Split('_');
        return new Vector3(float.Parse(arr[0]), float.Parse(arr[1]), float.Parse(arr[2]));
    }

    internal static int GetPower10(float f)
    {
        int result = 0;
        if (f >= 1)
        {
            while (f >= 10)
            {
                ++result;
                f /= 10;
            }
        }
        else if (f > 0)
        {
            while (f < 1)
            {
                --result;
                f *= 10;
            }
        }

        return result;
    }

    
    internal static bool IsEmpty(string path)
    {
        if (path == string.Empty)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //
    public static int GetStringLength(string str)
    {
        string temp = str;
        int j = 0;
        for (int i = 0; i < temp.Length; i++)
        {
            if (Regex.IsMatch(temp.Substring(i, 1), @"[\u4e00-\u9fa5]+"))
            {
                j += 2;
            }
            else
            {
                j += 1;
            }

        }
        return j;
    }

    public static string GetStringUTF8(string name)
    {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(name);
        string str = string.Empty;
        foreach (byte b in buffer)
        {
            str += string.Format("%{0:X}", b);
        }
        return str;
    }
    //
    public static DateTime TransStringToDateTimeSecond(string str)
    {
        if (str.Trim().Equals("0"))
        {
            //Log.Log_hjx("str :" + str + ":");
            return DateTime.MinValue;
        }
        //Log.Log_hjx("str " + str);
        int year = int.Parse(str.Substring(0, 4));
        int month = int.Parse(str.Substring(4, 2));
        int day = int.Parse(str.Substring(6, 2));
        int hours = int.Parse(str.Substring(8, 2));
        int minute = int.Parse(str.Substring(10, 2));
        int second = int.Parse(str.Substring(12, 2));
        //Log.Log_hjx("year " + year + "month " + month + "day " + day + " hours " + hours + " minute " + minute + "second " + second);
        DateTime d = new DateTime(year, month, day, hours, minute, second, DateTimeKind.Utc);
        return d;
    }

    public static DateTime TransStringToDateTimeMinute(string str)
    {
        if (str.Trim().Equals("0"))
        {
            //Log.Log_hjx("str :" + str + ":");
            return DateTime.MinValue;
        }
        int year = int.Parse(str.Substring(0, 4));
        int month = int.Parse(str.Substring(4, 2));
        int day = int.Parse(str.Substring(6, 2));
        int hours = int.Parse(str.Substring(8, 2));
        int minute = int.Parse(str.Substring(10, 2));
        //Log.Log_hjx("year " + year + "month " + month + "day " + day + " hours " + hours + " minute " + minute);
        DateTime d = new DateTime(year, month, day, hours, minute, 0, DateTimeKind.Utc);
        return d;
    }

    public static Color GetColor(int R, int G, int B)
    {
        return new Color(R / 255f, G / 255f, B / 255f);
    }

    #region Ngui
    public static string GetColorString(string s, Color color)
    {
        string r = TransDToX((int)(color.r * 255));
        string g = TransDToX((int)(color.g * 255));
        string b = TransDToX((int)(color.b * 255));

        return "[" + r + g + b + "]" + s + "[-]";
    }
    static string TransDToX(int value)
    {
        string s = value.ToString("X");

        if (value < 16)
        {
            s = "0" + s;
        }
        return s;
    }

    public static void SetAllDepthOffset(int offset, GameObject go)
    {
        UISprite[] arr = go.GetComponentsInChildren<UISprite>(true);
        foreach (UISprite sp in arr)
        {
            sp.depth += offset;
        }
        UILabel[] arrLabel = go.GetComponentsInChildren<UILabel>(true);
        foreach (UILabel label in arrLabel)
        {
            label.depth += offset;
        }
    }
    public static UILabel GetUILabel(GameObject go)
    {
        UILabel w = null;
        UILabel[] arr = go.GetComponentsInChildren<UILabel>(true);
        if (arr.Length > 0)
        {
            w = arr[0];
        }
        return w;
    }
    public static UIGrid GetUIGrid(GameObject go)
    {
        UIGrid w = null;
        UIGrid[] arr = go.GetComponentsInChildren<UIGrid>(true);
        if (arr.Length > 0)
        {
            w = arr[0];
        }
        return w;
    }
    public static UIPanel GetUIPanel(GameObject go)
    {
        UIPanel w = null;
        UIPanel[] arr = go.GetComponentsInChildren<UIPanel>(true);
        if (arr.Length > 0)
        {
            w = arr[0];
        }
        return w;
    }
    public static UIScrollBar GetUIScrollBar(GameObject go)
    {
        UIScrollBar w = null;
        UIScrollBar[] arr = go.GetComponentsInChildren<UIScrollBar>(true);
        if (arr.Length > 0)
        {
            w = arr[0];
        }
        return w;
    }
    //public static UIDraggablePanel GetUIDraggablePanel(GameObject go)
    //{
    //    UIDraggablePanel w = null;
    //    UIDraggablePanel[] arr = go.GetComponentsInChildren<UIDraggablePanel>(true);
    //    if (arr.Length > 0)
    //    {
    //        w = arr[0];
    //    }
    //    return w;
    //}
    public static bool IsMouseOverUI
    {
        get
        {
            Vector3 mousePostion = Input.mousePosition;
            //GameObject hoverobject = UICamera.Raycast(mousePostion, out UICamera.lastHit) ? UICamera.lastHit.collider.gameObject : null;
            GameObject hoverobject = UICamera.Raycast(mousePostion) ? UICamera.lastHit.collider.gameObject : null;
            if (hoverobject != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    #endregion
    //ui
    public static void DestroyAllChild(Transform tf)
    {
        int num = tf.childCount;
        Transform child = null;
        for (int i = num - 1; i >= 0; i--)
        {
            child = tf.GetChild(i);
            GameObject.Destroy(child.gameObject);
        }
    }
    public static void SetAllLayer(Transform transform, int layer)
    {
        int elementCount = transform.childCount;
        //UITool.Log(Go.name+" elementCount=" + elementCount);
        for (int i = 0; i < elementCount; ++i)
        {
            Transform t = transform.GetChild(i);
            GameObject childObj = t.gameObject;
            childObj.layer = layer;
            //
            SetAllLayer(t, layer);
        }
    }
    static bool AddChildTf(Transform tC, Transform tP)
    {
        if (tC != tP)
        {
            tC.parent = tP;
            tC.localScale = Vector3.one;
            tC.localPosition = Vector3.zero;
            tC.localRotation = Quaternion.identity;
            return true;
        }
        return false;
    }
    static public bool AddChild(Transform tC, Transform tP)
    {
        if (tC != null && tP != null)
        {
            return AddChildTf(tC, tP);
        }
        else
        {
            Debug.LogError("AddChild tf null.");
            return false;
        }
    }
    static public bool AddChild(GameObject child, GameObject parent)
    {
        if (child != null && parent != null)
        {
            Transform tC = child.transform;
            Transform tP = parent.transform;
            return AddChildTf(tC, tP);
        }
        else
        {
            Debug.LogError("AddChild go null.");
            return false;
        }
    }
    public static Transform FindInChildByName(Transform parent, string childName)
    {
        Transform child = parent.FindChild(childName);
        if (child == null)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                child = FindInChildByName(parent.GetChild(i), childName);
                if (child != null)
                {
                    break;
                }
            }
        }
        return child;
    }

    //	Unity对您的脚本支持的平台定义有:

    //UNITY_EDITOR
    //用于执行UnityEditor模式下的脚本的定义.
    //UNITY_STANDALONE_OSX
    //用于编译和执行专门针对Mac OS的代码的平台定义.
    //UNITY_DASHBOARD_WIDGET
    //为创建Dashboard widget工具的代码的平台定义.
    //UNITY_STANDALONE_WIN
    //专门为Windows的独立应用程序编译/执行代码的平台定义.
    //UNITY_STANDALONE_LINUX
    //专门为Linux的独立应用程序编译/执行代码的平台定义.
    //UNITY_STANDALONE
    //为任何平台的独立应用程序编译/执行代码的平台定义（包括Windows、Mac、Linux）.
    //UNITY_WEBPLAYER
    //网页播放器的内容的平台定义 (这包括Windows和Mac的Web播放器的可执行文件).
    //UNITY_WII
    //专门为Wii游戏机编译和执行代码的平台定义.
    //UNITY_IPHONE
    //为iPhone平台的编译和执行代码的平台定义.
    //UNITY_ANDROID
    //为安卓平台的编译和执行代码的平台定义.
    //UNITY_PS3
    //为PS3编译和执行代码的平台定义.
    //UNITY_XBOX360
    //为XBOX360编译和执行代码的平台定义.
    //UNITY_NACL
    //为谷歌客户端编译和执行代码的平台定义. (这是对 UNITY_WEBPLAYER的补充).
    //UNITY_FLASH
    //为AdobeFlash编译和执行代码的平台定义.
    public static string Encrypt(string p)
    {
        string result = string.Empty;
        for (int i = p.Length - 1; i >= 0; i--)
        {
            result = EncryptOne(i, p[i].ToString()) + result;
        }
        return result;
    }
    public static string Decrypt(string p)
    {
        string result = string.Empty;
        for (int i = p.Length - 1; i >= 0; i--)
        {
            result = DecryptOne(i, p[i].ToString()) + result;
        }
        return result;
    }

    private static string EncryptOne(int i, string str)
    {
        int result = 0;
        int num = int.Parse(str);
        if (i == 0)
        {
            result = num + 1;
        }
        else if (i == 1)
        {
            result = num + 3;
        }
        else if (i == 2)
        {
            result = num + 5;
        }
        else if (i == 3)
        {
            result = num + 7;
        }
        else if (i == 4)
        {
            result = num + 9;
        }
        else if (i == 5)
        {
            result = num - 2;
        }
        else if (i == 6)
        {
            result = num - 4;
        }
        else if (i == 7)
        {
            result = num - 6;
        }
        else if (i == 8)
        {
            result = num - 8;
        }
        else if (i == 9)
        {
            result = num - 0;
        }
        //
        if (result < 0)
        {
            result += 10;
        }
        if (result >= 10)
        {
            result -= 10;
        }
        //Log.Log_hjx(num + "num   " + result + " result " + "EncryptOne");
        return result.ToString();
    }
    private static string DecryptOne(int i, string str)
    {
        int result = 0;
        int num = int.Parse(str);
        if (i == 0)
        {
            result = num - 1;
        }
        else if (i == 1)
        {
            result = num - 3;
        }
        else if (i == 2)
        {
            result = num - 5;
        }
        else if (i == 3)
        {
            result = num - 7;
        }
        else if (i == 4)
        {
            result = num - 9;
        }
        else if (i == 5)
        {
            result = num + 2;
        }
        else if (i == 6)
        {
            result = num + 4;
        }
        else if (i == 7)
        {
            result = num + 6;
        }
        else if (i == 8)
        {
            result = num + 8;
        }
        else if (i == 9)
        {
            result = num + 0;
        }
        //
        if (result < 0)
        {
            result += 10;
        }
        if (result >= 10)
        {
            result -= 10;
        }
        //Log.Log_hjx(num + "num   " + result + " result " + "DecryptOne");
        return result.ToString();
    }

    public static string GetTimeHHMMSS(float seconds)
    {
        int h = (int)(seconds / 3600);
        int m = (int)((seconds - h * 3600) / 60);
        int s = (int)(seconds - h * 3600 - m * 60);
        return h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
    }
}
