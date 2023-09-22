#region Copyright

// **********************************************************************
// Copyright (C) 2023 HeJing
//
// Script Name :		GameWorldTimeSystem.cs
// Author Name :		YunShu
// Create Time :		2023/09/18 11:55:59
// Description :
// **********************************************************************

#endregion


using UnityEngine;
using UnityEngine.Events;

namespace GameWorldFramework.RunTime
{
    public class GameWorldTimeSystem : SystemBase
    {
        private float passingTime;

        public float PassingTime
        {
            get => passingTime;
            set
            {
                if (passingTime > DailyInterval)
                {
                    Day++;
                    passingTime = value - DailyInterval;
                }
                else
                {
                    passingTime = value;
                }

               
            }
        }

        private int dailyInterval;

        /// <summary>
        /// 每日时间间隔
        /// </summary>
        public int DailyInterval
        {
            get => dailyInterval;
            set => dailyInterval = value;
        }

        private int day;

        public int Day
        {
            get => day;
            private set
            {
                if (value >= 30)
                {
                    Month++;
                }

                day = value % 30;

                DayChange.Invoke(this);
            }
        }

        /// <summary>
        /// 天数变化
        /// </summary>
        public UnityEvent<GameWorldTimeSystem> DayChange = new UnityEvent<GameWorldTimeSystem>();

        private int month;

        public int Month
        {
            get => month;

            private set
            {
                month = value;
                MonthChange.Invoke(this);
            }
        }

        public UnityEvent<GameWorldTimeSystem> MonthChange = new UnityEvent<GameWorldTimeSystem>();

        private bool pause = true;

        public bool Pause
        {
            get => pause;
            set => pause = value;
        }

        protected override void Awake()
        {
            base.Awake();
           
        }

        public override void InitConfig()
        {
            DailyInterval = gameWorld.Config.TimeSystemConfig.DailyInterval;
        }

        private void Update()
        {
            if (Pause)
                return;
            PassingTime += Time.deltaTime;
        }
    }
}