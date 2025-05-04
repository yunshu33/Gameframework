#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		CreateAsset.cs
// Author Name :		YunShu
// Create Time :		2022/07/01 16:09:02
// Description :
// **********************************************************************
#endregion


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yun.Framework
{
    [CreateAssetMenu(fileName = "CreateAssetTest1", menuName = "CreateAssetTest")]
    public class CreateAsset : ScriptableObject
    {
        [Header("测试")]
        [SerializeField]
        public List<TestCreateAssetClass> testCreateAssetClasses = new();
    }


    [System.Serializable]
    public class TestCreateAssetClass
    {
        public string name;

    }

}