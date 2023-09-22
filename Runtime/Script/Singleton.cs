using System;

namespace GameWorldFramework.RunTime.Module
{
    /// <summary>
    /// 单例
    /// </summary>
    public abstract class Singleton<T>  where T : class
    {
        private static T instance;

        /// <summary>
        ///多线程双重锁
        /// </summary>
        private static  object LockRoot = new object();

        public static T Instance
        {
            get
            {
                if (instance == null)

                    lock (LockRoot)
                    {
                        if (instance == null)
                        {
                            instance = (T)Activator.CreateInstance(typeof(T), true);
                            if (instance is Singleton<T> ins) ins.Initialization();
                        }
                            
                    }

                return instance;
            }
        }

        protected abstract void Initialization();
    }
}