using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(UIRoot))]
public class UIRootFitH : MonoBehaviour
{
	private const int STANDARD_WIDTH = 1136;
	private const int STANDARD_HEIGHT = 640;

	private const float ASPECT_RATIO_1136_640 = 1.775f;//1136/640
	private const float ASPECT_RATIO_960_640 = 1.5f;//960/640
	private const float ASPECT_RATIO_1024_768 = 1.33f;//1024/768

	public bool runOnlyOnce = false;
    public UIRoot uiRoot;
    //
	private int screenHeight;
	private int screenWidth;
	private int count;

	void Awake()
	{
		//UICamera.onScreenResize += ScreenSizeChanged;
		//uiRoot = NGUITools.FindInParents<UIRoot>(this.gameObject);
		//uiRoot.scalingStyle = UIRoot.Scaling.FixedSize;
		uiRoot.scalingStyle = UIRoot.Scaling.ConstrainedOnMobiles;
        count = 0;
		this.UpdateWin();
	}

	void OnDestroy()
	{
		//UICamera.onScreenResize -= ScreenSizeChanged;
	}

	void Start()
	{
	}
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
	public int GetScreenWidth()
	{
		return screenWidth;
	}
	public int GetScreenHeight()
	{
		return screenHeight;
	}
	private float CalScreenWidth()
	{
		return STANDARD_HEIGHT * 1f / Screen.height * Screen.width;
	}
	//private float CalRootHeight()
	//{
	//	return STANDARD_WIDTH * 1f / Screen.width * Screen.height;
	//}
	void UpdateWin()
	{
		float currentAspectRatio = Screen.width * 1f / Screen.height;
		//Log.Log_hjx("currentAspectRatio " + currentAspectRatio);

		if (currentAspectRatio > ASPECT_RATIO_1136_640 - 0.01f)
		{
			uiRoot.manualHeight = STANDARD_HEIGHT;
			screenWidth = STANDARD_WIDTH;
			screenHeight = STANDARD_HEIGHT;
			//Log.Log_hjx(">=ASPECT_RATIO_1136_640");
		}
		else if (currentAspectRatio < ASPECT_RATIO_960_640 - 0.01f)
		{
			//Log.Log_hjx("< ASPECT_RATIO_960_640");
			uiRoot.manualHeight = Mathf.FloorToInt(STANDARD_WIDTH * 1f / currentAspectRatio);
			screenWidth = STANDARD_WIDTH;
			screenHeight = STANDARD_HEIGHT;
		}
		else
		{
			//Log.Log_hjx(">= ASPECT_RATIO_960_640");
			uiRoot.manualHeight = STANDARD_HEIGHT;
			screenWidth = Mathf.FloorToInt(CalScreenWidth());
			screenHeight = STANDARD_HEIGHT;
		}
		//Log.Log_hjx("screenHeight " + screenHeight + " screenWidth " + screenWidth);

		if (runOnlyOnce)
		{
			this.enabled = false;
		}
	}
}