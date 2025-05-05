using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LJVoyage.Game
{

    /// <summary>
    /// 继承mono 的单例基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// this
        /// </summary>
        private static T instance;
        /// <summary>
        ///多线程双重锁
        /// </summary>
        private static readonly object lockRoot = new();

        /// <summary>
        /// 初始化  单例
        /// </summary>
        public static T Instance
        {
            get
            {
                //锁
                lock (lockRoot)
                {
                    if (MonoSingletonObject.monoSingletonObject is null)
                    {
                        MonoSingletonObject.monoSingletonObject = new GameObject("SingletonObject");
                        DontDestroyOnLoad(MonoSingletonObject.monoSingletonObject);
                    }
                    if (MonoSingletonObject.monoSingletonObject is not null && instance is null)
                    {

                        instance = MonoSingletonObject.monoSingletonObject.AddComponent<T>();

                    }

                }
                return instance;
            }
        }
        /// <summary>
        /// 变化场景时是否销毁  默认不销毁
        /// </summary>
        protected bool switchingScenesDestroy = false;

        /// <summary>
        /// 添加Scene 切换 回调
        /// </summary>
        public void AddSceneChangedEvent()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }



        /// <summary>
        /// 场景变化时 触发
        /// </summary>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            if (switchingScenesDestroy == true)
            {
                if (instance is not null)
                {
                    DestroyImmediate(instance);
                }
            }
        }
        /// <summary>
        /// 添加场景切换事件 
        /// </summary>
        protected virtual void Start()
        {
            AddSceneChangedEvent();
        }

        protected virtual void OnDestroy()
        {

            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

    }

    /// <summary>
    /// 挂载单例脚本的类
    /// </summary>
    public class MonoSingletonObject
    {
        /// <summary>
        /// 挂载单例脚本的物体
        /// </summary>
        public static GameObject monoSingletonObject;

    }
}

