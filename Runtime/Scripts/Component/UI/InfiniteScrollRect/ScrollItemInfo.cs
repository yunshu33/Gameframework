using System;
using LJVoyage.Game.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LJVoyage.Game.UI
{

    public class ScrollItemInfo : UIBehaviour
    {
        [SerializeField]
        [ReadOnly]
        private int id;

        public Bounds bounds;

        public InfiniteScrollRect infiniteScrollRect;

        private RectTransform rectTransform;

        public int ID
        {
            get => id;
            set
            {
                id = value;
                infiniteScrollRect.ContentCalculatingLayout();
            }
        }

        public RectTransform RectTransform
        {
            get => rectTransform;
            private set => rectTransform = value;
        }

        protected override void Awake()
        {
            rectTransform = transform as RectTransform;

            if (rectTransform == null)
                throw new Exception();

            bounds = new Bounds(rectTransform.GetCentralPoint(), rectTransform.rect.size);
        }

        /// <summary>
        /// 重置矩形
        /// </summary>
        /// <returns></returns>
        public Bounds ResetBounds()
        {
            bounds.center = rectTransform.GetCentralPoint();
            return bounds;
        }
    }
}