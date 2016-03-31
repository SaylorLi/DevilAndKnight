using UnityEngine;
using System.Collections;

/// <summary>
/// 控件类型.
/// </summary>
public enum eComponentType
{
	GameObject,
	Sprite,
	Button,
	ImageButton,
	Input,
	Label,
	PopupList,
	//PopupMenu,
	ProgressBar,
	ScrollBar,
	Slider,
	Checkbox,
	//复杂控件.
	ScrollPanel,
	//
	TabButtonGroup,
	TabButton,
	TabPanel
}

public class CGameObject
{
	protected eComponentType m_componentType;//控件类型.
	protected GameObject _Go;//主物体.
	protected CPanel _CPanel;
	public CGameObject()
	{
	}
	public GameObject Go
	{
		set
		{
			_Go = value;
			//m_Obj.layer = (int)eLayer.NGUI;
			tf = _Go.transform;
		}
		get
		{
			return _Go;
		}
	}


	public CPanel Panel
	{
		set
		{
			_CPanel = value;
		}
		get
		{
			return _CPanel;
		}
	}

	public void SetActive(bool bActive)
	{
		_Go.SetActive(bActive);
	}
	//public bool GetActive()
	//{
	//	return m_Obj.activeInHierarchy;
	//}
	public virtual void Init()
	{
	}
	public GameObject FindChild(string name)
	{
		Transform t = tf.Find(name);
		//if (null == t)
		//{
		//	Debug.LogWarning("BaseComponent" + m_name + "can't find child:" + objName);
		//}
		return t == null ? null : t.gameObject;
	}

	//
	public Vector2 Position
	{
		set
		{
			tf.localPosition = new Vector3(value.x, value.y, tf.localPosition.z);
		}
		get
		{
			return tf.localPosition;
		}
	}
	public Vector2 Scale
	{
		set
		{
			tf.localScale = new Vector3(value.x, value.y, tf.localScale.z);
		}
		get
		{
			return tf.localScale;
		}
	}

    public Transform tf { get; private set; }
}
