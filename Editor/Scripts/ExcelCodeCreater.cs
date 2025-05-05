#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;

namespace LJVoyage.GameEditor{

public class ExcelCodeCreater
{

    #region --- Create Code ---

    //创建代码，生成数据C#类
    public static string CreateCodeStrByExcelData(ExcelMediumData excelMediumData)
    {
        if (excelMediumData == null)
            return null;
        //Excel名字
        string excelName = excelMediumData.excelName;
        if (string.IsNullOrEmpty(excelName))
            return null;
        //Dictionary<字段名称, 字段类型>
        Dictionary<string, string> propertyNameTypeDic = excelMediumData.propertyNameTypeDic;
        if (propertyNameTypeDic == null || propertyNameTypeDic.Count == 0)
            return null;
        //List<一行数据>，List<Dictionary<字段名称, 一行的每个单元格字段值>>
        List<Dictionary<string, string>> allItemValueRowList = excelMediumData.allItemValueRowList;
        if (allItemValueRowList == null || allItemValueRowList.Count == 0)
            return null;
        //行数据类名
        string itemClassName = excelName + "ExcelItem";
        //整体数据类名
        string dataClassName = excelName + "ExcelData";

        //生成类
        StringBuilder classSource = new StringBuilder();
        classSource.Append("/*自动创建的代码文件 请勿删除*/\n");
        classSource.Append("\n");
        //添加引用
        classSource.Append("using UnityEngine;\n");
        classSource.Append("using System.Collections.Generic;\n");
        classSource.Append("using System;\n");
        classSource.Append("using System.IO;\n");
        classSource.Append("\n");
        //生成行数据类，记录每行数据
        classSource.Append(CreateExcelRowItemClass(itemClassName, propertyNameTypeDic));
        classSource.Append("\n");
        //生成整体数据类，记录整个Excel的所有行数据
        classSource.Append(CreateExcelDataClass(dataClassName, itemClassName));
        classSource.Append("\n");
        //生成Asset操作类，用于自动创建Excel对应的Asset文件并赋值
        classSource.Append(CreateExcelAssetClass(excelMediumData));
        classSource.Append("\n");
        return classSource.ToString();
    }

    //----------

    //生成行数据类
    private static StringBuilder CreateExcelRowItemClass(string itemClassName, Dictionary<string, string> propertyNameTypeDic)
    {
        //生成Excel行数据类
        StringBuilder classSource = new StringBuilder();
        classSource.Append("[Serializable]\n");
        classSource.Append("public class " + itemClassName + " : ExcelItemBase\n");
        classSource.Append("{\n");
        //声明所有字段
        foreach (var item in propertyNameTypeDic)
        {
            classSource.Append(CreateCodeProperty(item.Key, item.Value));
        }
        classSource.Append("}\n");
        return classSource;
    }

    //声明行数据类字段
    private static string CreateCodeProperty(string name, string type)
    {
        if (string.IsNullOrEmpty(name))
            return null;
        if (name == "id")
            return null;
        //list的泛型T
        string T="";
        //属性
        string propertyStr = "";
        //判断字段类型 
        if (type == "int" || type == "Int" || type == "INT")
            type = "int";
        else if (type == "float" || type == "Float" || type == "FLOAT")
            type = "float";
        else if (type == "bool" || type == "Bool" || type == "BOOL")
            type = "bool";
        else if (type.StartsWith("enum") || type.StartsWith("Enum") || type.StartsWith("ENUM"))
            type = type.Split('|').LastOrDefault();
        //如果是集合的话单独处理
        else if (type.StartsWith("list") || type.StartsWith("LIST") || type.StartsWith("List"))
        {
            T = type.Split('|').LastOrDefault();
            if (T == "int" || T == "Int" || T == "INT")
                T = "int";
            else if (T == "float" || T == "Float" || T == "FLOAT")
                T = "float";
            else if (T == "bool" || T == "Bool" || T == "BOOL")
                T = "bool";

            type = "List<" + T + ">";
            propertyStr = "\tpublic " + type + " " + name +" = " + "new "+type+"()"+";\n";
            return propertyStr;
        }     
        else
            type = "string";
        //声明
        propertyStr = "\tpublic " + type + " " + name + ";\n";
        return propertyStr;
    }

    //----------

    //生成数据类
    private static StringBuilder CreateExcelDataClass(string dataClassName, string itemClassName)
    {
        StringBuilder classSource = new StringBuilder();
        classSource.Append("[CreateAssetMenu(fileName = \"" + dataClassName + "\", menuName = \"Excel To ScriptableObject/Create " + dataClassName + "\", order = 1)]\n");
        classSource.Append("public class " + dataClassName + " : ExcelDataBase<" + itemClassName + ">\n");
        classSource.Append("{\n");
        //声明字段，行数据类数组
        //classSource.Append("\tpublic " + itemClassName + "[] items;\n");
        classSource.Append("}\n");
        return classSource;
    }

    //----------

    //生成Asset操作类
    private static StringBuilder CreateExcelAssetClass(ExcelMediumData excelMediumData)
    {
        if (excelMediumData == null)
            return null;

        string excelName = excelMediumData.excelName;
        if (string.IsNullOrEmpty(excelName))
            return null;

        Dictionary<string, string> propertyNameTypeDic = excelMediumData.propertyNameTypeDic;
        if (propertyNameTypeDic == null || propertyNameTypeDic.Count == 0)
            return null;

        List<Dictionary<string, string>> allItemValueRowList = excelMediumData.allItemValueRowList;
        if (allItemValueRowList == null || allItemValueRowList.Count == 0)
            return null;

        string itemClassName = excelName + "ExcelItem";
        string dataClassName = excelName + "ExcelData";

        StringBuilder classSource = new StringBuilder();
        classSource.Append("#if UNITY_EDITOR\n");
        //类名
        classSource.Append("public class " + excelName + "AssetAssignment\n");
        classSource.Append("{\n");
        //方法名
        classSource.Append("\tpublic static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)\n");
        //方法体，若有需要可加入try/catch
        classSource.Append("\t{\n");
        classSource.Append("\t\tif (allItemValueRowList == null || allItemValueRowList.Count == 0)\n");
        classSource.Append("\t\t\treturn false;\n");
        classSource.Append("\t\tint rowCount = allItemValueRowList.Count;\n");
        classSource.Append("\t\t" + itemClassName + "[] items = new " + itemClassName + "[rowCount];\n");
        classSource.Append("\t\tfor (int i = 0; i < items.Length; i++)\n");
        classSource.Append("\t\t{\n");
        classSource.Append("\t\t\titems[i] = new " + itemClassName + "();\n");
        foreach (var item in propertyNameTypeDic)
        {
            //如果是集合就去遍历赋值 非集合直接赋值
            if (item.Value.Contains("list")|| item.Value.Contains("List") || item.Value.Contains("LIST"))
            {
                //记住一定要用英文逗号
                classSource.Append("\t\t\tstring[] temps = allItemValueRowList[i][\"" + item.Key + "\"]" + ".Split(',');" +  "\n");
                classSource.Append("\t\t\tforeach(var obj in temps)\n");
                classSource.Append("\t\t\t{\n");
                if (item.Value.Contains("int") || item.Value.Contains("INT") || item.Value.Contains("Int"))
                    classSource.Append("\t\t\t\titems[i]." + item.Key + ".Add(int.Parse(obj));\n");
                else if(item.Value.Contains("float") || item.Value.Contains("Float") || item.Value.Contains("FLOAT"))
                    classSource.Append("\t\t\t\titems[i]." + item.Key + ".Add(float.Parse(obj);\n");
                else if(item.Value.Contains("bool") || item.Value.Contains("Bool") || item.Value.Contains("BOOL"))
                    classSource.Append("\t\t\t\titems[i]." + item.Key + ".Add(bool.Parse(obj));\n");
                classSource.Append("\t\t\t}\n");
            }
            else
            {
                classSource.Append("\t\t\titems[i]." + item.Key + " = ");
                classSource.Append(AssignmentCodeProperty("allItemValueRowList[i][\"" + item.Key + "\"]", propertyNameTypeDic[item.Key]));
                classSource.Append(";\n");
            }  
        }
        classSource.Append("\t\t}\n");
        classSource.Append("\t\t" + dataClassName + " excelDataAsset = ScriptableObject.CreateInstance<" + dataClassName + ">();\n");
        classSource.Append("\t\texcelDataAsset.items = items;\n");
        classSource.Append("\t\tif (!Directory.Exists(excelAssetPath))\n");
        classSource.Append("\t\t\tDirectory.CreateDirectory(excelAssetPath);\n");
        classSource.Append("\t\tstring pullPath = excelAssetPath + \"/\" + typeof(" + dataClassName + ").Name + \".asset\";\n");
        classSource.Append("\t\tUnityEditor.AssetDatabase.DeleteAsset(pullPath);\n");
        classSource.Append("\t\tUnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);\n");
        classSource.Append("\t\tUnityEditor.AssetDatabase.Refresh();\n");
        classSource.Append("\t\treturn true;\n");
        classSource.Append("\t}\n");
        //
        classSource.Append("}\n");
        classSource.Append("#endif\n");
        return classSource;
    }

    //声明Asset操作类字段
    private static string AssignmentCodeProperty(string stringValue, string type)
    {
        //判断类型
        if (type == "int" || type == "Int" || type == "INT")
        {
            return "Convert.ToInt32(" + stringValue + ")";
        }
        else if (type == "float" || type == "Float" || type == "FLOAT")
        {
            return "Convert.ToSingle(" + stringValue + ")";
        }
        else if (type == "bool" || type == "Bool" || type == "BOOL")
        {
            return "Convert.ToBoolean(" + stringValue + ")";
        }
        else if (type.StartsWith("enum") || type.StartsWith("Enum") || type.StartsWith("ENUM"))
        {
            return "(" + type.Split('|').LastOrDefault() + ")(Convert.ToInt32(" + stringValue + "))";
        }
        else
            return stringValue;
    }

    #endregion

}
#endif
}