using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWorldFramework.Editor.YEditorGUI
{
    public class YLabel : YEditorGUIBase
    {

       

        public YLabel() : this(new Rect())
        {

        }

        public YLabel(Rect rect): this(rect,GUI.skin.label)
        {

        }

        public YLabel(GUIStyle style) : this(new Rect(),style)
        {

        }
        public YLabel(string text) : this()
        {
            Text = text;
        }

        public YLabel(Rect rect, GUIStyle style): base(rect,style)
        {
           
        }
       
        
        public YLabel(Rect rect,string text) : this(rect)
        {
            Text = text;
        }

        public YLabel(Rect rect, string text, GUIStyle style) : this(rect, style)
        {
            Text = text;
        }

        protected string text;

        public virtual string Text { 
            get => text; 
            set => text = value; 
        }

        
        public override void OnGUI()
        {
            GUI.Label(m_rect, Text,m_Style);
        }
    }
}