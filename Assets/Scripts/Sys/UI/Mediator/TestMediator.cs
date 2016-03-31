using UnityEngine;
using System.Collections;

public class TestMediator : BaseMediator
{
    public const string NAME = "test";

    private CButton btnClose;//关闭按钮

    public override void LoadWin()
    {
        LoadWinComplete(NAME, UIRootManager.Ins.goAnchor.transform.Find("else/panel_test").gameObject);
    }
    protected override void FirstOpen()
    {
        SetFix((int)eMediatorDepth.PartnerSkillMediator);
        collider.enabled = false;

        btnClose = panel.GetCom("(Button)btn_Close") as CButton;
        btnClose.SetClickDelegate(ClickDelegate);
    }

    private void ClickDelegate(CButton source = null)
    {
        if (source == btnClose)
        {
            this.Close();
        }
    }
}
