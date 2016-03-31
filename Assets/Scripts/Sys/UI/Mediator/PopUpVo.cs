using System;
using System.Collections.Generic;
using UnityEngine;

public class PopUpVo
{
    /// <summary>
    /// 显示界面标题
    /// </summary>
    public string title = string.Empty;

    /// <summary>
    /// 显示内容
    /// </summary>
    public string content = string.Empty;

    /// <summary>
    /// 宝箱ID
    /// </summary>
    //public string mTreasureID = string.Empty;

    /// <summary>
    ///  显示确定按内容 
    /// </summary>
    public string mOkTitle = string.Empty;

    /// <summary>
    ///   显示确认按钮内容
    /// </summary>
    public string mSureTitle = string.Empty;

    /// <summary>
    ///   显示取消按钮内容
    /// </summary>
    public string mCanelTitle = string.Empty;

    /// <summary>
    /// 确认委托 （出现两个按钮时）
    /// </summary>
    public VoidDelegate mSureDelegate;

    /// <summary>
    /// 确认委托 （一个按钮时）
    /// </summary>
    public VoidDelegate OkDelegate;

    /// <summary>
    /// 取消委托
    /// </summary>
    public VoidDelegate CanelDelegate;

    /// <summary>
    /// 设置行宽度
    /// </summary>
    public int lineWidth = 420;

    /// <summary>
    /// 设置字体大小
    /// </summary>
    public Vector2 fontScale = new Vector2(22, 22);

}

