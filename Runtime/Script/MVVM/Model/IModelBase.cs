#region Copyright
// **********************************************************************
// Copyright (C) 2023
//
// Script Name :		IModelBase.cs
// Author Name :		云舒
// Create Time :		2023/03/27 14:43:13
// Description :
// **********************************************************************
#endregion


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameWorldFramework.RunTime.MVVM
{
    public interface IModelBase<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        Binder<T> Binder { get; }

        /// <summary>
        /// 数据模型
        /// </summary>
        T DataModel { get; }
    }
}
    

