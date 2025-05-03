#region Copyright
// **********************************************************************
// Copyright (C) 2023 #COMPANYNAME#
//
// Script Name :		YVerticalScrollbar.cs
// Author Name :		YunShu
// Create Time :		2023/03/01 15:08:29
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Editor.YEditorGUI
{
    public class YVerticalScrollbar : YScrollbar
    {
        public YVerticalScrollbar(float scrollbarSize, float min, float max) : this(new Rect(), scrollbarSize, min, max)
        {

        }

        public YVerticalScrollbar(Rect rect, float scrollbarSize, float min, float max) : this(rect, scrollbarSize, min, max, GUI.skin.verticalScrollbar)
        {

        }

        public YVerticalScrollbar(float scrollbarSize, float min, float max, GUIStyle style) : this(new Rect(), scrollbarSize, min, max, style)
        {

        }


        public YVerticalScrollbar(Rect rect, float scrollbarSize, float min, float max, GUIStyle style) : base(rect, scrollbarSize, min, max, style)
        {

        }

        public override void OnGUI()
        {
            sliderValue = GUI.HorizontalScrollbar(m_rect, sliderValue, scrollbarSize, min, max, m_Style);
        }
    }
}