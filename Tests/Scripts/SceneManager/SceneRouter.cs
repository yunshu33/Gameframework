using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Yun.Util;
namespace Yun.Framework
{
    /// <summary>
    /// 场景路由
    /// </summary>
    public class SceneRouter : MonoSingleton<SceneRouter>
    {


        /// <summary>
        /// 开始场景名称
        /// </summary>
        public static string startGameName = "InitFramework";



        /// <summary>
        /// 当前 场景节点
        /// </summary>
        private SceneNode nowSceneNode;

        /// <summary>
        /// 是否返回上层 
        /// </summary>
        private bool isToUpperScene = false;



        /// <summary>
        ///  自动跳转 至 配置的开始场景  startGameName
        /// </summary>
      //  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void ToStartGameScene()
        {

            if (startGameName is not null)
            {

                if (SceneManager.GetActiveScene().name == startGameName)
                {

                    return;

                }

                SceneManager.LoadScene(startGameName);

            }
            else
            {

                Debug.Log("已经启用SceneRouter但未配置游戏开始场景");

            }
        }

         void Awake()
        {

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        }

        void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            //若  nowSceneNode 为空 即 加载的场景是第一个场景
            //若  nowSceneNode.fatherNode 为空 即 加载的场景是第二个场景
            //若  nowSceneNode.fatherNode.SceneName 与加载的场景名称相同则 判断进行了回退操作   
            //回退场景操作加锁


            if (!isToUpperScene)
            {
                //双重检测 防止  使用Scene名称 或者 下标 直接  跳转        
                if (nowSceneNode is not null && nowSceneNode.fatherNode is not null && nowSceneNode.fatherNode.sceneName == arg0.name)
                {

                    nowSceneNode = nowSceneNode.fatherNode;

                }
                else if ((nowSceneNode is null || nowSceneNode.fatherNode is null || nowSceneNode.fatherNode.sceneName != arg0.name))
                {

                    SceneNode node = new()
                    {
                        sceneName = arg0.name,

                        fatherNode = nowSceneNode

                    };

                    nowSceneNode = node;
                }

            }

            //重置 
            isToUpperScene = false;

        }


        /// <summary>
        /// 移除 回调
        /// </summary>
        void OnDisable()
        {

            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;

        }


        /// <summary>
        /// 返回上层Scene
        /// </summary>
        public void ToUpperScene()
        {
            SceneManager.LoadScene(nowSceneNode.fatherNode.sceneName);
            nowSceneNode = nowSceneNode.fatherNode;
            isToUpperScene = true;
        }
        /// <summary>
        /// 前往最顶层 场景
        /// </summary>
        public void ToTopScene()
        {
            while (nowSceneNode.fatherNode is not null)
            {
                nowSceneNode = nowSceneNode.fatherNode;
            }
            string name = nowSceneNode.sceneName;
            nowSceneNode = null;
            SceneManager.LoadScene(name);

        }
        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="index"></param>
        public static void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}

