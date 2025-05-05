using System;
using UnityEngine;
using UnityEngine.UIElements;
using Yun.UIElements.Runtime.Manipulator;
using Cursor = UnityEngine.UIElements.Cursor;

namespace Yun.UIElements.Runtime
{
    public class DragLineAnchor : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<DragLineAnchor, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private UxmlEnumAttributeDescription<E_Orientation> Orientation = new()
            {
                name = $"{nameof(Orientation)}",
                defaultValue = E_Orientation.Horizontal
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                if (ve is not DragLineAnchor target) return;

                target.Orientation = Orientation.GetValueFromBag(bag, cc);
            }
        }

        private const string s_UssClassName = "unity-two-pane-split-view";

        private const string s_ContentContainerClassName = "unity-two-pane-split-view__content-container";

        private const string s_HandleDragLineClassName = "unity-two-pane-split-view__dragline";

        private const string s_HandleDragLineVerticalClassName = s_HandleDragLineClassName + "--vertical";

        private const string s_HandleDragLineHorizontalClassName = s_HandleDragLineClassName + "--horizontal";

        private const string s_HandleDragLineAnchorClassName = "unity-two-pane-split-view__dragline-anchor";

        private const string s_HandleDragLineAnchorVerticalClassName = s_HandleDragLineAnchorClassName + "--vertical";

        private const string s_HandleDragLineAnchorHorizontalClassName =
            s_HandleDragLineAnchorClassName + "--horizontal";

        private const string s_VerticalClassName = "unity-two-pane-split-view--vertical";

        private const string s_HorizontalClassName = "unity-two-pane-split-view--horizontal";

        private readonly VisualElement m_DragLine;

        private readonly SquareResizer squareResizer;

        private E_Orientation m_Orientation = E_Orientation.Horizontal;

        /// <summary>
        /// 为空 或者 返回 false 不移动
        /// </summary>
        public Func<float,float> Resizer;

        public E_Orientation Orientation
        {
            get => m_Orientation;
            set
            {
                if (m_Orientation == value) return;

                m_Orientation = value;

                ChangeOrientation();
            }
        }

        public DragLineAnchor()
        {
            name = "unity-dragline-anchor";

            AddToClassList(s_HandleDragLineAnchorClassName);

            // Create drag
            m_DragLine = new VisualElement
            {
                name = "unity-dragline"
            };

            m_DragLine.AddToClassList(s_HandleDragLineClassName);

            Add(m_DragLine);

            squareResizer = new SquareResizer(m_Orientation, OnResizer);

            this.AddManipulator(squareResizer);

            ChangeOrientation();
        }

        private void OnResizer(float delta)
        {
            if (Resizer is null)
                throw new Exception("Resizer 回调函数不可为空");

            delta = Resizer.Invoke(delta);
            
            if (delta == 0 )
                return;

            var position = transform.position;

            switch (m_Orientation)
            {
                case E_Orientation.Horizontal:
                    position.x += delta;
                    break;

                case E_Orientation.Vertical:
                    position.y += delta;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            transform.position = position;
        }

        /// <summary>
        /// 方向改变时触发
        /// </summary>
        private void ChangeOrientation()
        {
            switch (Orientation)
            {
                case E_Orientation.Horizontal:
                    style.height = new StyleLength(new Length(100, LengthUnit.Percent));
                    style.width = 1;
                    m_DragLine.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
                    m_DragLine.style.width = 10;
                    m_DragLine.style.top = 0;
                    m_DragLine.style.left = -5;
                    break;

                case E_Orientation.Vertical:
                    style.height = 1;
                    style.width = new StyleLength(new Length(100, LengthUnit.Percent));
                    m_DragLine.style.height = 10;
                    m_DragLine.style.top = -5;
                    m_DragLine.style.left = 0;
                    m_DragLine.style.width = new StyleLength(new Length(100, LengthUnit.Percent));

                    style.cursor = new StyleCursor(new Cursor());
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            m_DragLine.RemoveFromClassList(s_HandleDragLineHorizontalClassName);

            m_DragLine.RemoveFromClassList(s_HandleDragLineVerticalClassName);

            m_DragLine.AddToClassList(Orientation == E_Orientation.Horizontal
                ? s_HandleDragLineHorizontalClassName
                : s_HandleDragLineVerticalClassName);


            if (squareResizer != null)
            {
                squareResizer.m_Orientation = m_Orientation;
            }
        }
    }
}