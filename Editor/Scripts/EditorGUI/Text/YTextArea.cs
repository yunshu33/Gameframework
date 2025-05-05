#region Copyright
// **********************************************************************
// Copyright (C) 2023 #COMPANYNAME#
//
// Script Name :		YTextArea.cs
// Author Name :		YunShu
// Create Time :		2023/03/01 13:28:15
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Editor.UI
{
    public class YTextArea : YTextField
    {
        public YTextArea() : this(new Rect())
        {

        }

        public YTextArea(Rect rect) : this(rect, GUI.skin.textArea)
        {

        }

        public YTextArea(GUIStyle style) : this(new Rect(),style)
        {

        }
        public YTextArea(string text) : this()
        {
            Text = text;
        }

        public YTextArea(Rect rect, GUIStyle style) : base(rect, style)
        {

        }


        public YTextArea(Rect rect, string text) : this(rect)
        {
            Text = text;
        }

        public YTextArea(Rect rect, string text, GUIStyle style) : this(rect, style)
        {
            Text = text;
        }


        public override void OnGUI()
        {
            Text = GUI.TextArea(m_rect, Text, m_Style);
        }
    }
}