#region Copyright

// **********************************************************************
// Copyright (C) 2023 HeJing
//
// Script Name :		ToggleExpand.cs
// Author Name :		YunShu
// Create Time :		2023/09/22 10:34:27
// Description :
// **********************************************************************

#endregion


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LJVoyage.Game.UI
{
    public static class ToggleExpand
    {
        /// <summary>
        /// 设置 toggle 状态 并触发 on Value Change
        /// </summary>
        /// <param name="_toggle"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsOn(this Toggle _toggle, bool value)
        {
            if (_toggle.isOn == value)
            {
                _toggle.onValueChanged.Invoke(value);
            }
            else
            {
                _toggle.isOn = value;
            }
            
            return value;
        }
    }
}