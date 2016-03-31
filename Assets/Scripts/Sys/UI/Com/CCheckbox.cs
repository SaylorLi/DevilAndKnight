using UnityEngine;
using System.Collections;


public class CCheckbox : CGameObject
{
	private string strSound = string.Empty;
	private UIToggle uiToggle;
	private UILabel uiLabel;
	private Color labelColor;
	public bool isFirst = true;
	private bool enabled;
	public CCheckbox()
	{
		m_componentType = eComponentType.Checkbox;
	}
	public void SetStringSound(string str)
	{
		strSound = str;
	}
    
	public bool Enable
	{
		get { return enabled; }
		set
		{
			enabled = value;
			if (value)
			{
				if (uiLabel != null)
				{
					uiLabel.color = labelColor;
				}

				Go.GetComponent<Collider>().enabled = true;
			}
			else
			{
				Go.GetComponent<Collider>().enabled = false;
				if (uiLabel != null)
				{
					uiLabel.color = Color.gray;
				}
			}

		}
	}
	public bool IsChecked
	{
		get { return uiToggle.value; }
		set
		{
			uiToggle.value = value;
		}
	}
	public override void Init()
	{
		uiToggle = _Go.GetComponent<UIToggle>();

		//uiLabel = UITool.GetUILabel(_Go);

		//if (uiLabel != null)
		//{
		//	labelColor = uiLabel.color;
		//}
	}

}
