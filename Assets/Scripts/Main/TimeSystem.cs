﻿using System;using System.Collections.Generic;using System.Text;using UnityEngine;public class TimeSystem{    public float LastRealTime = 0.0f;    public float RealTime = 0.0f;    public float LastEngineTime = 0.0f;    public float EngineTime = 0.0f;    //心跳    //private ulong heartbeatTimeID;    //private static DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));    //private ulong startSyncTime = 0;    public void Init()    {        //EngineTime = LastEngineTime = Time.time;        //RealTime = LastRealTime = Time.realtimeSinceStartup;        //测试用        //TimerModule tm = HunterClient.Instance.FindModule("Timer") as TimerModule;        //tm.AddTimer(1000, uint.MaxValue, UpdateDateTime, null);    }    public float Update()    {        LastRealTime = RealTime;        RealTime = Time.realtimeSinceStartup;        LastEngineTime = EngineTime;        EngineTime = Time.time;
        return EngineTime - LastEngineTime;
        return Time.deltaTime;
        //return RealTime - LastRealTime;
    }
    //public void UpdateDateTime(ulong id, object obj)
    //{
    //    DateTime t = DateTime.Now ;
    //    DateTimeString = string.Format("{0}-{1}-{2}  {3}:{4}:{5}", t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
    //}   
    //    public void StartHeartbeat()
    //    {
    //        TimerModule tm = HunterClient.Instance.FindModule("Timer") as TimerModule;
    //        heartbeatTimeID = tm.AddTimer((uint)(LogicTimeSyncPeriod * 1000), uint.MaxValue, HeartBeatRequest);
    //
    //        TcpClient.getInstance().addHandler((int)NetCommandID.HeartbeatRequest, OnHeartbeatRespone);
    //        LogicTime = 0u;
    //    }
    //    public void StopHeartbeat()
    //    {
    //        TimerModule tm = HunterClient.Instance.FindModule("Timer") as TimerModule;
    //        tm.RemoveTimer(heartbeatTimeID);
    //
    //        TcpClient.getInstance().removeHandler((int)NetCommandID.HeartbeatRequest, OnHeartbeatRespone);
    //    }
    //    public void HeartBeatRequest(ulong id)
    //    {
    //        startSyncTime = LogicTime;
    //        OutputPacket pkt = new OutputPacket((int)NetCommandID.HeartbeatRequest);      
    //        TcpClient.getInstance().send(pkt);
    //    }
    //    public void OnHeartbeatRespone(InputPacket pkt)
    //    {
    //        ulong RTT = LogicTime - startSyncTime;
    //        LogicTime =(ulong)(pkt.ReadInt() * 1000) + (ulong )(pkt.ReadInt())+(RTT>>1);
    //    }
    //public void CurrentLogicTime(out uint seconds ,out uint millisecond)
    //{
    //    seconds = (uint)(LogicTime / 1000);
    //    millisecond = (uint)(LogicTime % 1000);
    //}

}