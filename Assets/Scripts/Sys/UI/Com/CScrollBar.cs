using UnityEngine;
using System.Collections;

public class CScrollBar : CGameObject
{
    public delegate void OnScrollAdd();
    private OnScrollAdd onScrollAdd;
    public delegate void OnScrollPercent(float percent);
    private OnScrollPercent onScrollPercent;
    private UIScrollBar uiScrollBar;
    public CScrollBar()
    {
        base.m_componentType = eComponentType.ScrollBar;
    }
    public override void Init()
    {
        uiScrollBar = _Go.GetComponent<UIScrollBar>();
        //uiScrollBar.onChange = ScrollBarChange;
    }
    public void SetDragFinished(UIScrollBar.OnDragFinished onDragFinished)
    {
        uiScrollBar.onDragFinished = onDragFinished;
    }
    /// <summary>
    /// 滚动的时候调用.
    /// </summary>
    /// <param name="onScrollAdd"></param>
    public void SetOnScrollPercent(OnScrollPercent onScrollPercent)
    {
        this.onScrollPercent = onScrollPercent;
    }
    /// <summary>
    /// 滚动到头的时候调用,既value==1.
    /// </summary>
    /// <param name="onScrollAdd"></param>
    public void SetOnScrollAdd(OnScrollAdd onScrollAdd)
    {
        this.onScrollAdd = onScrollAdd;
    }
    public float ScrollValue
    {
        get
        {
            return uiScrollBar.scrollValue;
        }
        set
        {
            uiScrollBar.scrollValue = value;
        }
    }
    private void ScrollBarChange(UIScrollBar sb)
    {
        ScrollPercent(uiScrollBar.scrollValue);
    }
    bool canAdd = true;
    private void ScrollPercent(float percent)
    {
        if (onScrollPercent != null)
        {
            onScrollPercent(percent);
        }
        //UITool.Log("滚动值: " + percent);
        if (percent == 1)
        {
            if (canAdd)
            {
                canAdd = false;
                if (onScrollAdd != null)
                {
                    onScrollAdd();
                }
            }
        }
        else
        {
            canAdd = true;
        }
    }
}
