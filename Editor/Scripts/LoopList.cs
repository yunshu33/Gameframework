#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		LoopList.cs
// Author Name :		YunShu
// Create Time :		2022/06/10 16:34:39
// Description :
// **********************************************************************
#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Yun.Util
{
	/// <summary>
	/// 游标数组/ 循环数组
	/// </summary>
	public class LoopList<T>
	{
		
		public List<T> list;
		/// <summary>
		/// 数量
		/// </summary>
		public int count { get; private set; }
		/// <summary>
		/// 游标
		/// </summary>
		public int tag;
       public LoopList(int count)
        {
			this.count = count;
			tag = 0;

			list = new List<T>(count);
		}
		public LoopList() : this(0)
        {

        }

		public T Next()
        {
			tag  = ++tag %this.count;
			return list[tag];
        }

		public T last()
        {
			tag = (--tag + this.count) % this.count;
			return list[tag];
        }

       
    }
}