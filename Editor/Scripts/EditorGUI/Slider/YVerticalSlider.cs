#region Copyright
// **********************************************************************
// Copyright (C) 2023 #COMPANYNAME#
//
// Script Name :		YVerticalSlider.cs
// Author Name :		YunShu
// Create Time :		2023/03/01 14:37:46
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.GameEditor.UI
{
    public class YVerticalSlider : YSlider
    {
        public YVerticalSlider(float min, float max) : this(new Rect(), min, max)
        {

        }

        public YVerticalSlider(Rect rect, float min, float max) : this(rect, min, max, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb)
        {

        }

        public YVerticalSlider(float min, float max, GUIStyle style, GUIStyle thumb) : this(new Rect(), min, max, style, thumb)
        {

        }


        public YVerticalSlider(Rect rect, float min, float max, GUIStyle style, GUIStyle thumb) : base(rect, min, max, style, thumb)
        {

        }

        public override void OnGUI()
        {
            SliderValue = GUI.HorizontalSlider(m_rect, SliderValue, min, max, m_Style, ThumbStyle);
        }
    }
}