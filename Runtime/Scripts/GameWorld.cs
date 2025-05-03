#region

// **********************************************************************
// Create Time :		2022/11/15 12:16:18
// Description :
// **********************************************************************

#endregion

using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using YunFramework.RunTime.Module;
using YunFramework.RunTime.Utility.Serializable;

namespace YunFramework.RunTime
{
    public class GameWorld : Singleton<GameWorld>
    {
        /// <summary>
        /// 系统字典
        /// </summary>
        private readonly Dictionary<Type, ISystem> systems = new Dictionary<Type, ISystem>();

        private readonly GameObject gameWorld;

        public GameWorldConfig Config { get; private set; }

        private GameWorld()
        {
            gameWorld = new GameObject("GameWorld");

            UnityEngine.Object.DontDestroyOnLoad(gameWorld);

            var asset = Resources.Load<TextAsset>("GameWorldConfig");

            Config = XmlSerializable.Deserialize<GameWorldConfig>(asset.ToString());

            if (Config == null)
            {
                Debug.LogWarning("未创建配置文件,使用默认配置运行");
            }

            Debug.Log("GameWorld 启动成功");
        }

        protected override void Initialization()
        {
            StartUpSystem();
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Auto()
        {
            var gw = GameWorld.Instance;
        }

        private void StartUpSystem()
        {
            var assembly = this.GetType().Assembly;

            foreach (var systemName in Config.AutoSystemNames)
            {
                var type = assembly.GetType(systemName);

                if (type == null)
                {
                    Debug.LogError($"未找到 类型 {systemName}");
                    break;
                }

                GetSystem(type);
            }
        }

        public object GetSystem(Type type)
        {
            if (type == null)
            {
                throw new Exception("type 不可为空");
            }

            return systems.TryGetValue(type, out var value) ? value : CreationSystem(type);
        }

        public T GetSystem<T>() where T : ISystem, new()
        {
            return (T)GetSystem(typeof(T));
        }

        private object CreationSystem(Type type)
        {
            var obj = new GameObject(type.Name);

            var system = obj.AddComponent(type);

            if (system == null)
                throw new Exception($"添加 {type.Name} Component 失败");

            system.transform.parent = gameWorld.transform;

            return system;
        }

        /// <summary>
        /// 创建模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T CreationSystem<T>() where T : ISystem, new()
        {
            return (T)CreationSystem(typeof(T));
        }

        public ISystem AddSystem(ISystem system)
        {
            if (!systems.TryAdd(system.GetType(), system))
            {
                Debug.LogError($"{system.GetType()} 已经存在");

                UnityEngine.Object.Destroy(system.GameObject);

                return null;
            }

            system.InitConfig();

            system.GameObject.transform.parent = gameWorld.transform;

            return system;
        }
    }
}