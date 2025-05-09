#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		AsyncExpand.cs
// Author Name :		YunShu
// Create Time :		2022/12/03 15:37:14
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;


namespace LJVoyage.Game.Runtime.Utility { 
/// <summary>
/// 异步拓展
/// </summary>
public static class AsyncExpand
{
    /// <summary>
    /// 扩展方法
    /// https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
    /// </summary>
    /// <param name="asyncOp"></param>
    /// <returns></returns>
    public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
    {
        var tcs = new TaskCompletionSource<object>();
        asyncOp.completed += obj => { tcs.SetResult(null); };
        return ((System.Threading.Tasks.Task)tcs.Task).GetAwaiter();
    }
}
}