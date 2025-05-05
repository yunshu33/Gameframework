#region Copyright
// **********************************************************************
// Copyright (C) 2023 #COMPANYNAME#
//
// Script Name :		YToolbar.cs
// Author Name :		YunShu
// Create Time :		2023/03/01 13:46:30
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Editor.UI
{
    public class YToolbar : YEditorGUIBase
    {
        public YToolbar() : this(new Rect())
        {

        }

        public YToolbar(Rect rect) : this(rect, GUI.skin.button)
        {

        }

        public YToolbar(GUIStyle style) : this(new Rect(), style)
        {

        }
       

        public YToolbar(Rect rect, GUIStyle style) : base(rect, style)
        {

        }

        public YToolbar(Rect rect, string[] toolbarNames, GUIStyle style) : this(rect, style)
        {
            ToolbarNames = toolbarNames;
        }


        protected string[] toolbarNames;

        public string[] ToolbarNames { get => toolbarNames; set => toolbarNames = value; }

        /// <summary>
        /// 选中的按钮下标
        /// </summary>
        public int Selected { get => selected; set => selected = value; }

        protected int selected = 0;

        public override void OnGUI()
        {
            Selected = GUI.Toolbar(m_rect, Selected,ToolbarNames,m_Style);
        }
    }
}