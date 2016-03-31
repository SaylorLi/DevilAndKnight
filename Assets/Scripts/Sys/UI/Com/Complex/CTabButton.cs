using UnityEngine;
using System.Collections;

public delegate void TClickDelegate(CTabButton source);

public class CTabButton : CCheckbox
{

    private TClickDelegate m_ClickDelegate;
    public UIButton uiButton;

    public void SetClickDelegate(TClickDelegate clickDelegate)
    {
        m_ClickDelegate = clickDelegate;
    }

    private void ClickCallback()
    {
        if (m_ClickDelegate != null)
        {
            m_ClickDelegate(this);
        }
    }

    public CTabButton()
    {
        m_componentType = eComponentType.TabButton;
    }

    public override void Init()
    {
        base.Init();
        ButtonEvent e = _Go.GetComponent<ButtonEvent>();
        if (null == e)
        {
            e = _Go.AddComponent<ButtonEvent>();
        }

        e.ClickCallback = ClickCallback;
        uiButton = _Go.GetComponent<UIButton>();
    }
}
