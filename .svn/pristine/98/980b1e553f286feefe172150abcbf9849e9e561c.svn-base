using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CPanel
{
    private Dictionary<string, CGameObject> dic = new Dictionary<string, CGameObject>();
    //protected int depthOffset;
    private bool m_bLoadAtlas;//是否已经加载过atlas
    private GameObject go;
    public static CPanel CreatePanel(GameObject go)
    {
        //字体.
        //UILabel[] labels = obj.GetComponentsInChildren<UILabel>(true);
        //foreach (UILabel label in labels)
        //{
        //    label.font = UIRootManager.Ins.GetFont();
        //}

        //UIPopupList[] uipls = obj.GetComponentsInChildren<UIPopupList>(true);
        //foreach (UIPopupList uipl in uipls)
        //{
        //    uipl.font = UIRootManager.Ins.GetFont();
        //}

        CPanel panel = new CPanel();
        panel.Go = go;
        panel.Init();
        return panel;
    }
    private CPanel()
    {
    }
    //public void SetDepthOffset(int offset)
    //{
        //depthOffset += offset;
        //foreach (CGameObject c in dic.Values)
        //{
        //    if (c is CLabel)
        //    {
        //        (c as CLabel).uiLabel.depth += offset;
        //    }
        //    else if (c is CSprite)
        //    {
        //        (c as CSprite).uiSprite.depth += offset;
        //    }
        //}
    //}
    //public int GetDepthOffset()
    //{
    //    return depthOffset;
    //}
    public void Init()
    {
        Ergodic(Go);
        InitLabelText();
    }

    private void InitLabelText()
    {
        //Dictionary<uint, CLabelInfo> dict = SingletonObject<CLabelDownload>.GetInst().GetDict();
        //CLabelInfo labelInfo;
        //CElementBase e;
        //foreach (KeyValuePair<uint, CLabelInfo> kvp in dict)
        //{
        //    if (kvp.Value.GetPanelName() == Go.name)
        //    {
        //        labelInfo = kvp.Value;
        //        e = GetElementBase(labelInfo.GetElementName());
        //        if (e is IGetSetText)
        //        {
        //            string str = CGUITextDownload.GetInst().GetText(labelInfo.GetTextID());
        //            //if (labelInfo.GetTextID() == 2182)
        //            //{
        //            //    Log.Print_hjx("InitLabelText   " + str + "  " + labelInfo.GetElementName());
        //            //}
        //            (e as IGetSetText).SetText(str);
        //        }
        //    }
        //}
    }
    private void Ergodic(GameObject go)
    {
        int elementCount = go.transform.childCount;
        //UITool.Log(Go.name+" elementCount=" + elementCount);
        for (int i = 0; i < elementCount; ++i)
        {
            Transform t = go.transform.GetChild(i);
            GameObject childObj = t.gameObject;
            //childObj.layer = (int)eLayer.NGUI;
            string childName = childObj.name;
            if (childName.StartsWith("component"))
            {
                continue;
            }
            AddCom(childName, childObj);
            //
            Ergodic(childObj);
        }
    }

    public void SetActive(bool bShow)
    {
        Go.SetActive(bShow);
        if (bShow)
        {
            if (!m_bLoadAtlas)
            {
                m_bLoadAtlas = true;
                RevertAtlas();
            }
        }
    }

    private void RevertAtlas()
    {
        //Dictionary<uint, CAtlasInfo> dict = SingletonObject<CAtlasDownload>.GetInst().GetDict();


        //if (m_bPopUp)
        //{
        //    CLoadingManager.GetInst().StartAddUrl(eLoadingType.NGUI_ATLAS, eParaType.NONE);
        //}
        
        //CAtlasInfo atlasInfo;
        //foreach (KeyValuePair<uint, CAtlasInfo> kvp in dict)
        //{
        //    //Log.Log_xm("aaa" + kvp.Value.GetPanelName() + "  Go.name" + Go.name);
        //    if (kvp.Value.GetPanelName() == Go.name)
        //    {
        //        atlasInfo = kvp.Value;
        //        string s = MyConvert_Convert.ToString(atlasInfo.GetKey());
        //        LoadHelp.LoadObject(s, UITool.GetAtlasUIRelativePath(atlasInfo.GetAtlasPath()), ThreadPriority.Normal, LoadAtlasCompleted);
        //        //Log.Log_xm("Go.name " + Go.name + " 加载图集:" + UITool.GetAtlasUIRelativePath(atlasInfo.GetAtlasPath()));

        //    }
        //}

        //if (m_bPopUp)
        //{
        //    CLoadingManager.GetInst().EndAddUrl();
        //}
        
        //if (ClientMain.WEB_PLATFORM)
        //{
        //    SingletonObject<AtlasLoadingMediator>.GetInst().Open();
        //    UIManager.GetInst().BringToFont(SingletonObject<AtlasLoadingMediator>.GetInst());
        //}
    }

    private void LoadAtlasCompleted(string interim, UnityEngine.Object asset)
    {
        ////Log.Log_hjx("加载图集成功:" + interim);
        //GameObject o = (GameObject)(asset);
        //UIAtlas atlas = o.GetComponent<UIAtlas>();

        //uint uiKey = MyConvert_Convert.ToUInt32(interim);
        //CAtlasInfo pAtlasInfo = SingletonObject<CAtlasDownload>.GetInst().GetAtlasInfo(uiKey);
        //if (null == pAtlasInfo)
        //{
        //    Debug.LogError("can't find atlas:"+uiKey);
        //    return;
        //}
        //string elementBaseName = pAtlasInfo.GetElementName();

        //List<string> indexList = pAtlasInfo.GetIndexList();
        //if (indexList.Count != 0)
        //{
        //    byte byMin = MyConvert_Convert.ToByte(indexList[0]);
        //    byte byMax = MyConvert_Convert.ToByte(indexList[indexList.Count - 1]);

        //    for (byte byIndex = byMin; byIndex <= byMax; ++byIndex)
        //    {
        //        SetElementBaseAtlas(elementBaseName + byIndex, atlas);
        //    }
        //}
        //else
        //{
        //    SetElementBaseAtlas(elementBaseName, atlas);

        //}
    }

    //private void SetComAtlas(string comName, UIAtlas atlas)
    //{
    //    CGameObject elementBase = GetCom(comName);
    //    if (null == elementBase)
    //    {
    //        return;
    //    }

    //    GameObject obj = elementBase.Go;
    //    if (null == obj)
    //    {
    //        Debug.LogError("panel:" +Go.name + " elementbase"+ comName +" obj is null");
    //        return;
    //    }

    //    Transform child = obj.transform;

    //    if (null == child)
    //    {
    //        return;
    //    }

    //    UISprite[] uis = child.GetComponentsInChildren<UISprite>(true);
    //    foreach (UISprite sp in uis)
    //    {
    //        sp.atlas = atlas;
    //    }

    //    UIPopupList[] uipls = child.GetComponentsInChildren<UIPopupList>(true);
    //    foreach (UIPopupList uipl in uipls)
    //    {
    //        uipl.atlas = atlas;
    //    }
    //}


    //public void AddElementBase<T>(string elementName, GameObject obj) where T : CElementBase
    //{
    //    //Debug.LogError("AddElementBase:panel:"+Go.name+"..."+elementName);
    //    if (m_elementBaseDict.ContainsKey(elementName))
    //    {
    //        Debug.LogError("AddElementBase error,panel:" + Go.name + " has the same key:" + elementName);
    //        return;
    //    }
    //    T elementBase = (T)Activator.CreateInstance(typeof(T));
    //    elementBase.ElementName = elementName;
    //    elementBase.ElementObj = obj;
    //    elementBase.ParentPanel = this;
    //    elementBase.Init();

    //    m_elementBaseDict.Add(elementName, elementBase);
    //}

    public void AddCom(string goName, GameObject go)
    {
        CGameObject goCom = null;
        if (goName.StartsWith("(Button)"))
        {
            goCom = new CButton();
        }
        else if (goName.StartsWith("(Label)"))
        {
            goCom = new CLabel();
        }
        else if (goName.StartsWith("(Sprite)"))
        {
            goCom = new CSprite();
        }
        else if (goName.StartsWith("(Checkbox)"))
        {
            goCom = new CCheckbox();
        }
        else if (goName.StartsWith("(Slider)"))
        {
            goCom = new CSlider();
        }
        else if (goName.StartsWith("(ScrollBar)"))
        {
            goCom = new CScrollBar();
        }
        else if (goName.StartsWith("(ProgressBar)"))
        {
            goCom = new CProgressBar();
        }
        else if (goName.StartsWith("(PopupList)"))
        {
            goCom = new CPopupList();
        }
        else if (goName.StartsWith("(Input)"))
        {
            goCom = new CInput();
        }
        //else if (elementName.StartsWith("(TabButtonGroup)"))
        //{
        //    elementBase = new CTabButtonGroup();
        //}
        else if (goName.Contains("(TabButtonGroup)"))
        {
            goCom = new CTabButtonGroup();
        }
        else if (goName.Contains("(TabButton)"))
        {
            goCom = new CTabButton();
        }
        else if (goName.Contains("(TabPanel)"))
        {
            goCom = new CTabPanel();
        }
        else if (goName.Contains("(ScrollPanel)"))
        {
            goCom = new CScrollPanel();
        }
        else if (goName.StartsWith("(GameObject)"))
        {
            goCom = new CGameObject();
        }
        else
        {
            //Debug.LogWarning(" panel name : " + this.Go.name + " goName:" + goName);
            return;
        }
        if (goCom != null)
        {
            if (dic.ContainsKey(goName))
            {
				Debug.Log("AddElementBase error,panel:" + Go.name + " has the same key:" + goName);
                return;
            }
            //elementBase.Name = elementName;
            goCom.Go = go;
            goCom.Panel = this;
            goCom.Init();
            dic.Add(goName, goCom);
            //Log.Log_hjx("goName :"+goName);
        }
    }

    public CGameObject GetCom(string elementName)
    {
        CGameObject com = null;
        dic.TryGetValue(elementName, out com);

        //if (null == com)
        //{
            //Debug.LogError("panel:" + Go.name + " Can't find elementBase:" + elementName);
        //}
        return com;
    }

    public GameObject Go
    {
        set
        {
            go = value;
        }
        get
        {
            return go;
        }
    }
}
