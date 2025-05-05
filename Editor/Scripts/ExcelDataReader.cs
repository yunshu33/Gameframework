#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Excel;
using System.Reflection;
using System;
using System.Data;

namespace LJVoyage.Game.Editor{

//Excel中间数据
public class ExcelMediumData
{
    //Sheet名字
    public string excelName;
    //Dictionary<字段名称, 字段类型>，记录类的所有字段及其类型
    public Dictionary<string, string> propertyNameTypeDic;
    //List<一行数据>，List<Dictionary<字段名称, 一行的每个单元格字段值>>
    //记录类的所有字段值，按行记录
    public List<Dictionary<string, string>> allItemValueRowList;
}

public static class ExcelDataReader
{
    //Excel第2行对应字段名称
    const int excelNameRow = 2;
    //Excel第4行对应字段类型
    const int excelTypeRow = 4;
    //Excel第5行及以后对应字段值
    const int excelDataRow = 5;

    //Excel读取路径
    public static string excelFilePath = Application.dataPath + "/Excel";
    //public static string excelFilePath = Application.dataPath.Replace("Assets", "Excel");

    //自动生成C#类文件路径
    static string excelCodePath = Application.dataPath + "/Script/Excel/AutoCreateCSCode";
    //自动生成Asset文件路径
    static string excelAssetPath = "Assets/Resources/ExcelAsset";

    #region --- Read Excel ---

    //创建Excel对应的C#类
    public static void ReadAllExcelToCode()
    {
        //读取所有Excel文件
        //指定目录中与指定的搜索模式和选项匹配的文件的完整名称（包含路径）的数组；如果未找到任何文件，则为空数组。
        string[] excelFileFullPaths = Directory.GetFiles(excelFilePath, "*.xlsx");

        if (excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            Debug.Log("Excel file count == 0");
            return;
        }
        //遍历所有Excel，创建C#类
        for (int i = 0; i < excelFileFullPaths.Length; i++)
        {
            ReadOneExcelToCode(excelFileFullPaths[i]);
        }
    }

    //创建Excel对应的C#类
    public static void ReadOneExcelToCode(string excelFileFullPath)
    {
        //解析Excel获取中间数据
        List<ExcelMediumData> excelMediumDatas = CreateClassCodeByExcelPath(excelFileFullPath);

        foreach (ExcelMediumData excelMediumData in excelMediumDatas)
        {
            //根据数据生成C#脚本
            string classCodeStr = ExcelCodeCreater.CreateCodeStrByExcelData(excelMediumData);
            if (!string.IsNullOrEmpty(classCodeStr))
            {
                //写文件，生成CSharp.cs
                if (WriteCodeStrToSave(excelCodePath, excelMediumData.excelName + "ExcelData", classCodeStr))
                {
                    Debug.Log("<color=green>Auto Create Excel Scripts Success : </color>" + excelMediumData.excelName);
                }
            }
            else
            {
                //生成失败
                Debug.LogError("Auto Create Excel Scripts Fail : " + (excelMediumData == null ? "" : excelMediumData.excelName));
            }
        }
    }

    #endregion

    #region --- Create Asset ---

    //创建Excel对应的Asset数据文件
    public static void CreateAllExcelAsset()
    {
        //读取所有Excel文件
        //指定目录中与指定的搜索模式和选项匹配的文件的完整名称（包含路径）的数组；如果未找到任何文件，则为空数组。
        string[] excelFileFullPaths = Directory.GetFiles(excelFilePath, "*.xlsx");
        if (excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            Debug.Log("Excel file count == 0");
            return;
        }
        //遍历所有Excel，创建Asset
        for (int i = 0; i < excelFileFullPaths.Length; i++)
        {
            CreateOneExcelAsset(excelFileFullPaths[i]);
        }
    }

    //创建Excel对应的Asset数据文件
    public static void CreateOneExcelAsset(string excelFileFullPath)
    {
        //解析Excel获取中间数据
        List<ExcelMediumData> excelMediumDatas = CreateClassCodeByExcelPath(excelFileFullPath);
        foreach (ExcelMediumData excelMediumData in excelMediumDatas)
        {
            //获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();
            //创建类的实例，返回为 object 类型，需要强制类型转换，assembly.CreateInstance("类的完全限定名（即包括命名空间）");
            object class0bj = assembly.CreateInstance(excelMediumData.excelName + "Assignment", true);

            //必须遍历所有程序集来获得类型。当前在Assembly-CSharp-Editor中，目标类型在Assembly-CSharp中，不同程序将无法获取类型
            Type type = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                //查找目标类型
                Type tempType = asm.GetType(excelMediumData.excelName + "AssetAssignment");
                if (tempType != null)
                {
                    type = tempType;
                    break;
                }
            }
            if (type != null)
            {
                //反射获取方法
                MethodInfo methodInfo = type.GetMethod("CreateAsset");
                if (methodInfo != null)
                {
                    methodInfo.Invoke(null, new object[] { excelMediumData.allItemValueRowList, excelAssetPath });
                    //创建Asset文件成功
                    Debug.Log("<color=green>Auto Create Excel Asset Success : </color>" + excelMediumData.excelName);
                }
            }
            else
            {
                //创建Asset文件失败
                Debug.LogError("Auto Create Excel Asset Fail : " + (excelMediumData == null ? "" : excelMediumData.excelName));
            }
           
        }

    }

    #endregion

    #region --- private ---

    //解析Excel，创建中间数据
    private static List<ExcelMediumData> CreateClassCodeByExcelPath(string excelFileFullPath)
    {
        if (string.IsNullOrEmpty(excelFileFullPath))
            return null;

        excelFileFullPath = excelFileFullPath.Replace("\\", "/");

        FileStream stream = File.Open(excelFileFullPath, FileMode.Open, FileAccess.Read);
        if (stream == null)
            return null;
        //解析Excel
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //无效Excel
        if (excelReader == null || !excelReader.IsValid)
        {
            Debug.Log("Invalid excel ： " + excelFileFullPath);
            return null;
        }
        //获取所有工作表
        var result = excelReader.AsDataSet();
        List<ExcelMediumData> excelMediumDatas = new List<ExcelMediumData>();

        for (int index = 0; index < result.Tables.Count; index++)
        {
            //<数据名称,数据类型>
            Dictionary<string, string> propertyNameTypes = null;
            //List<KeyValuePair<数据名称, 单元格数据值>[]>，所有数据值，按行记录
            List<Dictionary<string, string>> allItemValueRowList = null;
            string sheetName = result.Tables[index].TableName;
        
            DataRowCollection dataRows = result.Tables[index].Rows;
            //当前遍历行，从1开始
            int curRowIndex;
            //开始读取，按行遍历
            List<object[]> datas = new List<object[]>();
            for (curRowIndex = 1; curRowIndex < dataRows.Count; curRowIndex++)
            {
                bool useful = true;
                object[] objects = dataRows[curRowIndex].ItemArray;
                foreach (var obj in objects)
                {
                    //空行/行中有空格是无效数据
                    if(obj.ToString() == "")
                    {
                        useful = false;
                        break;
                    }
                }
                if(useful)
                {
                    datas.Add(objects);
                }
            }
            //移除说明字段
            datas.RemoveAt(1);
            for(int r=0;r<datas.Count-1;r++)
            {
                if(datas[r].Length!=datas[r+1].Length)
                {
                    Debug.Log("配置表对应字段数量不匹配!");
                    return null;
                }
            }
            //TODO:还需要检测字段名是否重复
            
            ExcelMediumData excelMediumData = new ExcelMediumData();
            propertyNameTypes = new Dictionary<string, string>();
            allItemValueRowList = new List<Dictionary<string, string>>();
            excelMediumData.excelName = sheetName;
            for(int u=0;u<datas[0].Length;u++)
            {
                propertyNameTypes[datas[0][u].ToString()] = datas[1][u].ToString();
            }
            excelMediumData.propertyNameTypeDic = propertyNameTypes;
            for(int y=2;y<datas.Count;y++)
            {
                Dictionary<string, string> temp = new Dictionary<string, string>();
                for(int g =0;g<datas[y].Length;g++)
                {
                    temp[datas[0][g].ToString()] = datas[y][g].ToString();
                }
                allItemValueRowList.Add(temp);
            }
            excelMediumData.allItemValueRowList = allItemValueRowList;
            excelMediumDatas.Add(excelMediumData);
        }
        return excelMediumDatas;
    }

    //写文件
    private static bool WriteCodeStrToSave(string writeFilePath, string codeFileName, string classCodeStr)
    {
        if (string.IsNullOrEmpty(codeFileName) || string.IsNullOrEmpty(classCodeStr))
            return false;
        //检查导出路径
        if (!Directory.Exists(writeFilePath))
            Directory.CreateDirectory(writeFilePath);
        //写文件，生成CS类文件
        StreamWriter sw = new StreamWriter(writeFilePath + "/" + codeFileName + ".cs");
        sw.WriteLine(classCodeStr);
        sw.Close();
        //
        UnityEditor.AssetDatabase.Refresh();
        return true;
    }

    #endregion

}
}
#endif