#region Copyright
// **********************************************************************
// Copyright (C) 2023 #COMPANYNAME#
//
// Script Name :		YSelectionGrid.cs
// Author Name :		YunShu
// Create Time :		2023/03/01 13:52:02
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Editor.UI
{
    public class YSelectionGrid : YToolbar
    {
        public YSelectionGrid() : this(new Rect())
        {

        }

        public YSelectionGrid(Rect rect) : this(rect, GUI.skin.button)
        {

        }

        public YSelectionGrid(GUIStyle style) : this(new Rect(),style)
        {

        }


        public YSelectionGrid(Rect rect, GUIStyle style) : base(rect, style)
        {

        }

        public YSelectionGrid(Rect rect, string[] toolbarNames, GUIStyle style) : this(rect, style)
        {
            ToolbarNames = toolbarNames;
        }
        public override void OnGUI()
        {
            Selected = GUI.SelectionGrid(m_rect, Selected, ToolbarNames, 2,m_Style);
        }
    }
}