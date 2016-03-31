using System.Collections.Generic;


class VersionConfigVo : BaseConfigVo
{
	enum eConfig
	{
		version,
		desc,
		listCount
	}

	public string version = string.Empty;
	public string desc = string.Empty;


	public override void Init(List<string> list)
	{
		if (list.Count < (int)eConfig.listCount)
		{
			return;
		}

		desc = list[(int)eConfig.desc];
		version = list[(int)eConfig.version];
	}

}

