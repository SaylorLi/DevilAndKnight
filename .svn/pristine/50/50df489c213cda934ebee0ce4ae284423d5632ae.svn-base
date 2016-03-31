using UnityEngine;
using System.Collections;

public class CProgressBar : CGameObject
{
    private UISlider uiSlider;
    public CProgressBar()
    {
        base.m_componentType = eComponentType.ProgressBar;
    }
    public override void Init()
    {
        uiSlider = _Go.GetComponent<UISlider>();
    }
    //public void SetValueChange(UISlider.OnValueChange onValueChange)
    //{
    //    uiSlider.onValueChange = onValueChange;
    //}
    public void SetValue(float sliderValue)
    {
        uiSlider.sliderValue = sliderValue;
    }
    public float GetValue()
    {
        return uiSlider.sliderValue;
    }
}
