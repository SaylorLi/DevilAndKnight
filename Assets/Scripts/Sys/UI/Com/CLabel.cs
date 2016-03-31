using UnityEngine;
using System.Collections;

public class CLabel : CGameObject, IGetSetText
{
    public UILabel uiLabel;
    Color defaultColor;
    public CLabel()
    {
        base.m_componentType = eComponentType.Label;
    }
    public override void Init()
    {
        uiLabel = _Go.GetComponent<UILabel>();
        defaultColor = uiLabel.color;
    }
    public void SetText(string s)
    {
        uiLabel.text = s;
    }
    public void SetColor(Color color)
    {
        uiLabel.color = color;
    }
    public void ResetDefaultColor()
    {
        uiLabel.color = defaultColor;
    }
    public Vector2 GetRect()
    {
        Vector3 v=uiLabel.cachedTransform.localScale;
        return new Vector2(v.x, v.y);
    }
    public string Text
    {
        get
        {
            return uiLabel.text;
        }
        set
        {
            uiLabel.text = value;
        }
    }
    //换行文字处理
    //public string WrapText(string text, float maxWidth, int maxLineCount)
    //{
    //    return uiLabel.font.WrapText(text, maxWidth / uiLabel.transform.localScale.y, maxLineCount, uiLabel.supportEncoding,uiLabel.symbolStyle);
    //}


}
