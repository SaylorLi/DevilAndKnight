using System;
using UnityEngine;
using System.Collections.Generic;

public class PopUpMediator : BaseMediator
{
    public const string NAME = "PopUpMediator";

    public CButton btn_Canel;
    public CButton btn_Ok;
    private CButton btn_Frame;
    public CButton btn_Sure;
    private CButton btn_PopupBG;
    private VoidDelegate sureDelegate;
    private VoidDelegate canelDelegate;
    private VoidDelegate okDelegate;

    private CLabel lbl_Content;
    private CLabel lbl_Title;
    private CLabel lbl_CanelTitle;
    private CLabel lbl_OkTitle;
    private CLabel lbl_SureTitle;
    //private CLabel lbl_TreasureNum;
    //private CLabel lbl_AwardCD;

    private CSprite sp_FrameBg;
    //private CSprite sp_TreasureIcon;

    private string sureTitle = "确定";
    private string okTitle = "知道了";
    private string canelTitle = "取消";

    private Vector2 vt_BtnPos;
    private Vector2 vt_TitlePos;

    private GameObject go_Btn;
    private GameObject go_Title;
    //private CGameObject go_Treasure;

    PopUpVo mPopUpVo;
    //private CSprite sp_OkBg;

    public override void LoadWin()
    {
        LoadWinComplete(NAME, UIRootManager.Ins.goAnchor.transform.Find(MediatorParent.ELSE + "/panel_popup").gameObject);
    }
    public void CloseWin()
    {
        //Close();
    }
    protected override void FirstOpen()
    {
        SetFix((int)eMediatorDepth.PopUpMediator);
        //
        btn_Canel = panel.GetCom("(Button)btn_Canel") as CButton;
        btn_Canel.SetClickDelegate(ClickCanelDelegate);
        btn_Canel.SetStringSound(SoundConst.SE_CANCEL);
        btn_Ok = panel.GetCom("(Button)btn_Ok") as CButton;
        btn_Ok.SetClickDelegate(ClickOkDelegate);
        btn_Ok.SetStringSound(SoundConst.SE_FIX);

        btn_Sure = panel.GetCom("(Button)btn_Sure") as CButton;
        btn_Sure.SetClickDelegate(ClickSureDelegate);
        btn_Sure.SetStringSound(SoundConst.SE_FIX);

        btn_PopupBG = panel.GetCom("(Button)btn_PopupBG") as CButton;

        lbl_Title = panel.GetCom("(Label)lbl_Title") as CLabel;
        lbl_Content = panel.GetCom("(Label)lbl_Content") as CLabel;
        lbl_OkTitle = panel.GetCom("(Label)lbl_OkTitle") as CLabel;
        lbl_SureTitle = panel.GetCom("(Label)lbl_SureTitle") as CLabel;
        lbl_CanelTitle = panel.GetCom("(Label)lbl_CanelTitle") as CLabel;
        //lbl_TreasureNum = panel.GetCom("(Label)lbl_TreasureNum1") as CLabel;
        //lbl_AwardCD = panel.GetCom("(Label)lbl_AwardCD") as CLabel;

        sp_FrameBg = panel.GetCom("(Sprite)sp_FrameBg1") as CSprite;
        //sp_TreasureIcon = panel.GetCom("(Sprite)sp_TreasureIcon1") as CSprite;
        //sp_OkBg = panel.GetCom("(Sprite)sp_OkBg") as CSprite;

        go_Btn = panel.GetCom("(GameObject)go_Btn").Go;
        go_Title = panel.GetCom("(GameObject)go_Title").Go;
        //go_Treasure = panel.GetCom("(GameObject)go_Treasure") as CGameObject;

        vt_BtnPos = new Vector2(0, -60);
        vt_TitlePos = new Vector2(0, 100);
    }

    public override void UpdateWin()
    {
        btn_Ok.SetEnable(true);
        btn_Sure.SetEnable(true);
        btn_Canel.SetEnable(true);
    }

    private void ShowContent()
    {
        //lbl_Content.SetText(string.Empty);
        //if (mPopUpVo.mIsMiddle)
        //{
        //    lbl_Content.uiLabel.pivot = UIWidget.Pivot.Center;
        //    Vector2 vt = lbl_Content.Go.transform.localPosition;
        //    lbl_Content.Go.transform.localPosition = new Vector2(0,vt.y);
        //}
        //else
        //{
        //    lbl_Content.uiLabel.pivot = UIWidget.Pivot.Left;
        //    lbl_Content.tf.localPosition = new Vector2((-mPopUpVo.mWidth / 2 + 25), lbl_Content.tf.localPosition.y);
        //}

        lbl_Content.SetActive(true);
        lbl_Content.Text = mPopUpVo.content;
    }

    private void ShowTitle()
    {
        if (mPopUpVo.title != string.Empty)
        {
            lbl_Title.SetActive(true);
            lbl_Title.Text = mPopUpVo.title;
        }
        else
        {
            lbl_Title.SetActive(false);
        }
    }

    private void ShowButton()
    {
        if (sureDelegate != null)
        {
            btn_Sure.SetActive(true);
            if (mPopUpVo.mSureTitle != string.Empty)
            {
                lbl_SureTitle.Text = mPopUpVo.mSureTitle;
            }
            else
            {
                lbl_SureTitle.Text = sureTitle;
            }
        }
        else
        {
            btn_Sure.SetActive(false);
        }
        //btn_Sure.SetText("确定");
        if (okDelegate != null)
        {
            btn_Ok.SetActive(true);
            if (mPopUpVo.mOkTitle != string.Empty)
            {
                lbl_OkTitle.Text = mPopUpVo.mOkTitle;
            }
            else
            {
                lbl_OkTitle.Text = okTitle;
            }
        }
        else
        {
            btn_Ok.SetActive(false);
        }

        if (canelDelegate != null)
        {
            btn_Canel.SetActive(true);
        }
        else
        {
            btn_Canel.SetActive(false);
        }

    }

    public void OpenPupUp(PopUpVo vo)
    {
        this.sureDelegate = null;
        this.canelDelegate = null;
        this.okDelegate = null;
        mPopUpVo = vo;
        this.sureDelegate = mPopUpVo.mSureDelegate;
        this.canelDelegate = mPopUpVo.CanelDelegate;
        this.okDelegate = mPopUpVo.OkDelegate;

        Open();
        ShowTitle();
        ShowContent();
        ShowButton();
        RePosition();
    }

    private void ClickCanelDelegate(CButton source)
    {
        Close();
        if (canelDelegate != null)
        {
            canelDelegate();
            //  SoundManager.GetSePlayer().PlaySE(SoundConstants.SE_CANCEL);
        }

    }

    private void ClickOkDelegate(CButton source)
    {
        Close();
        if (okDelegate != null)
        {
            // SoundManager.GetSePlayer().PlaySE(SoundConstants.SE_FIX);
            okDelegate();
        }
        LoadingMediator loadingMediator = MediatorManager.Ins.GetBaseMediator(LoadingMediator.NAME) as LoadingMediator;
        if (loadingMediator.IsOpen)
        {
            loadingMediator.Close();
        }
    }

    private void ClickSureDelegate(CButton source)
    {
        Close();
        if (sureDelegate != null)
        {
            //  SoundManager.GetSePlayer().PlaySE(SoundConstants.SE_FIX);
            sureDelegate();
        }
    }

    private void RePosition()
    {
        //比默认宽度宽时
        lbl_Content.uiLabel.lineWidth = mPopUpVo.lineWidth;
        lbl_Content.Scale = mPopUpVo.fontScale;

        float fontScale = lbl_Content.Scale.x;
        float addHeight = (lbl_Content.uiLabel.relativeSize.y - 1) * fontScale;
        float addWidth = mPopUpVo.lineWidth + 60;

        Vector2 vt = new Vector2(vt_BtnPos.x, vt_BtnPos.y - (addHeight / 2));
        go_Btn.transform.localPosition = vt;

        vt = new Vector2(vt_TitlePos.x, vt_TitlePos.y + (addHeight / 2));
        go_Title.transform.localPosition = vt;

        sp_FrameBg.Scale = new Vector2(addWidth, addHeight + 210);

        float spaceRang = 0.0f;
        float startX = 0.0f;
        int num = 0;
        if (okDelegate != null)
        { num++; }
        if (sureDelegate != null)
        { num++; }
        if (canelDelegate != null)
        { num++; }
        spaceRang = sp_FrameBg.Scale.x / (num + 1);
        startX = (-sp_FrameBg.Scale.x / 2) + spaceRang;

        if (num == 1 || num == 0)
        {
            //sp_OkBg.uiSprite.spriteName = "ui_btn_a03";
        }
        else if (num == 3)
        {
            //sp_OkBg.uiSprite.spriteName = "ui_btn_a02";
        }
        else
        {
            //sp_OkBg.uiSprite.spriteName = "ui_btn_a03";
        }


        btn_Canel.Go.transform.localPosition = new UnityEngine.Vector2(startX, 6);
        btn_Sure.Go.transform.localPosition = new UnityEngine.Vector2(-startX, 6);
    }

    //float fontScale = lbl_VoiceInstruction.Scale.x;
    //   float addHeight = (lbl_VoiceInstruction.uiLabel.relativeSize.y - 1) * fontScale;
    //   sp_VoiceBg.Scale = new Vector2(sp_VoiceBg.Scale.x, addHeight + 90);
    //   Reposition("(GameObject)go_Voice", addHeight);

}
