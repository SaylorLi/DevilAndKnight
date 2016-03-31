using UnityEngine;
using System.Collections;

public class UIRootFitV : MonoBehaviour
{
    private const int STANDARD_WIDTH = 720;
    private const int STANDARD_HEIGHT = 1280;

    private const float ASPECT_RATIO_720_1280 = 0.563f;//720/1280
    private const float ASPECT_RATIO_720_1080 = 0.667f;//720/1080
    private const float ASPECT_RATIO_720_960 = 0.75f;//720/960

    public bool runOnlyOnce = true;
    public UIRoot uiRoot;
    //
    private int screenHeight;
    private int screenWidth;
    private int count;

    void Awake()
    {
        //UICamera.onScreenResize += ScreenSizeChanged;
        uiRoot = UIRootManager.Ins.go.GetComponent<UIRoot>();
        //uiRoot.scalingStyle = UIRoot.Scaling.FixedSize;
        uiRoot.scalingStyle = UIRoot.Scaling.ConstrainedOnMobiles;
        count = 0;
        this.UpdateWin();
    }

    //void OnDestroy()
    //{
        //UICamera.onScreenResize -= ScreenSizeChanged;
    //}
    //void Start()
    //{
    //}
    void Update()
    {
        if (++count > 40)
        {
            count = 0;
            UpdateWin();
        }
    }
    //void ScreenSizeChanged()
    //{
    //	if (mStarted && runOnlyOnce)
    //	{
    //		this.UpdateWin();
    //	}
    //}
    public int GetScreenHeight()
    {
        return uiRoot.manualHeight;
    }
    public int GetScreenWidth()
    {
        return STANDARD_WIDTH;
    }
    //private float CalScreenWidth()
    //{
    //    return STANDARD_HEIGHT * 1f / Screen.height * Screen.width;
    //}
    void UpdateWin()
    {
        float currentAspectRatio = Screen.width * 1f / Screen.height;
        Log.Log_hjx("currentAspectRatio " + currentAspectRatio);

        if (currentAspectRatio < ASPECT_RATIO_720_1280 + 0.01f)
        {
            Log.Log_hjx("<=ASPECT_RATIO_720_1280");
            uiRoot.manualHeight = STANDARD_HEIGHT;
        }
        else if (currentAspectRatio < ASPECT_RATIO_720_960 + 0.01f)
        {
            Log.Log_hjx("<= ASPECT_RATIO_720_960");
            uiRoot.manualHeight = (int)(STANDARD_WIDTH * 1f / currentAspectRatio);
        }
        else
        {
            Log.Log_hjx(">ASPECT_RATIO_720_1080");
            uiRoot.manualHeight = (int)(STANDARD_WIDTH * 1f / currentAspectRatio);
        }
        Log.Log_hjx("screenHeight " + screenHeight + " screenWidth " + STANDARD_WIDTH);

        if (runOnlyOnce)
        {
            this.enabled = false;
        }
    }
}