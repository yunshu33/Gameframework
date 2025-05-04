using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Yun.Tools
{
    /// <summary>
    /// 配置脚本 备注
    /// </summary>
    public class Copyright : UnityEditor.AssetModificationProcessor
    {
        private const string CompanyName = "HeJing";
        private const string AuthorName = "YunShu";
        private const string DateFormat = "yyy/MM/dd HH:mm:ss";

        
        
        public static void OnWillCreateAsset(string path)
        {
            // path = path.Replace(".meta", "");
            //
            //
            //
            // if (path.EndsWith(".cs"))
            // {
            //      path = Path.GetFullPath(path);
            //   
            //      Debug.Log(path);
            //      
            //     string _fileText = File.ReadAllText(path);
            //     
            //     _fileText = _fileText.Replace("#COMPANYNAME#", CompanyName).Replace("#COPYRIGHTYEAR#", System.DateTime.Now.Year.ToString())
            //         .Replace("#AuthorName#", AuthorName).Replace("#CreateTime#", System.DateTime.Now.ToString(DateFormat));
            //     File.WriteAllText(path, _fileText);
            //     AssetDatabase.Refresh();
            // }
        }
    }
}
