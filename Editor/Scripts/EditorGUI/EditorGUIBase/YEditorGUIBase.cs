using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.GameEditor.UI
{
    public abstract class YEditorGUIBase
    {


        public YEditorGUIBase(Rect rect, GUIStyle style)
        {
            m_Style = style;
            m_rect = rect;
        }

        public YEditorGUIBase(GUIStyle style) : this(new Rect(),style)
        {

        }

        protected GUIStyle style;

        public virtual GUIStyle m_Style { get => style; set => style = value; }

        /// <summary>
        /// 自身矩形
        /// </summary>
        protected Rect m_rect;


        public abstract void OnGUI();

        protected Vector2 minSize;

        protected Vector2 maxSize;



        public virtual float x
        {
            get
            {
                
                return m_rect.x;
            }
            set
            {
                m_rect.x = value;
            }
        }
        public virtual float y
        {
            get
            {
                return m_rect.y;
            }
            set
            {
                m_rect.y = value;
            }
        }
        public virtual float width
        {
            get
            {
                return m_rect.width;
            }
            set
            {
                m_rect.width = value;
                
            }
        }

        public virtual float height
        {
            get
            {
                return m_rect.height;
            }
            set
            {
                m_rect.height = value;
            }
        }

       public virtual Vector2 position { 
            get {
                return m_rect.position;
            } 
            set
            {
                m_rect.position = value;
                
            }
        }

        public virtual Vector2 size
        {
            get { 
                return m_rect.size;
            }
            set { 
                m_rect.size = value; 
            }
        }

        protected Vector2 MinSize { get => minSize; set => minSize = value; }
        protected Vector2 MaxSize { get => maxSize; set => maxSize = value; }
    }
}