using UnityEngine;
using System.Collections.Generic;

public class CScrollPanel : CGameObject
{
	public List<CPanel> list = new List<CPanel>();
	private string itemName;
	private UIGrid uiGrid;
	private UIScrollBar uiScrollBar;
	public GameObject goGrid;
	public CScrollBar scrollBar;
	UIPanel uip;
	private Vector4 i_clipRange;
	private Vector3 i_position;
	//public UIDraggablePanel uiDraggablePanel;
	public CScrollPanel()
	{
		base.m_componentType = eComponentType.ScrollPanel;
		//m_font = UIRootManager.Ins.font;
	}
	public override void Init()
	{
		//uiGrid = UITool.GetUIGrid(_Go);
		if (uiGrid != null)
		{
			goGrid = uiGrid.gameObject;
		}
		//
		uip = UITool.GetUIPanel(_Go);
		i_clipRange = new Vector4(uip.clipRange.x, uip.clipRange.y, uip.clipRange.z, uip.clipRange.w);
		i_position = uip.gameObject.transform.localPosition;
		//uiDraggablePanel = UITool.GetUIDraggablePanel(_Go);
		//i_position = new Vector3(position.x, position.y, position.z);
		uiScrollBar = UITool.GetUIScrollBar(_Go);
		if (uiScrollBar != null)
		{
			scrollBar = Panel.GetCom(uiScrollBar.gameObject.name) as CScrollBar;
		}
	}

	public void SetPosition(Vector3 position)
	{
		Vector3 p = uip.gameObject.transform.localPosition;
		uip.gameObject.transform.localPosition = position;
		uip.clipRange = new Vector4((uip.clipRange.x - position.x + p.x), uip.clipRange.y - position.y + p.y, uip.clipRange.z, uip.clipRange.w); ;
	}
	public void Reset()
	{
		uip.gameObject.transform.localPosition = i_position;
		uip.clipRange = i_clipRange;
		//uiDraggablePanel.DisableSpring();
	}
	/// <summary>
	/// 初始化方法 必须先调用 过后该为InitScrollPanel(string itemName)
	/// </summary>
	public void InitScrollPanel(string itemName)
	{
		this.itemName = itemName;
	}

	//增加一个组件 注意全添加完之后一定要调Repositon
	public int AddItem()
	{
		//GameObject go = SingletonObject<ComponentFactory>.Ins.GetMemoryObj(itemName);
		GameObject go = ComponentManager.Ins.GetPrefabInstance(itemName);
		//UILabel[] labels = go.GetComponentsInChildren<UILabel>(true);
		//foreach (UILabel label in labels)
		//{
		//    label.font = m_font;
		//}

		CPanel panel = CPanel.CreatePanel(go);
		UITool.AddChild(go, goGrid);
		panel.SetActive(true);

		//UICheckbox checkbox = go.GetComponentInChildren<UICheckbox>();
		//if (checkbox != null)
		//{
		//    //Debug.Log(" goGrid.transform " + goGrid.ToString()) ;

		//    checkbox.radioButtonRoot = goGrid.transform;
		//}
		list.Add(panel);
		return list.Count - 1;//从0开始计数
	}

    public void RemoveItem(int nIndex)
    {
        if (nIndex >= list.Count || nIndex < 0)
        {
            Debug.LogError("CScrollPanel RemoveItem error:nIndex >= list.Count,nIndex:" + nIndex + " count:" + list.Count);
            return;
        }
        ComponentManager.Ins.RemovePrefabInstance(itemName, list[nIndex].Go);
        list.RemoveAt(nIndex);
        //if (list.Count > 0)
        //{
        //    if (nIndex < list.Count)
        //    {
        //        SetSelect(nIndex);
        //    }
        //    else
        //    {
        //        SetSelect(0);
        //    }
        //}
    }
    public void Repositon()
	{
		//uiGrid.repositionNow = true;
		//return;
		int x = 0;
		int y = 0;
		Transform myTrans = goGrid.transform;
		//if (uiGrid.sorted)
		//{
		//}
		//else
		//{
			for (int i = 0, imax = list.Count; i < imax; ++i)
			{
				Transform t = list[i].Go.transform;

				if (!t.gameObject.activeInHierarchy && uiGrid.hideInactive) continue;

				float depth = t.localPosition.z;
				t.localPosition = (uiGrid.arrangement == UIGrid.Arrangement.Horizontal) ?
					new Vector3(uiGrid.cellWidth * x, -uiGrid.cellHeight * y, depth) :
					new Vector3(uiGrid.cellWidth * y, -uiGrid.cellHeight * x, depth);

				if (++x >= uiGrid.maxPerLine && uiGrid.maxPerLine > 0)
				{
					x = 0;
					++y;
				}
			}
		//}

	}

	//public int GetSelectIndex()
	//{
	//    for (int i = 0,cout = list.Count; i < cout;++i )
	//    {
	//        UICheckbox checkbox = list[i].Go.GetComponentInChildren<UICheckbox>();
	//        if (checkbox != null)
	//        {
	//            if (checkbox.isChecked)
	//            {
	//                return i;
	//            }
	//        }
	//    }
	//    return -1;
	//}

	//public void SetSelect(int nIndex)
	//{
	//    if (nIndex >= list.Count || nIndex < 0)
	//    {
	//        Debug.LogError("CScrollPanel SetSelect error:nIndex >= list.Count,nIndex:" + nIndex + "...count:" + list.Count);
	//        return;
	//    }

	//    CPanel panel = list[nIndex];

	//    UICheckbox checkbox = panel.Go.GetComponentInChildren<UICheckbox>();
	//    if (checkbox != null)
	//    {
	//        checkbox.isChecked = true;

	//        ////有时删除邮箱时设置setselect时不会显示高亮状态,暂时不明原因,先就这样解决
	//        //UISprite sprite = checkbox.checkSprite;
	//        //if (sprite != null)
	//        //{
	//        //    sprite.MakePixelPerfect();
	//        //}
	//    }
	//}

	public void ClearItem()
	{
		//goGrid.transform.DetachChildren();
		foreach (CPanel panel in list)
		{
			ComponentManager.Ins.RemovePrefabInstance(itemName, panel.Go);
		}
		list = new List<CPanel>();
		if (scrollBar != null)
		{
			scrollBar.ScrollValue = 0;
		}
	}

	//public void SetActive(int nIndex, string elementName,bool bActive)
	//{
	//    if (nIndex >= list.Count || nIndex < 0)
	//    {
	//        Debug.LogError("CScrollPanel SetActive error:nIndex >= list.Count,nIndex:" + nIndex + "...count:" + list.Count);
	//        return;
	//    }

	//    CPanel panel = list[nIndex];
	//    BaseComponent elementBase = panel.GetElementBase(elementName);
	//    if (elementBase != null)
	//    {
	//        elementBase.SetActive(bActive);
	//    }
	//}

	//public void SetText(int nIndex, string elementName, string text)
	//{
	//    if (nIndex >= list.Count || nIndex < 0)
	//    {
	//        Debug.LogError("CScrollPanel SetText error:nIndex >= list.Count,nIndex:" + nIndex + "...count:" + list.Count);
	//        return;
	//    }

	//    CPanel panel = list[nIndex];
	//    CLabel label = panel.GetElementBase(elementName) as CLabel;
	//    if (label != null)
	//    {
	//        label.SetText(text);
	//    }
	//}

	//public void SetTextColor(int nIndex, string elementName, Color color)
	//{
	//    if (nIndex >= list.Count || nIndex < 0)
	//    {
	//        Debug.LogError("CScrollPanel SetText error:nIndex >= list.Count,nIndex:" + nIndex + "...count:" + list.Count);
	//        return;
	//    }

	//    CPanel panel = list[nIndex];
	//    CLabel label = panel.GetElementBase(elementName) as CLabel;
	//    if (label != null)
	//    {
	//        label.SetColor(color);
	//    }
	//}

	//public void SetSprite(int nIndex, string elementName, string atlasName, string spriteName, 

	//                    string path, bool bIcon ,bool bNeedPerfect  )
	//{
	//    if (nIndex >= list.Count || nIndex < 0)
	//    {
	//        Debug.LogError("CScrollPanel SetSprite error:nIndex >= list.Count,nIndex:" + nIndex + "...count:" + list.Count);
	//        return;
	//    }
	//    CPanel panel = list[nIndex];
	//    CSprite sprite = panel.GetCom(elementName) as CSprite;
	//    if (sprite != null)
	//    {
	//        UITool.LoadSprite(sprite, atlasName, spriteName, path, bIcon,bNeedPerfect);
	//    }
	//}

	//public void SetCheckBoxEvent(int nIndex, string elementName,string index,string tooltip, OnCheckBoxStateChange onStateChange,CheckboxClickDelegate onClick = null)
	//{
	//    if (nIndex >= list.Count || nIndex < 0)
	//    {
	//        Debug.LogError("CScrollPanel SetCheckBoxEvent error:nIndex >= list.Count,nIndex:" + nIndex + "...count:" + list.Count);
	//        return;
	//    }

	//    CPanel panel = list[nIndex];
	//    CCheckbox checkbox = panel.GetElementBase(elementName) as CCheckbox;
	//    if (checkbox != null)
	//    {
	//        checkbox.SetClickDelegate(onClick);
	//        checkbox.SetStateChange(onStateChange);
	//        checkbox.ToolTip = tooltip;
	//        checkbox.Index = index;
	//    }
	//}

	//public void SetButtonEvent(int nIndex, string elementName,string tooltip, ClickDelegate clickDelegate)
	//{
	//    if (nIndex >= list.Count || nIndex < 0)
	//    {
	//        Debug.LogError("CScrollPanel SetButtonEvent error:nIndex >= list.Count,nIndex:" + nIndex + "...count:" + list.Count);
	//        return;
	//    }

	//    CPanel panel = list[nIndex];
	//    CButton btn = panel.GetElementBase(elementName) as CButton;
	//    if (btn != null)
	//    {
	//        btn.SetClickDelegate(clickDelegate);
	//        btn.ToolTip = tooltip;
	//    }
	//}

	//public void SetButtonEvent(int nIndex, string elementName,string index,string tooltip, ClickDelegate clickDelegate)
	//{
	//    if (nIndex >= list.Count || nIndex < 0)
	//    {
	//        Debug.LogError("CScrollPanel SetButtonEvent error:nIndex >= list.Count,nIndex:" + nIndex + "...count:" + list.Count);
	//        return;
	//    }

	//    CPanel panel = list[nIndex];
	//    CButton btn = panel.GetElementBase(elementName) as CButton;
	//    if (btn != null)
	//    {
	//        btn.SetClickDelegate(clickDelegate);
	//        btn.ToolTip = tooltip;
	//        btn.Index = index;
	//    }
	//}

	public void SetScrollAdd(CScrollBar.OnScrollAdd onScrollAdd)
	{
		scrollBar.SetOnScrollAdd(onScrollAdd);
	}

}
