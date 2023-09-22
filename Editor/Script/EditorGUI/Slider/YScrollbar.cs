#region Copyright
// **********************************************************************
// Copyright (C) 2023 #COMPANYNAME#
//
// Script Name :		YScrollbar.cs
// Author Name :		YunShu
// Create Time :		2023/03/01 14:47:41
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWorldFramework.Editor.YEditorGUI
{
    public abstract class YScrollbar : YEditorGUIBase
    {
        public YScrollbar(float scrollbarSize, float min, float max) : this(new Rect(), scrollbarSize, min, max)
        {

        }

        public YScrollbar(Rect rect, float scrollbarSize, float min, float max) : this(rect, scrollbarSize, min, max, GUI.skin.horizontalSlider)
        {

        }

        public YScrollbar(float scrollbarSize, float min, float max, GUIStyle style) : this(new Rect(), scrollbarSize, min, max, style)
        {

        }


        public YScrollbar(Rect rect, float scrollbarSize, float min, float max, GUIStyle style) : base(rect, style)
        {
            ScrollbarSize = scrollbarSize;
            Min = min;
            Max = max;
        }


        protected float scrollbarSize = 1.0f;

        protected float min = 0.0f;

        protected float max = 1.0f;

        protected float sliderValue = 0.0f;

        public virtual float SliderValue { get => sliderValue; set => sliderValue = value; }
        public virtual float Min { get => min; set => min = value; }
        public virtual float Max { get => max; set => max = value; }
        public virtual float ScrollbarSize { get => scrollbarSize; set => scrollbarSize = value; }
    }
}