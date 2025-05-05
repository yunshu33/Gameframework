using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System;

namespace LJVoyage.GameEditor.Table
{
    public class DataTable
    {
        DataTableHeader header;
        DataTableBody body;

        EditorWindow window;

        public DataTable(EditorWindow window, List<string> tableHeaders)
        {

            this.window = window;

            header = new DataTableHeader(tableHeaders);

            header.size = new Vector2(window.position.width,EditorGUIUtility.singleLineHeight);

            body = new DataTableBody(header);

            body.y = header.y + header.height;

            body.size = new Vector2(window.position.width, window.position.height - header.height);

        }


        /// <summary>
        /// 绘制头部
        /// </summary>
        private void DrawerHeader()
        {

            // 头部宽度 与窗口宽度一致  头部有最小宽度
            header.width = window.position.width;

            ///跟随 滚动条偏移
            header.x = - body.ScrollPosition.x;

            header.OnGUI();

        }


        public void OnGUI(List<List<string>> data)
        {

            DrawerHeader();

            DrawerData(data);

        }

        void DrawerData(List<List<string>> data)
        {
            body.SetData(data);

            body.width = window.position.width;

            //高度为窗户 高度 减去 头部高度
            //可视高度为窗户 高度 减去 头部高度
            body.height = window.position.height - header.height;

            body.OnGUI();

        }

    }
    


    public class DataTableEditor : EditorWindow
    {
        static DataTableEditor window;

        DataTable dataTable;

        [MenuItem("GameWorld/Test/DataTable")]
        static DataTableEditor Open()
        {

            window = GetWindow<DataTableEditor>();

            window.Show();

            window.dataTable = new DataTable(window, new List<string> { "第一列", "第二列", "第3列", "第4列" });

            return window;
        }
        private void OnGUI()
        {

            if (window == null)
            {
                window = Open();
            }

            dataTable.OnGUI(new List<List<string>> {
                new List<string> {"1","2"},
                 new List<string> {"2","2"},
                  new List<string> {"3","2"},
                new List<string> {"4","2"},
                 new List<string> {"1","2"},
                  new List<string> {"1","2"},
                new List<string> {"1","2"},
                 new List<string> {"1","2"},
                  new List<string> {"1","2"},
                new List<string> {"1","2"},
                 new List<string> {"1","2"},
                  new List<string> {"1","2"},
                new List<string> {"1","2"},
                 new List<string> {"1","2"},
                  new List<string> {"1","2"},
                new List<string> {"1","2"},
                 new List<string> {"1","2"},
                  new List<string> {"1","2"},
                new List<string> {"1","2"},
                 new List<string> {"1","2"},
                  new List<string> {"1","2"},
            });
        }

        void OnInspectorUpdate()
        {
            //开启窗口的重绘，不然窗口信息不会刷新
            Repaint();
        }
    }
}