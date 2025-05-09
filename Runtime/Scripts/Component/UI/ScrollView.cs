using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LJVoyage.Game.Runtime.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollView : UIBehaviour
    {
        public GameObject prefab;

        [Header("最大展示数量")] [Tooltip("0表示无限制")] [SerializeField]
        private int maxShowCount ;

        public UnityEvent<GameObject, int, object> createChildEvent;

        public UnityEvent<GameObject> resetChild;

        /// <summary>
        /// 页码变化
        /// </summary>
        public UnityEvent pageChange;
        
        private List<GameObject> children = new List<GameObject>();

        public List<GameObject> Children
        {
            get => children;
        }

        private object userData;

        private int page ;

        /// <summary>
        /// 页码从0开始
        /// </summary>
        public int Page
        {
            get { return page > MaxPage ? MaxPage : page; }

            private set
            {
                page = value;
                CreatePage();
                pageChange.Invoke();
            }
        }

        public int MaxPage
        {
            get => maxShowCount == 0 ? 0 : (int)Math.Ceiling(Count / (float)maxShowCount) - 1;
        }

        private int count;

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get => count;
            private set => count = value;
        }

        private ScrollRect scrollRect;

        public ScrollRect ScrollRect
        {
            get
            {
                if (scrollRect == null)
                    scrollRect = GetComponent<ScrollRect>();
                return scrollRect;
            }

            private set => scrollRect = value;
        }

        public int MaxShowCount
        {
            get { return maxShowCount == 0 ? Count : maxShowCount; }
            set
            {
                if (Count <= 0)
                    throw new Exception("未创建child");

                maxShowCount = value < 0 ? 0 : value;

                Page = Page > MaxPage ? MaxPage : Page;
            }
        }


        public bool CanNext
        {
            get { return Page < MaxPage; }
        }

        public bool CanPre
        {
            get { return Page > 0; }
        }


        public void NextPage()
        {
            if (!CanNext) return;
            ++Page;
        }

        public void PrePage()
        {
            if (!CanPre) return;
            --Page;
        }

        private void CreatePage()
        {
            if (Page > MaxPage || Page < 0)
                throw new Exception();

            foreach (var child in children)
            {
                resetChild.Invoke(child);
            }

            var showCount = Page == MaxPage ? Count - MaxShowCount * Page : MaxShowCount;

            if (children.Count != showCount)
            {
                if (children.Count < showCount)
                {
                    CreateChildren(showCount);
                }
                else
                {
                    RemoveChildren(showCount);
                }
            }

            for (int i = 0; i < children.Count; i++)
            {
                createChildEvent.Invoke(children[i], this.Page * MaxShowCount + i, this.userData);
            }

            ScrollRect.horizontalNormalizedPosition = 0;

            ScrollRect.verticalNormalizedPosition = 0;
        }


        private void CreateChildren(int count)
        {
            for (int i = children.Count; i < count; i++)
            {
                var child = Instantiate(prefab, ScrollRect.content);

                children.Add(child);
            }
        }

        private void RemoveChildren(int count)
        {
            var remove = children.GetRange(count, children.Count - count);

            foreach (var item in remove)
            {
                Destroy(item);
            }

            children = children.GetRange(0, count);
        }


        public void Create(int count, object userData = null, int maxShowCount = 0)
        {
            this.Count = count;
            
            this.userData = userData;
            
            if (maxShowCount != 0)
            {
                MaxShowCount = maxShowCount;
            }

            //必须最后设置页码
            this.Page = 0;
        }
    }
}