using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LJVoyage.Game.Editor
{
    public class Export :UnityEditor.Editor
    {
        /// <summary>
        /// 导出工具包
        /// </summary>
        [MenuItem("YunTools/Export/ExportTools")]
        private static void ExportTools()
        {
            string name = "YunTools-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".unitypackage";
            AssetDatabase.ExportPackage(new string[2] {  "Assets/YunTools", "Assets/plugins" }, name, ExportPackageOptions.Recurse);


            string pathName = Path.Combine(Application.dataPath, "..");
            Application.OpenURL("file://" + pathName);
        }



        /// <summary>
        /// 导出框架包
        /// </summary>
        [MenuItem("YunTools/Export/ExportFramework")]
        private static void ExportFarmework()
        {
            string name = "YunFramework-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".unitypackage";
            AssetDatabase.ExportPackage("Assets/YunFramework", name, ExportPackageOptions.Recurse);


            string pathName = Path.Combine(Application.dataPath, "..");
            Application.OpenURL("file://" + pathName);
        }

        /// <summary>
        /// 导出tools framework plugins 完整的Yun
        /// </summary>
        [MenuItem("YunTools/Export/ExportCompleteYun")]
        private static void ExportCompleteYun()
        {
            string name = "CompleteYun-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".unitypackage";
            
            
            AssetDatabase.ExportPackage(new string[5] { "Assets/YunFramework", "Assets/YunTools" , "Assets/plugins", "Assets/ScriptTemplates", "Assets/YunUtil" }, name, ExportPackageOptions.Recurse);


            string pathName = Path.Combine(Application.dataPath, "..");
            Application.OpenURL("file://" + pathName);
        }

    }
}
