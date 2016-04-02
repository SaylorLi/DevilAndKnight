using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OneInstanceID
{
    uint pt;
    protected uint GetInstanceID()
    {
        if (pt == uint.MaxValue)
        {
            pt = 0;
        }
        return ++pt;
    }
}

public class TimerMgr : OneInstanceID
{
    public static readonly TimerMgr Ins = new TimerMgr();
    // 回调委托
    public delegate void ParamsDelegate(params object[] args);
    //
    private Dictionary<uint, TaskItem> dic = new Dictionary<uint, TaskItem>();
    //
    public void Update(float deltaTime)
    {
        List<uint> list = new List<uint>();
        foreach (TaskItem item in dic.Values)
        {
            item.delay -= deltaTime;
            if (item.delay <= 0)
            {
                item.delay = 0;
                // 引发回调
                item.callback(item.args);
                // 处理repeat
                if (item.repeat)
                {
                    item.delay = item.delayMax;
                }
                else
                {
                    item.Recycle();
                    list.Add(item.InstanceID);
                }
            }
        }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            dic.Remove(list[i]);
        }
    }

    /// <summary>
    /// delayTime后调用
    /// </summary>
    /// <param name="callback">回调函数</param>
    /// <param name="delayTime">等待时间</param>
    /// <param name="isRepeat">是否重复调度</param>
    /// <param name="args">回调参数</param>
    /// <returns>返回任务句柄</returns>
    public uint AddTimer(ParamsDelegate callback, float delayTime = 0, bool isRepeat = false, params object[] args)
    {
        if (callback == null) return 0;
        TaskItem item = new TaskItem(callback, delayTime, isRepeat, args);
        uint pt = item.InstanceID = GetInstanceID();
        dic.Add(pt, item);
        object[] newArgs = null;
        if (args == null || args.Length == 0)
        {
            newArgs = new object[] { pt };
        }
        else
        {
            newArgs = new object[args.Length + 1];
            newArgs[0] = pt;
            for (int i = 1; i < newArgs.Length; i++)
            {
                newArgs[i] = args[i - 1];
            }
        }
        item.args = newArgs;
        return pt;
    }

    public void Reset()
    {
        foreach (TaskItem item in dic.Values)
        {
            item.Recycle();
        }
        dic.Clear();
    }
    /// <summary>
    /// 打断循环计时器
    ///uint pt = uint.Parse(args[0].ToString());//第一个参数是id
    ///Invoker.Instance.CancelTimer(pt);
    /// </summary>
    /// <param name="id"></param>
    public void RemoveTimer(uint id)
    {
        if (dic.ContainsKey(id))
        {
            TaskItem item = dic[id];
            item.Recycle();
            dic.Remove(id);
        }
    }
    /// <summary>
    /// 任务实体
    /// </summary>
    private class TaskItem
    {
        public TaskItem(ParamsDelegate callback, float delay, bool repeat, params object[] args)
        {
            this.callback = callback;
            this.delay = delayMax = delay;
            this.repeat = repeat;
            this.args = args;
        }

        /// <summary>
        /// 回收
        /// </summary>
        public void Recycle()
        {
            callback = null;
            delay = 0;
            delayMax = 0;
            args = null;
        }
        // 回调
        public ParamsDelegate callback;
        // 参数
        public object[] args;
        // 当前剩余的延迟执行时间
        public float delay;
        // 执行的时间间隔，对于repeat时有效
        public float delayMax;
        // 是否重复执行
        public bool repeat;
        public uint InstanceID { get; set; }
    }

}
