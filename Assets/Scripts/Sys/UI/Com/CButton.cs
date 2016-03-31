using UnityEngine;
using System.Collections;

public delegate void ClickDelegate(CButton source);
public delegate void PressDownDelegate(CButton source);
public delegate void PressUpDelegate(CButton source);
public delegate void HoverInDelegate(CButton source);
public delegate void HoverOutDelegate(CButton source);

public class CButton : CGameObject, IGetSetText
{
	private ClickDelegate m_ClickDelegate;
    private PressDownDelegate m_PressDownDelegate;
    private PressUpDelegate m_PressUpDelegate;
    private HoverInDelegate m_HoverInDelegate;
    private HoverOutDelegate m_HoverOutDelegate;

    public UILabel uiLabel;
	public UIButton uiButton;
	private UISprite uiSprite;
	private string spriteNameNormal;
	private string spriteNameDisable;
	private string strSound = string.Empty;
	//private Color defaultColor;
	private bool isChangeSprite;

	public CButton()
	{
		m_componentType = eComponentType.Button;
	}
	public void SetSprite(UISprite uiSprite, string spriteNameNormal, string spriteNameDisable)
	{
		this.uiSprite = uiSprite;
		this.spriteNameNormal = spriteNameNormal;
		this.spriteNameDisable = spriteNameDisable;
		isChangeSprite = true;
	}
	//public void SetLabelGray(bool b)
	//{
	//	if (uiLabel != null)
	//	{
	//		if (b)
	//		{
	//			uiLabel.color = defaultColor;
	//		}
	//		else
	//		{
	//			uiLabel.color = Color.gray;
	//		}
	//	}
	//}
	public void SetEnable(bool isEnabled)
	{
        //if (canClick)
        //{
        //    uiButton.UpdateColor(isEnabled, true);
        //}
        //else
        //{
			uiButton.isEnabled = isEnabled;
        //}
		if (isChangeSprite)
		{
			uiSprite.spriteName = isEnabled ? spriteNameNormal : spriteNameDisable;
		}

	}
	public void SetStringSound(string str)
	{
		//strSound = str;
	}
	public void SetClickDelegate(ClickDelegate clickDelegate)
	{
		m_ClickDelegate = clickDelegate;
        strSound = SoundConst.se_button_click;
    }
	private void ClickCallback()
	{
		if (m_ClickDelegate != null)
		{
			m_ClickDelegate(this);
		}
		SoundManager.Ins.sePlayer.PlaySE(strSound);
	}

    public void SetPressDownDeleate(PressDownDelegate pressDownDelegate)
    {
        m_PressDownDelegate = pressDownDelegate;
    }

    private void PressDownCallback()
    {
        if (m_PressDownDelegate != null)
        {
            m_PressDownDelegate(this);
        }
    }
    
    public void SetPressUpDeleate(PressUpDelegate pressUpDelegate)
    {
        m_PressUpDelegate = pressUpDelegate;
    }

    private void PressUpCallback()
    {
        if (m_PressUpDelegate != null)
        {
            m_PressUpDelegate(this);
        }
    }

    public void SetHoverInDeleate(HoverInDelegate hoverInCallback)
    {
        m_HoverInDelegate = hoverInCallback;
    }

    private void HoverInCallback()
    {
        if (m_HoverInDelegate != null)
        {
            m_HoverInDelegate(this);
        }
    }

    public void SetHoverOutDeleate(HoverOutDelegate hoverOutCallback)
    {
        m_HoverOutDelegate = hoverOutCallback;
    }

    private void HoverOutCallback()
    {
        if (m_HoverOutDelegate != null)
        {
            m_HoverOutDelegate(this);
        }
    }

    public override void Init()
	{
		ButtonEvent e = _Go.GetComponent<ButtonEvent>();
		if (null == e)
		{
			e = _Go.AddComponent<ButtonEvent>();
		}

		e.ClickCallback = ClickCallback;
	    e.PressDownCallback = PressDownCallback;
        e.PressUpCallback = PressUpCallback;
        e.HoverInCallback = HoverInCallback;
        e.HoverOutCallback = HoverOutCallback;

        uiLabel = UITool.GetUILabel(_Go);
		uiButton = _Go.GetComponent<UIButton>();

		//if (uiLabel != null)
		//{
		//	defaultColor = uiLabel.color;
		//}
	}

	public void SetText(string s)
	{
		uiLabel.text = s;
	}

	public string GetText()
	{
		return uiLabel.text;
	}

	
}
