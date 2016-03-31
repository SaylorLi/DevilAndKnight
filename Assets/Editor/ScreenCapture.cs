using UnityEngine;
using UnityEditor;

using System.IO;
using System.Collections;
using System;

public class ScreenCapture
{
    [MenuItem("TM/CaptureScreenshot")]
    public static void CaptureScreenshot()
    {
        string parent = Application.dataPath + "/../Screenshot/";
        if (!Directory.Exists(parent))
            Directory.CreateDirectory(parent);
        string path = parent + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".png";
        Application.CaptureScreenshot(path);
    }
}
