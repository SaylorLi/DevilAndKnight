using UnityEngine;
using System.Collections;

public class CInput : CGameObject
{
    public UIInput uiInput;
    public CInput()
    {
        base.m_componentType = eComponentType.Input;
    }
    public override void Init()
    {
        uiInput = _Go.GetComponent<UIInput>();
    }
    ///// <summary>
    ///// 按回车确认的时候调用,主要给聊天框使用.
    ///// </summary>
    ///// <param name="onSubmit"></param>
    //public void SetSubmit(UIInput.OnSubmit onSubmit)
    //{
    //    uiInput.onSubmit = onSubmit;
    //}
    public string Text
    {
        set
        {
            uiInput.value=value;
        }
        get 
        {
            return uiInput.value;
        }
       
    }
    public string defaultText
    {
        set
        {
            uiInput.defaultText = value;
        }
    }
    public int MaxChars
    {
        set
        {
            uiInput.characterLimit = value;
        }
        get
        {
            return uiInput.characterLimit;
        }
    }

    ///// <summary>
    ///// 修正回调 文本变化时调用 可以在这里限制输入内容,简单的如限制只能输入数字可挂一个UIInputValidator脚本
    ///// 黄金星.
    ///// </summary>
    ///// <param name="validator"></param>
    //public void SetValidator(UIInput.Validator validator)
    //{
    //    uiInput.validator = validator;
    //}


    ////限制输入的是数字还是英文或者其他
    //public void SetInputValidator(UIInputValidator.Validation validator)
    //{
    //    UIInputValidator iv = _Go.GetComponent<UIInputValidator>();
    //    if (null == iv)
    //    {
    //        iv = _Go.AddComponent<UIInputValidator>();
    //    }

    //    iv.logic = validator;
    //}

}
