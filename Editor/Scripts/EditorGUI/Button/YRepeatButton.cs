using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YunFramework.Editor.YEditorGUI
{

    public class YRepeatButton : YButton
    {
        public override void OnGUI()
        {
            IsClick = GUI.RepeatButton(m_rect, Text, m_Style);
        }
    }
}