using System;
using UnityEngine;

public static class Extentions
{
    public static bool HasError(this WWW www)
    {
        return !string.IsNullOrEmpty(www.error);
    }
}
