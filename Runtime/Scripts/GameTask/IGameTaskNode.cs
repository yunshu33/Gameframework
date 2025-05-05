using System;

namespace LJVoyage.Game.Tasks
{
    public  interface IGameTaskNode
    {
        private Action fun()
        {
            return null;
        }
        
        public abstract TaskType NodeType
        {
            get;
        }

        /// <summary>
        /// 进入
        /// </summary>
        public  void Enter();

        /// <summary>
        /// 动作
        /// </summary>
        public  void Action(object arg);

        /// <summary>
        /// 完成
        /// </summary>
        public  void Complete();

        /// <summary>
        /// 获得进度
        /// </summary>
        public  void GetProgress();

        /// <summary>
        /// 判断
        /// </summary>
        /// <param name="arg"></param>
        public void Estimate(object arg);

    }
}