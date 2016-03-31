using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class CSV
{
    public CSV() { }
    /// <summary>
    /// 构建CSV对象
    /// </summary>
    /// <param name="stream">流对象</param>
    public CSV(Stream stream)
    {
        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        Load(reader.ReadToEnd());
        reader.Close();
        stream.Close();
    }

    /// <summary>
    /// 加载文本
    /// </summary>
    /// <param name="text"></param>
    public void Load(string text)
    {
        text = text.Replace("\r", "").Trim();
        string[] arr = text.Split('\n');
        if (arr == null || arr.Length < 1)
        {
            Debug.LogError("不合法的CSV文件");
            return;
        }
        // 获取中文标题
        {
            mCnLine = arr[0];
        }
        // 获取英文标题
        {
            mEnLine = arr[1];
            if (string.IsNullOrEmpty(mEnLine)) return;
            mENTitles = mEnLine.Split(',');
            // 保证标题的干净
            for (int i = 0, len = mENTitles.Length; i < len; i++)
            {
                mENTitles[i] = mENTitles[i].Trim();
            }
            // 判断是否有相同的字段
            // if (HasRepetitiveColumn(ref mENTitles)) return;
            mColumnNumber = mENTitles.Length;
        }
        // 判断是否有记录
        mRowNumber = arr.Length - 2;
        if (mRowNumber == 0)
        {
            // 考虑到可能要插入数据，所以初始化一定的空间
            mContent = new List<Dictionary<string, string>>(10);
            return;
        }
        // 获取记录
        mContent = new List<Dictionary<string, string>>(mRowNumber);
        // 循环读入记录
        for (int i = 0; i < mRowNumber; i++)
        {
            // 过滤掉空行
            if (string.IsNullOrEmpty(arr[i + 2]))
                continue;
            string[] tmpArr = arr[i + 2].Split(',');
            // 查看Mono源码，了解Capacity的生成机制 (int)(capacity/0.9f) + 1;
            Dictionary<string, string> item = new Dictionary<string, string>(mColumnNumber);
            for (int j = 0; j < mColumnNumber; j++)
            {
                item.Add(mENTitles[j], tmpArr[j]);
            }
            mContent.Add(item);
        }
        mContent.TrimExcess();
        if (!VerifyCSV())
        {
            //Logger.w("CSV文件校验不通过");
            Recycle();
            return;
        }
        OnCSVLoaded();
    }

    /// <summary>
    /// 采用打开时所采用的编码方式保存CSV到指定位置
    /// </summary>
    /// <param name="path"></param>
    public void Save(string path)
    {
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
        if (File.Exists(path))
            File.Delete(path);
        File.Create(path).Close();
        Save(File.OpenWrite(path), Encoding.UTF8);
    }

    /// <summary>
    /// 保存CSV文件，编码为打开时采用的编码方式
    /// </summary>
    /// <param name="stream">输出流</param>
    public void Save(Stream stream, Encoding encoding)
    {
        StreamWriter writer = new StreamWriter(stream, encoding);
        // 1.写入中文文标题
        writer.WriteLine(mCnLine);
        writer.WriteLine(mEnLine);
        // 2.写入内容
        StringBuilder builder = new StringBuilder(mColumnNumber * 2 - 1);
        foreach (Dictionary<string, string> line in mContent)
        {
            for (int i = 0; i < mColumnNumber; i++)
            {
                builder.Append(line[GetColName(i)]);
                if ((i + 1) != mColumnNumber)
                {
                    builder.Append(",");
                }
            }
            writer.WriteLine(builder.ToString());
            builder.Remove(0, builder.Length);
            writer.Flush();
        }
        writer.Close();
    }

    /// <summary>
    /// 检测是否有重复列(内容)
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    protected virtual bool HasRepetitiveColumn(ref string[] arr)
    {
        int len = arr.Length;
        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < len; j++)
            {
                if (i == j)
                    continue;
                // 检测arr中是否有相同的内容
                if (arr[i].Trim().Equals(arr[j].Trim()))
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 校验CSV内容
    /// </summary>
    /// <returns>true表示校验通过，false表示校验不通过</returns>
    protected virtual bool VerifyCSV()
    {
        return true;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public virtual void Recycle()
    {
        mENTitles = null;
        mCnLine = null;
        mEnLine = null;
        mColumnNumber = 0;
        mRowNumber = 0;
        if (mContent != null)
        {
            foreach (Dictionary<string, string> item in mContent)
            {
                item.Clear();
            }
            mContent.Clear();
            mContent.TrimExcess();
            mContent = null;
        }
    }

    /// <summary>
    /// 获取指定行的记录
    /// </summary>
    /// <param name="uRow">行号</param>
    /// <returns></returns>
    public Dictionary<string, string> GetRow(int uRow)
    {
        return mContent[uRow];
    }

    /// <summary>
    /// 增加或修改指定行记录
    /// </summary>
    /// <param name="uRow"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public bool SetRow(int uRow, string content)
    {
        Dictionary<string, string> row = null;
        if (uRow < mRowNumber)
        {
            // 修改
            row = GetRow(uRow);
        }
        else
        {
            // 追加
            row = new Dictionary<string, string>(mColumnNumber * 2 - 1);
            mContent.Add(row);
        }
        // 赋值
        string[] colArr = content.Split(',');
        for (int i = 1, len = colArr.Length; i < len; i++)
        {
            row[GetColName(i)] = colArr[i];
        }
        return true;
    }

    /// <summary>
    /// 获取指定索引的字段名
    /// </summary>
    /// <param name="colNum">字段索引</param>
    /// <returns></returns>
    public string GetColName(int colNum)
    {
        return mENTitles[colNum];
    }

    /// <summary>
    /// 获取当前CSV文件的记录总数
    /// </summary>
    public int RowCount
    {
        get { return mRowNumber; }
    }

    /// <summary>
    /// 获取当前CSV文件的字段总数
    /// </summary>
    public int ColCount
    {
        get { return mColumnNumber; }
    }

    /// <summary>
    /// 获取指定行号、列号的原始内容
    /// </summary>
    /// <param name="uRow">行号</param>
    /// <param name="uCol">列号</param>
    /// <returns></returns>
    public string GetContent(int uRow, int uCol)
    {
        return mContent[uRow][GetColName(uCol)];
    }

    /// <summary>
    /// 设置指定行、列的原始值
    /// </summary>
    /// <param name="uRow">行号</param>
    /// <param name="uCol">列号</param>
    /// <param name="content">值</param>
    public void SetContent(int uRow, int uCol, string content)
    {
        mContent[uRow][GetColName(uCol)] = content;
    }

    /// <summary>
    /// 获取指定行号和字段名的原始内容
    /// </summary>
    /// <param name="uRow">行号</param>
    /// <param name="colName">字段名</param>
    /// <returns></returns>
    public string GetContent(int uRow, string colName)
    {
        return mContent[uRow][colName];
    }

    /// <summary>
    /// 设置指定行、列的原始值
    /// </summary>
    /// <param name="uRow">行号</param>
    /// <param name="colName">字段名</param>
    /// <param name="content">值</param>
    public void SetContent(int uRow, string colName, string content)
    {
        mContent[uRow][colName] = content;
    }

    /// <summary>
    /// CSV文件加载完毕
    /// </summary>
    protected virtual void OnCSVLoaded() { }

    // CSV标题集合(英文标题，程序内使用)
    private string[] mENTitles = null;
    // CSV记录
    private List<Dictionary<string, string>> mContent = null;
    // CSV记录的列总数、行总数据
    protected int mColumnNumber = 0, mRowNumber = 0;
    // 保存前3行，写入的时候很方便
    private string mEnLine, mCnLine;
}
