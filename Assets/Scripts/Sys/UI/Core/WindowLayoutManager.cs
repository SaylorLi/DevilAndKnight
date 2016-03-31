using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 窗口布局管理器,暂时只有互斥组的功能.
/// </summary>
public class WindowLayoutManager
{
    public static readonly WindowLayoutManager Ins = new WindowLayoutManager();
    public enum eMediatorExclusionGroup
    {
        Main,
        Item
    }
    private Dictionary<BaseMediator, int> m_MediatorIdExclusionDict = new Dictionary<BaseMediator, int>();
    private Dictionary<int, List<BaseMediator>> m_IdListExclusionDict = new Dictionary<int, List<BaseMediator>>();
    /// <summary>
    /// 互斥组
    /// </summary>

    public void AddMediator(BaseMediator baseMediator, eMediatorExclusionGroup e)
    {
        int id = (int)e;
        m_MediatorIdExclusionDict.Add(baseMediator, id);
        //
        List<BaseMediator> list;
        if (!m_IdListExclusionDict.ContainsKey(id))
        {
            list = new List<BaseMediator>();
            m_IdListExclusionDict.Add(id, list);
        }
        else
        {
            list = m_IdListExclusionDict[id];
        }
        list.Add(baseMediator);
    }

    public void ExclusionGroup(BaseMediator bm)
    {
        int id;
        if (m_MediatorIdExclusionDict.ContainsKey(bm))
        {
            id = m_MediatorIdExclusionDict[bm];
        }
        else
        {
            return;
        }
        List<BaseMediator> list = m_IdListExclusionDict[id];
        BaseMediator baseMediator;
        for (int i = 0, imax = list.Count; i < imax; ++i)
        {
            baseMediator = list[i];
            if (baseMediator != bm)
            {
                baseMediator.Close();
            }
        }
    }
}
