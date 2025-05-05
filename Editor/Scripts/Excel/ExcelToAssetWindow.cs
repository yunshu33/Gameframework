
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Data;

namespace LJVoyage.GameEditor
{
    public class ExcelToAssetWindow : EditorWindow
    {
        
        /// <summary>
        /// 表数据
        /// </summary>
        List<List<List<string>>> excelList = new List<List<List<string>>>();

        /// <summary>
        /// 文件地址
        /// </summary>
        string excelPath;
        /// <summary>
        /// 表名数组
        /// </summary>
        string[] tableName;

        /// <summary>
        /// 文件是否存在
        /// </summary>
        bool isFile = false;

        /// <summary>
        /// 滚动条区域
        /// </summary>
        Vector2 scrll; public Object source;

        public string ExcelPath
        {
            get { return excelPath; }
            set
            {
                excelPath = value;
                IsFile(excelPath);
            }
        }

        [MenuItem("YunTools/Excel/ExcelToAssetWindow")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            ExcelToAssetWindow window = (ExcelToAssetWindow)GetWindow(typeof(ExcelToAssetWindow));
            window.Show();
            window.minSize = new Vector2(200, 300);

        }

        void OnGUI()
        {

          
            ToolMagazine.GetDragFilePath(this, AnalysisPath);


            source = EditorGUILayout.ObjectField(source, typeof(GameObject), true);
            Debug.Log(source.name);
            Debug.Log(source.GetInstanceID());
            Debug.Log(source.GetType());
            GUILayout.Label("拖入Excel文件", EditorStyles.boldLabel);
            ExcelPath = EditorGUILayout.TextField(ExcelPath);


            //按钮  读取数据  
            EditorGUILayout.BeginToggleGroup("文件状态(存在/不存在)", isFile);
            GUI.skin.button.wordWrap = true;
            // 这行不能少
            if (GUILayout.Button("读取", GUILayout.Width(0)))
            {
                excelList = new List<List<List<string>>>();
                if (ExcelPath != null)
                {
                    DataSet result = FileLoader.ReadExcel(ExcelPath);
                    //包含的表数
                    int tables = result.Tables.Count;

                    tableName = new string[tables];



                    for (int index = 0; index < tables; index++)
                    {
                        string tableName = result.Tables[index].TableName;
                        this.tableName[index] = tableName;
                        //Tables[0] 下标0表示excel文件中第一张表的数据
                        int columnNum = result.Tables[index].Columns.Count;
                        int rowNum = result.Tables[index].Rows.Count;

                        List<List<string>> vs = new List<List<string>>();
                        for (int j = 0; j < 2; j++)
                        {
                            List<string> list = new List<string>();
                            for (int i = 0; i < columnNum; i++)
                            {
                                list.Add(result.Tables[index].Rows[j][i].ToString());
                            }
                            vs.Add(list);
                        }
                        excelList.Add(vs);
                    }


                }
                else
                {
                    Debug.LogError("请输入文件路径 或者 拖入  .xlsx ");
                }
            }

            EditorGUILayout.EndToggleGroup();


            scrll = EditorGUILayout.BeginScrollView(scrll);

            EditorGUILayout.BeginToggleGroup("表格数据：", false);
            // 绘制表格
            if (excelList.Count != 0)
            {
                int i = 0;
                foreach (var item in excelList)
                {
                    EditorGUILayout.BeginVertical();

                    EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(200));

                    EditorGUILayout.LabelField("Excel表名");
                    EditorGUILayout.TextField(tableName[i++], GUILayout.Width(100), GUILayout.Height(20));

                    EditorGUILayout.EndHorizontal();
                    foreach (var list in item)
                    {
                        EditorGUILayout.BeginHorizontal();
                        foreach (var l in list)
                        {

                            EditorGUILayout.TextField(l);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndScrollView();
            
            
        }

        /// <summary>
        /// 解析 地址数组
        /// </summary>
        /// <param name="str"></param>
        private void AnalysisPath(string[] str)
        {
            foreach (var item in str)
            {
                string[] s = item.Split('.');
                if (s.Length>=2 && s[1] == "xlsx")
                {
                    ExcelPath = item;
                    break;
                }else 
                {
                    Debug.LogError(item +"非  .xlsx 文件");
                }
            }
        }
        private void IsFile(string path)
        {
            isFile = File.Exists(ExcelPath);
        }
        #region 窗体事件调用
        private void OnProjectChange()
        {
            //  Debug.Log("当场景改变时调用");
        }

        private void OnHierarchyChange()
        {
            //  Debug.Log("当选择对象属性改变时调用");
        }

        void OnGetFocus()
        {
            // Debug.Log("当窗口得到焦点时调用");
        }

        private void OnLostFocus()
        {
            // Debug.Log("当窗口失去焦点时调用");
        }

        private void OnSelectionChange()
        {
            //Debug.Log("当改变选择不同对象时调用");
        }

        private void OnInspectorUpdate()
        {
            //Update
            //Debug.Log("监视面板调用");
            //Debug.Log(myString);
        }

        private void OnDestroy()
        {
            //Debug.Log("当窗口关闭时调用");
        }

        private void OnFocus()
        {
            //Debug.Log("当窗口获取键盘焦点时调用");
        }
        #endregion

     

    }
}
