#region Copyright

// **********************************************************************
// Copyright (C) 2023 HeJing
//
// Script Name :		TaskSystem.cs
// Author Name :		YunShu
// Create Time :		2023/09/19 10:14:44
// Description :
// **********************************************************************

#endregion


using System.Collections.Generic;
using LJVoyage.Game.Tasks;

namespace LJVoyage.Game
{
    public class TaskSystem : SystemBase
    {
        private List<GameTaskBase> tasks;

        public List<GameTaskBase> Tasks
        {
            get => tasks ??= new List<GameTaskBase>();
            private set => tasks = value;
        }

        public override void InitConfig()
        {
        }

        public void AddTask(GameTaskBase task)
        {
            Tasks.Add(task);
        }

        public int Count => Tasks.Count;

        public void Remove(GameTaskBase task)
        {
            Tasks.Remove(task);
        }
    }
}