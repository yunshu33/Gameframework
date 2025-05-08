using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace LJVoyage.Game.Editor.Excel
{
    public class ExcelBuild : UnityEditor.Editor
    {

        [MenuItem("YunTools/Excel/ExcelToAsset")]
        public static void CreateItemAsset()
        {
            AnimalNodeManager manager = ScriptableObject.CreateInstance<AnimalNodeManager>();
            //赋值
            manager.AnimalList = ExcelTool.CreateItemArrayWithExcel<AnimalNodeList>(ExcelConfig.excelsFolderPath + "AnimalInformation.xlsx", 3).ToArray();

            //确保文件夹存在
            if (!Directory.Exists(ExcelConfig.assetPath))
            {
                Directory.CreateDirectory(ExcelConfig.assetPath);
            }
            //asset文件的路径 要以"Assets/..."开始，否则CreateAsset会报错
            string assetPath = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "AnimalInformation");
            //生成一个Asset文件
            AssetDatabase.CreateAsset(manager, assetPath);
            //保存
            AssetDatabase.SaveAssets();
            //刷新  2017未知bug  会不刷新  需要重启编辑器
            AssetDatabase.Refresh();

        }
    }
}

