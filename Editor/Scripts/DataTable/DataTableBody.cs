

using System.Collections.Generic;
using LJVoyage.GameEditor.UI;
using UnityEditor;
using UnityEngine;

namespace LJVoyage.GameEditor.Table
{
    public class DataTableBody : YEditorGUIBase
    {

        DataTableHeader header;


        GUIStyle fieldGUIStyle;

        public DataTableBody(DataTableHeader header) : base(new Rect(), GUIStyle.none)
        {
            this.header = header;


            rowRect.height = EditorGUIUtility.singleLineHeight;



        }

        List<List<string>> data;

        public void SetData(List<List<string>> data)
        {
            this.data = data;


        }

        protected Vector2 scrollPosition = Vector2.zero;

        /// <summary>
        /// 视图矩形
        /// </summary>
        protected Rect viewRect = Rect.zero;

        public Vector2 ScrollPosition { get => scrollPosition; set => scrollPosition = value; }

        /// <summary>
        /// 内容矩形
        /// </summary>
        public Rect ViewRect { get => viewRect; set => viewRect = value; }


        private readonly Color _lighterColor = Color.white * 0.3f;

        private readonly Color _darkerColor = Color.white * 0.1f;


        /// <summary>
        /// 底纹 矩形
        /// </summary>
        Rect rowRect = new Rect();



        /// <summary>
        /// 设置底纹 矩形
        /// </summary>
        private void SetShadingRect()
        {
            ///底纹 矩形 的长度 与头部长度一致
            rowRect.width = header.width;

            rowRect.height = EditorGUIUtility.singleLineHeight;
            rowRect.y = 0;
        }


        /// <summary>
        /// 设置视图矩形
        /// </summary>
        private void SetViewRect()
        {
            viewRect.width = header.width;
            viewRect.height = data.Count * EditorGUIUtility.singleLineHeight;
        }


        private void SetGUIStyle()
        {
            if (fieldGUIStyle == null)
            {
                fieldGUIStyle = new GUIStyle(GUI.skin.label)
                {
                    padding = new RectOffset(left: 10, right: 10, top: 2, bottom: 2),

                    alignment = TextAnchor.MiddleCenter,
                };
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowCount"></param>
        private void DrawRow(int rowCount)
        {
            rowRect.y = EditorGUIUtility.singleLineHeight * rowCount;

            //绘制底纹
            if (rowCount % 2 == 0)
                EditorGUI.DrawRect(rowRect, _darkerColor);
            else
                EditorGUI.DrawRect(rowRect, _lighterColor);

            for (int j = 0; j < header.columns.Length && j < data[rowCount].Count; j++)
            {

                if (header.multiColumnHeader.IsColumnVisible(j))
                {

                    int visibleColumnIndex = header.multiColumnHeader.GetVisibleColumnIndex(j);

                    Rect columnRect = header.multiColumnHeader.GetColumnRect(visibleColumnIndex);

                    // This here basically is a row height, you can make it any value you like. Or you could calculate the max field height here that your object has and store it somewhere then use it here instead of `EditorGUIUtility.singleLineHeight`.
                    //这基本上是行高，你可以设置任何你喜欢的值。或者你可以计算你的对象拥有的最大字段高度，并将它存储在某个地方，然后在这里使用它而不是' EditorGUIUtility.singleLineHeight '。
                    // We move position of field on `y` by this height to get correct position.
                    //我们在“y”上移动这个高度来获得正确的位置。
                    columnRect.y = rowRect.y;


                    //获得单元格矩形
                    //var rect = header.multiColumnHeader.GetCellRect(visibleColumnIndex, columnRect);

                    ///绘制单元格
                    EditorGUI.LabelField(
                        position: header.multiColumnHeader.GetCellRect(visibleColumnIndex, columnRect),
                        label: new GUIContent(data[rowCount][j]),
                        style: fieldGUIStyle
                    );
                }

            }
        }

        /// <summary>
        /// 绘制单元格
        /// </summary>
        /// <param name="text"></param>
        void DrawTableCell(string text)
        {

        }

        public override void OnGUI()
        {
            SetGUIStyle();

            SetShadingRect();

            SetViewRect();




            ScrollPosition = GUI.BeginScrollView(m_rect, ScrollPosition, ViewRect);
            {
                for (int i = 0; i < data.Count; i++)
                {
                    DrawRow(i);
                }

            }

            GUI.EndScrollView();

        }
    }
}