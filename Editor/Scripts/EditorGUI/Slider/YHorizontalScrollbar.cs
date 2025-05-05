#region Copyright
// **********************************************************************
// Copyright (C) 2023 #COMPANYNAME#
//
// Script Name :		YHorizontalScrollbar.cs
// Author Name :		YunShu
// Create Time :		2023/03/01 14:43:23
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Editor.UI
{
    public class YHorizontalScrollbar : YScrollbar
    {
        public YHorizontalScrollbar(float scrollbarSize, float min, float max) : this(new Rect(), scrollbarSize, min, max)
        {

        }

        public YHorizontalScrollbar(Rect rect, float scrollbarSize, float min, float max) : this(rect, scrollbarSize, min, max, GUI.skin.horizontalScrollbar)
        {

        }

        public YHorizontalScrollbar(float scrollbarSize, float min, float max, GUIStyle style) : this(new Rect(), scrollbarSize, min, max, style)
        {

        }


        public YHorizontalScrollbar(Rect rect, float scrollbarSize, float min, float max, GUIStyle style) : base(rect, scrollbarSize, min, max, style)
        {

        }

        public override void OnGUI()
        {
            sliderValue = GUI.HorizontalScrollbar(m_rect, sliderValue,scrollbarSize,min,max,m_Style);
        }
    }
}