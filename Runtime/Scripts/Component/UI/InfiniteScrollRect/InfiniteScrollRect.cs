using System.Collections;
using System.Collections.Generic;
using LJVoyage.Game.Runtime.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace  LJVoyage.Game.Runtime.UI
{
    public class InfiniteScrollRect : ScrollRect
    {
        /// <summary>
        /// item 预制体
        /// </summary>
        public RectTransform itemPrefab;

        private IList data;

        private LinkedList<ScrollItemInfo> itemInfos;

        private Bounds viewPortBounds;

        protected override void Awake()
        {
            base.Awake();
            InitViewPort();
        }

        private void Update()
        {
            viewPortBounds.DrawBounds(Color.red);
        }


        /// <summary>
        /// 初始化视口
        /// </summary>
        private void InitViewPort()
        {
            viewPortBounds = new Bounds(viewport.GetCentralPoint(), viewport.rect.size);
        }


        protected virtual void OnScroll()
        {
            if (normalizedPosition.x > 1 && normalizedPosition.x < 0 ||
                normalizedPosition.y > 1 && normalizedPosition.y < 0)
                return;

            var itemInfoFirst = itemInfos.First;
            itemInfoFirst.Value.ResetBounds();

            var itemInfoLast = itemInfos.Last;
            itemInfoLast.Value.ResetBounds();

            //itemInfoFirst.Value.bounds.DrawBounds(Color.blue);
            //itemInfoLast.Value.bounds.DrawBounds(Color.magenta);

            if (!viewPortBounds.Intersects(itemInfoFirst.Value.bounds))
            {
                if (viewPortBounds.Intersects(itemInfoLast.Value.bounds))
                {
                    if (itemInfoLast.Value.ID == data.Count - 1)
                        return;

                    itemInfos.RemoveFirst();

                    itemInfos.AddLast(itemInfoFirst);

                    // 修改id 时 会自动触发 content layout
                    itemInfoFirst.Value.ID = itemInfoLast.Value.ID + 1;
                }
            }
            else
            {
                if (!viewPortBounds.Intersects(itemInfoLast.Value.bounds))
                {
                    if (itemInfoFirst.Value.ID == 0)
                        return;

                    itemInfos.RemoveLast();

                    itemInfos.AddFirst(itemInfoLast);

                    itemInfoLast.Value.ID = itemInfoFirst.Value.ID - 1;
                }
            }
        }

        protected override void SetContentAnchoredPosition(Vector2 position)
        {
            base.SetContentAnchoredPosition(position);

            OnScroll();
        }


        public void SetData(IList data)
        {
            this.data = data;

            SetContentSize();

            SetItemPrefab(itemPrefab);

            InitItem();
        }


        private void SetItemPrefab(RectTransform prefab)
        {
            prefab.pivot = new Vector2(0, 1);
            prefab.anchorMax = new Vector2(0, 1);
            prefab.anchorMin = new Vector2(0, 1);
        }

        /// <summary>
        /// 设置content 大小
        /// </summary>
        private void SetContentSize()
        {
            var itemSize = itemPrefab.sizeDelta;

            this.content.sizeDelta = new Vector2(itemSize.x, itemSize.y * data.Count);
        }


        /// <summary>
        /// 内容计算布局
        /// </summary>
        public void ContentCalculatingLayout()
        {
            var itemSize = itemPrefab.rect.size;

            foreach (var itemInfo in itemInfos)
            {
                var itemInfoRectTransform = itemInfo.transform as RectTransform;

                if (itemInfoRectTransform != null)
                {
                    var anchoredPosition = itemInfoRectTransform.anchoredPosition;

                    itemInfoRectTransform.anchoredPosition = new Vector3(anchoredPosition.x, -itemInfo.ID * itemSize.y);
                }
            }
        }


        /// <summary>
        /// 初始化 Item
        /// </summary>
        private void InitItem()
        {
            var itemSize = itemPrefab.rect.size;

            var viewPortSize = this.viewport.rect.size;

            if (itemInfos != null)
            {
                itemInfos.Clear();
            }
            else
            {
                itemInfos ??= new LinkedList<ScrollItemInfo>();
            }

            var count = Mathf.CeilToInt(viewPortSize.y / itemSize.y);


            count += 2;

            for (int i = 0; i < count; i++)
            {
                var item = Instantiate(itemPrefab, this.content);

                var itemInfo = item.gameObject.AddComponent<ScrollItemInfo>();

                itemInfo.infiniteScrollRect = this;

                itemInfo.ID = i;

                itemInfos.AddLast(itemInfo);
            }

            ContentCalculatingLayout();
        }
    }
}