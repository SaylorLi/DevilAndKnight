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
    ///// ���س�ȷ�ϵ�ʱ�����,��Ҫ�������ʹ��.
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
    ///// �����ص� �ı��仯ʱ���� ����������������������,�򵥵�������ֻ���������ֿɹ�һ��UIInputValidator�ű�
    ///// �ƽ���.
    ///// </summary>
    ///// <param name="validator"></param>
    //public void SetValidator(UIInput.Validator validator)
    //{
    //    uiInput.validator = validator;
    //}


    ////��������������ֻ���Ӣ�Ļ�������
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
