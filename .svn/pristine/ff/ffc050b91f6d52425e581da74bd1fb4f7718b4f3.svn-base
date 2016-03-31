using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CPopupList : CGameObject, IGetSetText
{
    public List<string> list = new List<string>();
    private UIPopupList uiPopupList;
    public CPopupList()
    {
        base.m_componentType = eComponentType.PopupList;
    }

    public override void Init()
    {
        uiPopupList = _Go.GetComponent<UIPopupList>();
    }
    //public void SetSelectionChange(UIPopupList.OnSelectionChange onSelectionChange)
    //{
    //    uiPopupList.onSelectionChange = onSelectionChange;
    //}
    //public void SetSelection(string s)
    //{
    //    uiPopupList.selection = s;
    //}
    public void SetSelection(int index)
    {
        if (index < 0 || index >= list.Count)
        {
			//Log.Log_sys("CPopupList SetSelection ²ÎÊý´íÎó.");
            return;
        }
        uiPopupList.selection = list[index];
    }
    void IGetSetText.SetText(string s)
    {
        string[] arr = s.Split("\n"[0]);
        list = new List<string>();
        for (int i = 0; i < arr.Length; i++)
        {
            list.Add(arr[i]);
        }
        uiPopupList.items = list;
    }

    public List<string> GetItem()
    {
        return uiPopupList.items;
    }

    //string IGetSetText.GetText()
    //{
    //    return uiPopupList.selection;
    //}
   
}
