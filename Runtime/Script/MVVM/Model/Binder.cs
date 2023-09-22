#region Copyright

// **********************************************************************
// Copyright (C) 2023 
//
// Script Name :		Binder.cs
// Author Name :		云舒
// Create Time :		2023/03/27 14:40:26
// Description :
// **********************************************************************

#endregion


using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine.Events;

namespace GameWorldFramework.RunTime.MVVM
{
    public class Binder<T> : IDisposable where T : class
    {
        private Dictionary<string, List<Action<T>>> propertyChangedEvents = new Dictionary<string, List<Action<T>>>();

        private readonly Dictionary<string, BindDescribe<T>> bindDescribes = new Dictionary<string, BindDescribe<T>>();

        internal void ValueChange<Property>(T model, ref Property _oldValue, Property _newValue,
            [CallerMemberName] string propertyName = null)
        {
            if (!Equals(_oldValue, _newValue))
            {
                _oldValue = _newValue;

                if (propertyName != null && propertyChangedEvents.TryGetValue(propertyName, out var actions))
                {
                    foreach (var action in actions)
                    {
                        action(model);
                    }
                }
            }
        }

        readonly T model;

        public Binder(T model)
        {
            this.model = model;
        }


        /// <summary>
        /// 添加来源监听
        /// </summary>
        public BindDescribe<T> From<TSource>(BindDescribe<T> bindDescribe, UnityEvent<TSource> e)
        {
            e.AddListener((value) => { bindDescribe.unityAction(value); });

            return bindDescribe;
        }


        /// <summary>
        /// 删除 来源监听
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="bindDescribe"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public BindDescribe<T> UnFrom<TSource>(BindDescribe<T> bindDescribe, UnityEvent<TSource> e)
        {
            e.RemoveListener((value) => { bindDescribe.unityAction(value); });

            return bindDescribe;
        }

        /// <summary>
        /// 通知谁
        /// </summary>
        public BindDescribe<T> To(BindDescribe<T> bindDescribe, Action<T> action)
        {
            if (propertyChangedEvents.TryGetValue(bindDescribe.memberName, out var actions))
            {
                actions.Add(action);
            }
            else
            {
                actions = new List<Action<T>>
                {
                    action
                };

                propertyChangedEvents[bindDescribe.memberName] = actions;
            }

            return bindDescribe;
        }


        /// <summary>
        /// 解除通知方
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="bindDescribe"></param>
        /// <param name="action"></param>
        public void Unto<TSource>(BindDescribe<T> bindDescribe, Action<T> action)
        {
            if (propertyChangedEvents.TryGetValue(bindDescribe.memberName, out var actions))
            {
                actions.Remove(action);
            }
        }


        /// <summary>
        /// 获得bind 描述
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="expression">需要绑定的属性</param>
        /// <param name="action">属性改变时触发的函数</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BindDescribe<T> Bind<TSource>(Expression<Func<T, TSource>> expression, UnityAction<object> action)
        {
            if (!(expression.Body is MemberExpression))
            {
                throw new Exception("expression 错误 应为 MemberExpression 类型 Lambda 表达式 ");
            }

            var propertyName = LambadUtilities.ParseMemberName(expression);

            if (bindDescribes.TryGetValue(propertyName, out var bindDescribe))
            {
                bindDescribe.unityAction = action;
            }
            else
            {
                bindDescribe = new BindDescribe<T>(this, propertyName, action);
                bindDescribes.Add(propertyName, bindDescribe);
            }

            return bindDescribe;
        }


        public void Dispose()
        {
            foreach (var actions in propertyChangedEvents)
            {
                actions.Value.Clear();
            }

            propertyChangedEvents.Clear();

            propertyChangedEvents = null;
        }
    }
}