#region Copyright
// **********************************************************************
// Copyright (C) 2023 #COMPANYNAME#
//
// Script Name :		YToggle.cs
// Author Name :		YunShu
// Create Time :		2023/03/01 13:35:26
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Editor.UI
{
    public class YToggle : YLabel
    {
        public YToggle() : this(new Rect())
        {

        }

        public YToggle(Rect rect) : this(rect, GUI.skin.toggle)
        {

        }

        public YToggle(GUIStyle style) : this(new Rect(), style)
        {

        }
        public YToggle(string text) : this()
        {
            Text = text;
        }

        public YToggle(Rect rect, GUIStyle style) : base(rect, style)
        {

        }


        public YToggle(Rect rect, string text) : this(rect)
        {
            Text = text;
        }

        public YToggle(Rect rect, string text, GUIStyle style) : this(rect, style)
        {
            Text = text;
        }

        protected bool isPitchOn = false;

        public virtual bool IsPitchOn { get => isPitchOn; set => isPitchOn = value; }

        public override void OnGUI()
        {
            IsPitchOn = GUI.Toggle(m_rect, IsPitchOn, Text, m_Style);
            
        }
    }
}