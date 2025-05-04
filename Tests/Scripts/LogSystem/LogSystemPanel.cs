#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		LogSystem.cs
// Author Name :		YunShu
// Create Time :		2022/06/27 12:09:01
// Description :
// **********************************************************************
#endregion


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Yun.Framework
{
    public class LogSystemPanel : MonoBehaviour
    {
        public TextMeshProUGUI type;
        public TextMeshProUGUI stackTrace;
        public TextMeshProUGUI condition;
    }
}