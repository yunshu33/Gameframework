using System;
using System.Collections.Generic;
using GameWorldFramework.RunTime;
using UnityEngine.Events;

namespace YGameFramework.Runtime.Script.Task
{
    public abstract class GameTaskBase
    {
        private TaskSystem system;

        private LinkedList<IGameTaskNode> linkedList = new LinkedList<IGameTaskNode>();

        /// <summary>
        /// 任务完成回调
        /// </summary>
        public UnityEvent CompletionTask
        {
            get => completionTask ??= new UnityEvent();
            private set => completionTask = value;
        }

        private UnityEvent completionTask;


        private bool completion = false;
        
        protected TaskSystem TaskSystem
        {
            get => system;
            private set => system = value;
        }

        public bool Completion
        {
            get => completion;
           private set => completion = value;
        }

        public GameTaskBase AddTaskNode(IGameTaskNode gameTaskNode)
        {
            if (Completion == true)
            {
                throw new Exception("任务已经完成 不可添加 任务节点");
            }
            
            linkedList.AddLast(gameTaskNode);
            
            return this;
        }

        /// <summary>
        /// 开始做任务
        /// </summary>
        /// <returns></returns>
        public GameTaskBase Enter()
        {
            var first = linkedList.First.Value;

            if (first != null)
            {
                linkedList.First.Value.Enter();
            }
           
            return this;
        }

        private GameTaskBase NextTaskNode()
        {
            return this;
        }
        


        protected GameTaskBase(TaskSystem system)
        {
            TaskSystem = system;
        }
    }
}