using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ErrorCodeConfigVo : BaseConfigVo
{
	enum eConfig
	{
		//描述 提示
		//	desc	 tip
		desc,
		tip,
		listCount,
	}

	public string desc=string.Empty;
	public string tip=string.Empty;

	public override void Init(List<string> list)
	{
		if (list.Count < (int)eConfig.listCount)
		{
			return;
		}
		string str;

		str = list[(int)eConfig.desc];
		desc = str;

		str = list[(int)eConfig.tip];
		tip = str;
		//Log.Log_hjx("desc " + desc + "tip " + tip);
	}
}
