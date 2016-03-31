using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIRootManager
{
    public static readonly UIRootManager Ins = new UIRootManager();
    //
    public GameObject go;
    public UICamera uiCamera;
    public GameObject goAnchor;
    public AudioListener audioListener;
    //public BattleTutorial battleTutorial;
    public UIRootFitV uiRootFit;
    public Transform tfHudPlayer;
    public Transform tfHudMonster;
    public Transform tfHudPartner;
    public Transform effect;

    public int screenHeight
    {
        get
        {
            return uiRootFit.GetScreenHeight();
        }
    }
    public int screenWidth
    {
        get
        {
            return uiRootFit.GetScreenWidth();
        }
    }
    public void Init()
    {
        go = GameObject.Find("UIRoot(2D)");
        GameObject.DontDestroyOnLoad(go);
        uiRootFit = go.AddComponent<UIRootFitV>();
        //´´½¨UIÉãÏñ»ú
        GameObject cameraObj = go.transform.Find("Camera").gameObject;
        uiCamera = cameraObj.GetComponent<UICamera>();
        //CameraMgr.Ins.SetCameraUI(cameraObj.GetComponent<Camera>());
        audioListener = go.AddComponent<AudioListener>();
        Transform tfAnchor = cameraObj.transform.Find("Anchor");
        goAnchor = tfAnchor.gameObject;
        //
        tfHudPlayer = tfAnchor.Find("hud/player");
        tfHudMonster = tfAnchor.Find("hud/monster");
        tfHudPartner = tfAnchor.Find("hud/partner");
        effect = tfAnchor.Find("effect");
        GameObject goLabel = new GameObject();
        goLabel.AddComponent<UIPanel>();
        goLabel.name = "Log";
        goLabel.layer = LayerMask.NameToLayer(Const.LAYER_NGUI);//Layer
        UITool.AddChild(goLabel, goAnchor);
    }
}
