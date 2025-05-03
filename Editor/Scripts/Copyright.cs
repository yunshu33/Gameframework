using System.IO;
using System.Linq;
using UnityEditor;

namespace YunFramework.Editor
{
    /// <summary>
    /// 配置脚本 备注
    /// </summary>
    public class Copyright : UnityEditor.AssetModificationProcessor
    {
       
        private const string DateFormat = "yyy/MM/dd HH:mm:ss";

        public static void OnWillCreateAsset(string path)
        {
            //string[] guids = AssetDatabase.FindAssets("t:" + typeof(GameWorldEditorConfig).Name);

            //var editorConfig = AssetDatabase.LoadAssetAtPath<GameWorldEditorConfig>(AssetDatabase.GUIDToAssetPath(guids.FirstOrDefault()));

            //path = path.Replace(".meta", "");

            //if (path.EndsWith(".cs"))
            //{
            //    string _fileText = File.ReadAllText(path);

            //    if (!_fileText.StartsWith("#region Copyright\r\n// **********************************************************************\r\n// Copyright (C) ") )
            //    {
                   
            //        _fileText = _fileText.Insert(0, "#region Copyright\r\n// **********************************************************************\r\n// Copyright (C) #COPYRIGHTYEAR# #COMPANYNAME#\r\n//\r\n// Script Name :\t\t#SCRIPTNAME#.cs\r\n// Author Name :\t\t#AuthorName#\r\n// Create Time :\t\t#CreateTime#\r\n// Description :\r\n// **********************************************************************\r\n#endregion\n\n");
            //    }

            //    _fileText = _fileText.Replace("#COPYRIGHTYEAR#", System.DateTime.Now.Year.ToString())
            //        .Replace("#AuthorName#", editorConfig.m_authorName)
            //        .Replace("#CreateTime#", System.DateTime.Now.ToString(DateFormat))
            //        .Replace("#NAMESPACE#", editorConfig.m_namespace)
            //        .Replace("#COMPANYNAME#", editorConfig.m_companyName);

            //    File.WriteAllText(path, _fileText);

            //    AssetDatabase.Refresh();
            //}
        }
    }
}
