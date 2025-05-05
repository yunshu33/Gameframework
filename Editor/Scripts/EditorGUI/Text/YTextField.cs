using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Editor.UI
{
    public class YTextField : YLabel
    {
        public YTextField() : this(new Rect())
        {

        }

        public YTextField(Rect rect) : this(rect, GUI.skin.textField)
        {

        }

        public YTextField(GUIStyle style) : this(new Rect(),style)
        {

        }
        public YTextField(string text) : this()
        {
            Text = text;
        }

        public YTextField(Rect rect, GUIStyle style) : base(rect, style)
        {

        }


        public YTextField(Rect rect, string text) : this(rect)
        {
            Text = text;
        }

        public YTextField(Rect rect, string text, GUIStyle style) : this(rect, style)
        {
            Text = text;
        }


      

        public override void OnGUI()
        {
            Text = GUI.TextField(m_rect, Text, m_Style);
        }
    }
}
