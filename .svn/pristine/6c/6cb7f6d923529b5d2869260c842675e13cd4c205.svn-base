using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;


public class CalcMD5
{
    /// <summary>
    /// 获取指定字节数组的SHA1值
    /// </summary>
    public static string CheckData(byte[] data)
    {
        MD5 md = new MD5CryptoServiceProvider();
        byte[] bytes = md.ComputeHash(data);
        md.Clear();
        return ToHex(bytes);
    }
    public static string CheckString(string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        return CheckData(bytes);
    }
    /// <summary>
    /// 获取指定路径的文件的SHA1值
    /// </summary>
    public static string CheckFile(string path)
    {
        if (File.Exists(path))
        {
            byte[] data = File.ReadAllBytes(path);
            return CheckData(data);
        }
        return string.Empty;
    }

    private static string ToHex(byte[] hashBytes)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            builder.Append(hashBytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
    //
    
}
