using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LJVoyage.GameEditor
{
    public class ExcelConfig : MonoBehaviour
    {
        /// <summary>
        /// 存放excel表文件夹的的路径，本例xecel表放在了"streamingAssets/Excels/"当中
        /// </summary>
        public static readonly string excelsFolderPath = Application.streamingAssetsPath + "/Excel/";

        /// <summary>
        /// 存放Excel转化CS文件的文件夹路径
        /// </summary>
        //public static readonly string assetPath = "Assets/StreamingAssets/Excel/DataAssets/"; 
        public static readonly string assetPath = "Assets/Resources/Excel/DataAssets/";

    }
}
