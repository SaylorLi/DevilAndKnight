using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class DebugUtil
{
    [Conditional("DEBUG")]
    public static void Assert(bool condition, string msg)
    {
        if (!condition)
        {
            StackFrame frame = new StackFrame(1, true);
            string fileName = frame.GetFileName();
            int fileLineNumber = frame.GetFileLineNumber();
            LogError("Assertion Failed!! " + msg + " - File: " + fileName + " Line: " + fileLineNumber.ToString());
        }
    }

    public static float GetUsedMemoryMB()
    {
        return ((((float) Profiler.usedHeapSize) / 1024f) / 1024f);
    }

    public static void LogError(string log)
    {
        if (log != string.Empty)
        {
            UnityEngine.Debug.LogError(log);
        }
    }


}

