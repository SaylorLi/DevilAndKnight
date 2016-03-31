using System.Collections.Generic;

public abstract class BaseConfigVo {
	public string id;		// iD
    public uint uid;
    protected List<string> listConfig;

    public virtual void Init(List<string> list)
    {
        listConfig = list;
    }
    protected string GetString(int enumId)
    {
        return listConfig[enumId];
    }
    protected int GetInt(int enumId)
    {
        return int.Parse(listConfig[enumId]);
    }
    protected uint GetUint(int enumId)
    {
        return uint.Parse(listConfig[enumId]);
    }
    protected float GetFloat(int enumId)
    {
        return float.Parse(listConfig[enumId]);
    }
}
