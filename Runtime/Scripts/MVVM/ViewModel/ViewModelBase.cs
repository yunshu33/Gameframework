
using UnityEngine;
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace YunFramework.RunTime.MVVM
{

    /// <summary>
    /// 视图模型基类
    /// </summary>
    /// <typeparam name="V">视图</typeparam>
    /// <typeparam name="M">数据模型</typeparam>
    public class ViewModelBase<V, M> : MonoBehaviour where V : ViewBase where M : class, IModelBase<M>, new()
    {

        public delegate void ModelValueChangedHandler(M newValue, M oldValue);

        /// <summary>
        /// 值变化句柄
        /// </summary>
        public  ModelValueChangedHandler OnModelValueChanged;

        [Header("视图模型")]
        [SerializeField]
        private V view;

        [Header("数据模型")]
        [SerializeField]
        private M model;

        /// <summary>
        /// 视图模型
        /// </summary>
        protected virtual V View
        {

            get => view;

            set => view = value;

        }
        /// <summary>
        /// 数据模型
        /// </summary>
        protected virtual M Model
        {

            get => model;
            

            set
            {
                if ((model == null) || !model.Equals(value))
                {

                    if (OnModelValueChanged != null)
                    {
                        OnModelValueChanged(value, model);
                    }
                   
                    model = value;
                }
            }
        }

    }
}