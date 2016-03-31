using System;
using System.Collections.Generic;
using UnityEngine;
public enum eLoadingMediatorType
{
	None,
	black,//黑一下
	loading_in,//打开阵容等界面
	loading_out,
	//battle,//进战斗之前 带tips
	download,//加载资源
	//
}
public class LoadingMediator : BaseMediator
{
	//
	public const string NAME = "LoadingMediator";
	//
	private float time;
	//
	private CGameObject go_tips;
	private CGameObject go_conn;
	private CGameObject go_loading;
	//
	private CSprite sp_LoadingBg1;
	public eLoadingMediatorType type;
	public VoidDelegate voidDelegate;
	private CSprite sp_BlockBg;
	//public bool isAutoClose;
	public override void LoadWin()
	{
        LoadWinComplete(NAME, UIRootManager.Ins.goAnchor.transform.Find("else/panel_loading").gameObject);
	}

	protected override void FirstOpen()
	{
		SetFix((int)eMediatorDepth.LoadingMediator);
		//
		go_tips = panel.GetCom("(GameObject)go_tips") as CGameObject;
		go_conn = panel.GetCom("(GameObject)go_connect") as CGameObject;
		go_loading = panel.GetCom("(GameObject)go_loading") as CGameObject;
		//
		sp_LoadingBg1 = panel.GetCom("(Sprite)sp_LoadingBg1") as CSprite;
		sp_BlockBg = panel.GetCom("(Sprite)sp_BlockBg") as CSprite;
	}

	public override void UpdateWin()
	{
	}
	public void OpenLoading(eLoadingMediatorType type, VoidDelegate voidDelegate)
	{
		Open();
		this.voidDelegate = voidDelegate;
		//Log.Log_hjx("type " + type + "  " + sp_LoadingBg1.uiSprite.alpha);
		if (this.type == type)
		{
			return;
		}

		this.type = type;
		if (type == eLoadingMediatorType.black)
		{
			time = 0;
			SetLoadingBgAlpha(1f);
			ShowLoading();
		}
		else if (type == eLoadingMediatorType.loading_in)
		{
			time = 0;
			SetLoadingBgAlpha(0f);
			ShowLoading();
		}
		else if (type == eLoadingMediatorType.loading_out)
		{
			//Log.Log_hjx("type " + type);
			time = 0;
			//SetLoadingBgAlpha(1f);
			HiddleLoading();
			ProcessDelegate();
		}
		else if (type == eLoadingMediatorType.download)
		{
			time = 0;
			go_conn.SetActive(true);
		}
	}
	public override void Update(float deltaTime)
	{
		//Log.Log_hjx(" Time.deltaTime " + Time.deltaTime + " type" + type);
		if (type == eLoadingMediatorType.black)
		{
			time += deltaTime;
			//SetLoadingBgAlpha(1 - time);
			if (time >= 1f)
			{
				ProcessDelegate();
				Close();
			}
		}
		else if (type == eLoadingMediatorType.loading_in)
		{
			time += deltaTime;
			//Log.Log_hjx("voidDelegatevoidDelegatevoidDelegate " + time);
			SetLoadingBgAlpha(time * 2);
			if (time >= 0.5f)
			{
				ProcessDelegate();
			}
		}
		else if (type == eLoadingMediatorType.loading_out)
		{
			time += deltaTime;
			//SetLoadingBgAlpha(1 - time);
			if (time >= 0.2f)
			{			
				Close();
			}

		}
		//

	}

	private void ProcessDelegate()
	{
		if (voidDelegate != null)
		{
			VoidDelegate tmp = voidDelegate;
			voidDelegate();
			if (tmp == voidDelegate)
			{
				voidDelegate = null;
			}
		}
	}
	private void SetLoadingBgAlpha(float f)
	{
		//Log.Log_hjx("f:" + f + "  sp_BlockBg.uiSprite.alpha " + sp_LoadingBg1.uiSprite.alpha.ToString("f5"));
		if (f < 0)
		{
			f = 0;
		}
		else if (f > 1)
		{
			f = 1;
		}
		//sp_BlockBg.uiSprite.alpha = f;
		sp_LoadingBg1.uiSprite.alpha = f;
	}

	public override void Close(CButton source = null)
	{
		go_conn.SetActive(false);
		//CloseWin();
		base.Close();
		if (voidDelegate != null)
		{
			voidDelegate();
			voidDelegate = null;
		}
	}
	public override void DestoryWin()
	{
		voidDelegate = null;
		base.DestoryWin();
	}
	private void ShowTips()
	{
		go_tips.Go.SetActive(true);
	}

	private void HiddleTips()
	{
		go_tips.Go.SetActive(false);
	}

	private void ShowLoading()
	{
		go_loading.Go.SetActive(true);
	}

	private void HiddleLoading()
	{
		go_loading.Go.SetActive(false);
	}



}
