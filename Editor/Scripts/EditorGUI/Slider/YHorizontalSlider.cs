#region Copyright
// **********************************************************************
// Copyright (C) 2023 #COMPANYNAME#
//
// Script Name :		YHorizontalSlider.cs
// Author Name :		YunShu
// Create Time :		2023/03/01 13:55:29
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.GameEditor.UI
{
    public class YHorizontalSlider : YSlider
    {
        public YHorizontalSlider(float min, float max) : this(new Rect(), min, max)
        {

        }

        public YHorizontalSlider(Rect rect, float min, float max) : this(rect, min, max, GUI.skin.horizontalSlider,GUI.skin.horizontalSliderThumb)
        {

        }

        public YHorizontalSlider(float min, float max, GUIStyle style, GUIStyle thumb) : this(new Rect(), min, max, style, thumb)
        {

        }


        public YHorizontalSlider(Rect rect, float min, float max, GUIStyle style, GUIStyle thumb) : base(rect, min, max, style, thumb)
        {

        }
        public override void OnGUI()
        {
            SliderValue = GUI.HorizontalSlider(m_rect, SliderValue,min,max, m_Style, ThumbStyle);
        }
    }
}