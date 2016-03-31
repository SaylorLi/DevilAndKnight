using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CTabButtonGroup : CGameObject
{
    public delegate void OnSelectionChange(int index);
    public OnSelectionChange onSelectionChange;
    //
    private List<UIToggle> listCb = new List<UIToggle>();
    private List<UILabel> listLabel = new List<UILabel>();
    private List<CTabPanel> listPanel = new List<CTabPanel>();
    //private List<UIButton> listUiButton=new List<UIButton>(); 
    private Dictionary<byte, string> tooltips = new Dictionary<byte,string>();
    private UILabel uiLabel;
    private byte num;
    public CTabButtonGroup()
    {
        base.m_componentType = eComponentType.TabButtonGroup;
    }
    public void InitButtonGroup(byte nNum)
    {
        num = nNum;
        for (int i = 0; i < nNum; ++i)
        {
            CTabButton tbtn = Panel.GetCom("(TabButton)" + (i)) as CTabButton;
            if (tbtn != null)
            {
                GameObject go = tbtn.Go;
                UIToggle uiCheckbox = go.GetComponent<UIToggle>();
                //uiCheckbox.onStateChange = StateChange;   
                listCb.Add(uiCheckbox);
                listLabel.Add(UITool.GetUILabel(go));
                tbtn.SetClickDelegate(TClickDelegate);
            }

            CTabPanel tPanel = Panel.GetCom("(TabPanel)" + (i)) as CTabPanel;
            if (tPanel != null)
            {
                listPanel.Add(tPanel);
            }
        }
    }

    public void TClickDelegate(CTabButton tbtn)
    {
        string tbtnName = tbtn.Go.name;
        int index = int.Parse(tbtnName.Substring(tbtnName.Length - 1, 1));
        for (int i = 0; i < listCb.Count; i++)
        {
            if (index == i)
            {
                listCb[i].isChecked = true;
                listPanel[i].SetActive(true);
                if (onSelectionChange != null)
                {
                    onSelectionChange(i);
                }
            }
            else
            {
                listCb[i].isChecked = false;
                listPanel[i].SetActive(false);
            }
        }


    }

    //private void StateChange(bool state)
    //{
    //    if (state)
    //    {
    //        int len = listCb.Count;
    //        for (int i = 0; i < len; ++i)
    //        {
    //            if (listCb[i].isChecked)
    //            {
    //                for (int j = 0, jmax = listPanel.Count; j < jmax; ++j)
    //                {
    //                    if (i == j)
    //                    {
    //                        listPanel[j].SetActive(true);
    //                    }
    //                    else
    //                    {
    //                        listPanel[j].SetActive(false);
    //                    }
    //                }

    //                //if (onSelectionChange != null)
    //                //{
    //                //    onSelectionChange(i);
    //                //}
    //            }
    //        }
    //    }
    //}


    public void SetSelectionChange(OnSelectionChange onSelectionChange)
    {
        this.onSelectionChange = onSelectionChange;
    }
    public void SetSelection(int index)
    {
        if (index < 0 || index >= listCb.Count)
        {
			//Log.Log_sys("CTabButtonGroup SetSelection 参数错误.");
            return;
        }

        CTabButton tbtn = Panel.GetCom("(TabButton)" + (index)) as CTabButton;
        TClickDelegate(tbtn);

        //for (int i = 0; i < listCb.Count; i++)
        //{
        //    if (index == i)
        //    {
        //        listCb[i].isChecked = true;
        //    }
        //    else
        //    {
        //        listCb[i].isChecked = false;
        //    }
        //}
        //if (listCb[index].isChecked)
        //{
        //    StateChange(true);
        //}
    }
    public void SetText(byte byIndex,string s)
    {
        if (byIndex >= listLabel.Count)
        {
			//Log.Log_sys("CTabButtonGroup SetText 参数错误." + s);
            return;
        }

        listLabel[byIndex].text = s;
    }

    public void SetActive(byte byIndex, bool bActive)
    {
        if (byIndex >= listCb.Count)
        {
			//Log.Log_sys("CTabButtonGroup SetActive 参数错误." + byIndex);
            return;
        }
        listCb[byIndex].gameObject.SetActive(bActive);

        if (!bActive)
        {
            listPanel[byIndex].SetActive(false);
        }
    }



    public void RePosition()
    {
        UIGrid grid = _Go.GetComponentInChildren<UIGrid>();
        if (grid != null)
        {
            grid.Reposition();
        }
    }


    public void SetToolTip(byte byIndex, string tooltip)
    {
        if (byIndex >= listCb.Count)
        {
			//Log.Log_sys("CTabButtonGroup SetToolTip 参数错误." + byIndex);
            return;
        }
        if (tooltips.ContainsKey(byIndex))
        {
            tooltips[byIndex] = tooltip;
        }
        else
        {
            tooltips.Add(byIndex, tooltip);
        }
        
    }

    public string GetToolTip(byte byIndex)
    {
        string tooltip = "";
        if (tooltips.ContainsKey(byIndex))
        {
            tooltip = tooltips[byIndex];
        }
        return tooltip;
    }

    public byte GetTabButtonCount()
    {
        return num;
    }


}

