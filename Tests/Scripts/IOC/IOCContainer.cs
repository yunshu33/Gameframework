#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		IOCcontainer.cs
// Author Name :		YunShu
// Create Time :		2022/06/30 09:29:15
// Description :
// **********************************************************************
#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game
{
    /// <summary>
    /// IOC容器
    /// </summary>
    public class IOCContainer 
    {
        private  Dictionary<Type, object> m_Instances = new();

        /// <summary>
        /// 字典长度
        /// </summary>
        public int Count { get { return m_Instances.Count; } }

        /// <summary>
        /// 添加一个单例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instances"></param>
        public void Register<T>(T instances)
        {
            var key = typeof(T);
            if (m_Instances.ContainsKey(key))
            {
                m_Instances[key] = instances;
            }
            else
            {
                m_Instances.Add(key, instances);
            }
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class
        {
            var key = typeof(T);
            m_Instances.TryGetValue(key, out object retobj);
            return retobj as T;
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>() where T : class
        {
            var key = typeof(T);
            if (m_Instances.ContainsKey(key))
            {
                m_Instances.Remove(key);
            }
        }
        
    }
}