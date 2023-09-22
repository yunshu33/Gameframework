using System;
using GameWorldFramework.RunTime;
using UnityEngine;

namespace YGameFramework.Runtime.Script.Task.TaskNode
{
    public class NoneTaskNode : IGameTaskNode
    {
       
        public TaskType NodeType => TaskType.None;

        public void Enter()
        {
            GameWorld.Instance.GetSystem<GameWorldTimeSystem>().DayChange.AddListener(Action);
        }

        public void Action(object arg)
        {
            Complete();
        }

        public void Complete()
        {
            GameWorld.Instance.GetSystem<GameWorldTimeSystem>().DayChange.RemoveListener(Action);

            Debug.Log("NoneTaskNode  - Complete");
        }

        public void GetProgress()
        {
            throw new System.NotImplementedException();
        }

        public void Estimate(object arg)
        {
            throw new System.NotImplementedException();
        }
    }
}