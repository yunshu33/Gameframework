using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using LJVoyage.GameEditor.UI;

namespace LJVoyage.GameEditor.Table
{
    public class DataTableHeader : YEditorGUIBase
    {
        MultiColumnHeaderState multiColumnHeaderState;

        public MultiColumnHeader multiColumnHeader;

        public MultiColumnHeaderState.Column[] columns;

        public DataTableHeader(Rect rect, List<string> tableHeaders, GUIStyle style) : base(rect, style)
        {
            columns = new MultiColumnHeaderState.Column[tableHeaders.Count];

            for (int i = 0; i < tableHeaders.Count; i++)
            {
                var column = new MultiColumnHeaderState.Column()
                {
                    allowToggleVisibility = false, // At least one column must be there.
                    autoResize = true,
                    minWidth = 200,
                    canSort = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    headerContent = new GUIContent(tableHeaders[i]),
                    headerTextAlignment = TextAlignment.Center,
                };

                columns[i] = column;
            }

            multiColumnHeaderState = new MultiColumnHeaderState(columns: columns);

            multiColumnHeader = new MultiColumnHeader(state: multiColumnHeaderState);

            // When we chagne visibility of the column we resize columns to fit in the window.
            //当我们改变列的可见性时，我们调整列的大小以适应窗口。
            multiColumnHeader.visibleColumnsChanged += (header) => header.ResizeToFit();

            // Initial resizing of the content.
            //内容的初始大小调整。
            multiColumnHeader.ResizeToFit();


            MinSize = new Vector2(columns.Sum((column) => column.minWidth), EditorGUIUtility.singleLineHeight);
        }

        public DataTableHeader(Rect rect, List<string> tableHeaders) : this(rect, tableHeaders, GUIStyle.none)
        {
        }

        public DataTableHeader(List<string> tableHeaders) : this(new Rect(), tableHeaders)
        {
        }

        public override Vector2 size
        {
            get => base.size;

            set
            {
                height = value.y;
                width = value.x;
            }
        }

        public override float height
        {
            get => base.height;
            set
            {
                if (value > MinSize.y)
                {
                    base.height = value;
                }
                else
                {
                    base.height = MinSize.y;
                }
            }
        }

        public override float width
        {
            get => base.width;
            set
            {
                if (value > MinSize.x)
                {
                    base.width = value;
                }
                else
                {
                    base.width = MinSize.x;
                }
            }
        }

        public override void OnGUI()
        {
            multiColumnHeader.OnGUI(m_rect, 0.0f);
        }
    }
}