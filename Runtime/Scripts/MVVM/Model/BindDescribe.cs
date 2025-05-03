using System;
using UnityEngine.Events;

namespace YunFramework.RunTime.MVVM
{
    /// <summary>
    /// 绑定描述
    /// </summary>
    public class BindDescribe<T> where T : class
    {
        public readonly string memberName;

        private readonly Binder<T> binder;

        public BindDescribe(Binder<T> binder, string memberName, UnityAction<object> unityAction)
        {
            this.binder = binder;
            
            this.memberName = memberName;
            
            this.unityAction = unityAction;
        }

        public UnityAction<object> unityAction;

        /// <summary>
        /// 添加来源监听
        /// </summary>
        public BindDescribe<T> From<TSource>(UnityEvent<TSource> unityEvent)
        {
            binder.From(this, unityEvent);
            return this;
        }

        /// <summary>
        /// 删除 来源监听
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public BindDescribe<T> UnFrom<TSource>(UnityEvent<TSource> e)
        {
            binder.UnFrom(this, e);

            return this;
        }

        /// <summary>
        /// 通知谁
        /// </summary>
        public BindDescribe<T> To(Action<T> action)
        {
            binder.To(this, action);

            return this;
        }

        /// <summary>
        /// 解除通知方
        /// </summary>
        /// <param name="action"></param>
        public BindDescribe<T> Unto(Action<T> action)
        {
            binder.Unto<T>(this, action);

            return this;
        }
    }
}