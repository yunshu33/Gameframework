using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Editor.YEditorGUI {

    public class YButton : YLabel
    {

       

        public YButton() : this(new Rect())
        {

        }

        public YButton(Rect rect) : this(rect, GUI.skin.label)
        {

        }

        public YButton(GUIStyle style) : this(new Rect(),style)
        {

        }
        public  YButton(string text) : this()
        {
            Text = text;
        }

        public YButton(Rect rect, GUIStyle style) : base(rect, style)
        {

        }


        public YButton(Rect rect, string text) : this(rect)
        {
            Text = text;
        }

        public YButton(Rect rect, string text, GUIStyle style) : this(rect, style)
        {
            Text = text;
        }




        protected bool isClick = false;
        /// <summary>
        /// 是否被点击
        /// </summary>
        public virtual bool IsClick { get => isClick; set => isClick = value; }

        public override void OnGUI()
        {
            IsClick = GUI.Button(m_rect, Text,m_Style);
        }
    }
}