using System;
using LJVoyage.Game.Manipulator;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game
{
    public class TwoPaneView : VisualElement
    {
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

        public new class UxmlFactory : UxmlFactory<TwoPaneView, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private UxmlEnumAttributeDescription<E_Orientation> Orientation = new()
            {
                name = $"{nameof(Orientation)}",
                defaultValue = E_Orientation.Vertical
            };

            private UxmlIntAttributeDescription MinDimension = new ()
            {
                name = $"{nameof(MinDimension)}",
                defaultValue = 200
            };


            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                if (ve is not TwoPaneView target) return;

                target.Orientation = Orientation.GetValueFromBag(bag, cc);
                target.MinDimension = MinDimension.GetValueFromBag(bag, cc);
            }
        }

        private VisualElement m_LeftPane;

        private VisualElement m_RightPane;

        public VisualElement LeftPane
        {
            get
            {
                if (m_LeftPane != null) return m_LeftPane;
                m_LeftPane = new VisualElement();
                m_Content.Add(m_LeftPane);
                LeftPanelStyleRebuilder();
                return m_LeftPane;
            }

            private set => m_LeftPane = value;
        }

        public VisualElement RightPane
        {
            get
            {
                if (m_RightPane != null) return m_RightPane;
                m_RightPane = new VisualElement();
                m_Content.Add(m_RightPane);
                RightPanelStyleRebuilder();
                return m_RightPane;
            }

            private set => m_RightPane = value;
        }

        private readonly DragLineAnchor m_DragLineAnchor;

        private readonly VisualElement m_Content;

        private E_Orientation m_Orientation;

        private int m_FixedPaneIndex;

        private int m_MinDimension = 150;

        private int m_MaxDimension = 200;

        public int MaxDimension
        {
            get => m_MaxDimension;
            set
            {
                if (m_MaxDimension == value)
                    return;

                m_MaxDimension = value;

                switch (Orientation)
                {
                    case E_Orientation.Horizontal:
                        LeftPane.style.maxWidth = m_MaxDimension;
                        LeftPane.style.maxHeight = StyleKeyword.Null;
                        break;
                    case E_Orientation.Vertical:
                        LeftPane.style.maxWidth = StyleKeyword.Null;
                        LeftPane.style.maxHeight = m_MaxDimension;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int MinDimension
        {
            get => m_MinDimension;
            set
            {
                if (m_MinDimension == value)
                    return;
                m_MinDimension = value;

                switch (Orientation)
                {
                    case E_Orientation.Horizontal:
                        LeftPane.style.minWidth = m_MinDimension;
                        LeftPane.style.minHeight = StyleKeyword.Null;
                        break;
                    case E_Orientation.Vertical:
                        LeftPane.style.minWidth = StyleKeyword.Null;
                        LeftPane.style.minHeight = m_MinDimension;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public E_Orientation Orientation
        {
            get => m_Orientation;
            set
            {
                if (m_Orientation == value)
                    return;

                m_Orientation = value;

                contentContainer.style.flexDirection = m_Orientation switch
                {
                    E_Orientation.Horizontal => FlexDirection.Row,
                    E_Orientation.Vertical => FlexDirection.Column,
                    _ => throw new ArgumentOutOfRangeException()
                };

                m_DragLineAnchor.Orientation = value;

                PaneStyleRebuilder();

                DragLineAnchorLayoutRebuilder();
            }
        }

        private SquareResizer squareResizer;

        public TwoPaneView()
        {
            AddToClassList(s_UssClassName);

            m_Content = new VisualElement()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    flexWrap = Wrap.NoWrap
                }
            };
            m_Content.AddToClassList(s_ContentContainerClassName);

            hierarchy.Add(m_Content);

            // Create drag anchor line.
            m_DragLineAnchor = new DragLineAnchor
            {
                Resizer = delta => OnResizer(delta, LeftPane, RightPane),
                Orientation = Orientation
            };

            hierarchy.Add(m_DragLineAnchor);
            
            PaneStyleRebuilder();
            DragLineAnchorLayoutRebuilder();
        }

        private float OnResizer(float delta, VisualElement left, VisualElement right)
        {
            float oldValue;
            
            switch (Orientation)
            {
                case E_Orientation.Horizontal:

                    oldValue = left.style.width.value.value;

                    if (oldValue + delta < MinDimension)
                    {
                        left.style.width = MinDimension;
                    }
                    else if (oldValue + delta > MaxDimension)
                    {
                        left.style.width = MaxDimension;
                    }
                    else
                    {
                        left.style.width = left.style.width.value.value + delta;
                    }

                    return left.style.width.value.value - oldValue;

                case E_Orientation.Vertical:

                    oldValue = left.style.height.value.value;

                    if (oldValue + delta < MinDimension)
                    {
                        left.style.height = MinDimension;
                    }
                    else if (oldValue + delta > MaxDimension)
                    {
                        left.style.height = MaxDimension;
                    }
                    else
                    {
                        left.style.height = left.style.height.value.value + delta;
                    }

                    return left.style.height.value.value - oldValue;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// 拖拽线 重新布局
        /// </summary>
        public void DragLineAnchorLayoutRebuilder()
        {
            if (contentContainer.childCount <= 0)
                return;

            Vector3 pos;

            switch (Orientation)
            {
                case E_Orientation.Horizontal:
                    pos = m_DragLineAnchor.transform.position;
                    pos.y = 0;
                    pos.x = contentContainer[0].style.width.value.value;
                    m_DragLineAnchor.transform.position = pos;
                    break;

                case E_Orientation.Vertical:
                    pos = m_DragLineAnchor.transform.position;
                    pos.x = 0;
                    pos.y = contentContainer[0].style.height.value.value;
                    m_DragLineAnchor.transform.position = pos;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 窗格 样式 重置
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void PaneStyleRebuilder()
        {
            switch (Orientation)
            {
                case E_Orientation.Horizontal:
                    LeftPane.style.width = m_MinDimension;
                    LeftPane.style.minWidth = m_MinDimension;
                    LeftPane.style.minHeight = new StyleLength(StyleKeyword.Null);
                    LeftPane.style.height = new StyleLength(new Length(100, LengthUnit.Percent));

                    RightPane.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
                    break;
                case E_Orientation.Vertical:
                    LeftPane.style.height = m_MinDimension;
                    LeftPane.style.minHeight = m_MinDimension;
                    LeftPane.style.minWidth = new StyleLength(StyleKeyword.Null);
                    LeftPane.style.width = new StyleLength(new Length(100, LengthUnit.Percent));

                    RightPane.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 右侧窗格 样式重置
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void RightPanelStyleRebuilder()
        {
            RightPane.style.flexShrink = 0;
            RightPane.style.flexGrow = 1;
            RightPane.style.flexShrink = 0;
            RightPane.style.flexBasis = 0;
        }

        /// <summary>
        /// 左侧窗格 样式重置
        /// </summary>
        private void LeftPanelStyleRebuilder()
        {
            switch (Orientation)
            {
                case E_Orientation.Horizontal:
                    LeftPane.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
                    LeftPane.style.width = m_MinDimension;
                    
                    LeftPane.style.maxWidth = MaxDimension;
                    LeftPane.style.maxHeight = StyleKeyword.Null;
                    break;
                case E_Orientation.Vertical:
                    LeftPane.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
                    LeftPane.style.height = m_MinDimension;
                    
                    LeftPane.style.maxWidth = StyleKeyword.Null;
                    LeftPane.style.maxHeight = MaxDimension;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 子元素被添加到这个元素中，通常是this
        /// </summary>
        public override VisualElement contentContainer
        {
            get { return m_Content; }
        }
    }
}