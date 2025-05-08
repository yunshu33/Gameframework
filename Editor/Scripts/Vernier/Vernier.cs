using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor
{
    /// <summary>
    /// 游标
    /// </summary>
    public class Vernier : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<Vernier, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private UxmlColorAttributeDescription LineColor = new()
            {
                name = $"{nameof(LineColor)}",
                defaultValue = Color.white
            };

            private UxmlFloatAttributeDescription ChildWidth = new()
            {
                name = $"{nameof(ChildWidth)}",
                defaultValue = 50
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                if (ve is not Vernier target) return;

                target.LineColor = LineColor.GetValueFromBag(bag, cc);
                target.ChildWidth = ChildWidth.GetValueFromBag(bag, cc);
            }
        }

        private readonly List<VisualElement> m_Children = new();

        public Vernier() : this(50, Color.white)
        {   
            
        }

        public Vernier(float childWidth) : this(childWidth, Color.white)
        {
            
        }

        public Vernier(float childWidth, Color lineColor)
        {
            ChildWidth = childWidth;

            LineColor = lineColor;

            style.flexWrap = Wrap.NoWrap;

            style.flexDirection = FlexDirection.Row;

            style.left = new StyleLength(new Length(0, LengthUnit.Percent));

            style.right = new StyleLength(new Length(0, LengthUnit.Percent));
            
            RegisterCallback<GeometryChangedEvent>(OnGeometryChangedEvent);
        }

        private void OnGeometryChangedEvent(GeometryChangedEvent evt)
        {

            if (Math.Abs(evt.oldRect.width - evt.newRect.width) == 0) return;

            var count = (int)Math.Ceiling(evt.newRect.width / childWidth);

            if (count > m_Children.Count)
            {
                for (var i = 0; i < count - m_Children.Count + i; i++)
                {
                    CreateChild();
                }
            }
            else if (m_Children.Count > count)
            {
                for (var i = 0; i < m_Children.Count - count + i; i++)
                {
                    var last = m_Children.LastOrDefault();

                    if (last == null)
                        return;
                    m_Children.Remove(last);

                    Remove(last);
                }
            }
        }

        private VisualElement CreateChild()
        {
            var label = new Label($" {m_Children.Count}")
            {
                style =
                {
                    width = ChildWidth,
                    height = this.style.height,
                    unityTextAlign = TextAnchor.UpperLeft,
                    fontSize = 10,
                    color = Color.white,
                    borderRightColor = LineColor,
                    borderRightWidth = 1,
                    backgroundColor = Color.gray,
                }
            };
            m_Children.Add(label);
            Add(label);
            return label;
        }
        
        private float childWidth;

        private Color lineColor;

        public float ChildWidth
        {
            get => childWidth;
            set
            {
                if (Math.Abs(value - childWidth) == 0)
                    return;

                childWidth = value;

                foreach (var child in m_Children)
                {
                    child.style.width = childWidth;
                }
            }
        }

        public Color LineColor
        {
            get => lineColor;
            set
            {
                if (lineColor.Equals(value))
                    return;
                
                lineColor = value;
                
                foreach (var child in Children())
                {
                    child.style.borderRightColor = lineColor;
                }
            }
        }
    }
}