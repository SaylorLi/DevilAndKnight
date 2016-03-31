using UnityEngine;
using System.Collections;
/// <summary>
/// »¬¶¯×é¼þ.
/// </summary>
public class CSlider : CGameObject
{
    private UISlider uiSlider;
    public CSlider()
    {
        base.m_componentType = eComponentType.Slider;
    }
    public override void Init()
    {
        uiSlider = _Go.GetComponent<UISlider>();
    }
    //public void SetValueChange(UISlider.OnValueChange onValueChange)
    //{
    //    uiSlider.onValueChange = onValueChange;
    //}
    public void ForceUpdate()
    {
        uiSlider.ForceUpdate();
    }
}
