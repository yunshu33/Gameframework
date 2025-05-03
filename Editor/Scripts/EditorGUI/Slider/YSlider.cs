#region Copyright
// **********************************************************************
// Copyright (C) 2023 #COMPANYNAME#
//
// Script Name :		YSlider.cs
// Author Name :		YunShu
// Create Time :		2023/03/01 13:56:55
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Editor.YEditorGUI
{
    public abstract class YSlider : YEditorGUIBase
    {
        public YSlider(float min,float max) : this(new Rect(),min,max)
        {
           
        }

        public YSlider(Rect rect, float min, float max) : this(rect, min, max, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb)
        {

        }

        public YSlider(float min, float max,GUIStyle style, GUIStyle thumb) : this(new Rect(), min, max,style,thumb)
        {

        }
       

        public YSlider(Rect rect, float min, float max ,GUIStyle style, GUIStyle thumb) : base(rect, style)
        {
            ThumbStyle = thumb;
        }

        protected GUIStyle thumbStyle;

        protected float min = 0.0f;

        protected float max = 1.0f;

        protected float sliderValue = 0.0f;

        public virtual float SliderValue { get => sliderValue; set => sliderValue = value; }
        public virtual float Min { get => min; set => min = value; }
        public virtual float Max { get => max; set => max = value; }
        protected GUIStyle ThumbStyle { get => thumbStyle; set => thumbStyle = value; }
    }
}