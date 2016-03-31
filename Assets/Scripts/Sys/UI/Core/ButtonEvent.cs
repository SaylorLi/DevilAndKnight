using UnityEngine;
using System.Collections;

public class ButtonEvent : MonoBehaviour
{
    public delegate void Click();
    public delegate void PressDown();
    public delegate void PressUp();
    public delegate void HoverIn();
    public delegate void HoverOut();

    public Click ClickCallback;
    public PressDown PressDownCallback;
    public PressUp PressUpCallback;
    public HoverIn HoverInCallback;
    public HoverOut HoverOutCallback;

    void OnClick()
    {
        if (ClickCallback != null)
        {
            ClickCallback();
        }
    }

    void OnPress(bool isPressed)
    {
        if (isPressed)
        {
            if (PressDownCallback != null)
            {
                PressDownCallback();
            }
        }
        else
        {
            if (PressUpCallback != null)
            {
                PressUpCallback();
            }
        }
    }

    void OnHover(bool isOver)
    {
        if (isOver)
        {
            if (HoverInCallback != null)
            {
                HoverInCallback();
            }
        }
        else
        {
            if (HoverOutCallback != null)
            {
                HoverOutCallback();
            }
        }
    }
}
