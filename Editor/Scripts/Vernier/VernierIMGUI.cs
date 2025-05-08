using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor
{
    public class VernierIMGUI : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<VernierIMGUI, UxmlTraits>
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

                if (ve is not VernierIMGUI target) return;

                target.LineColor = LineColor.GetValueFromBag(bag, cc);
            }
        }

        private readonly List<VisualElement> m_Children = new();

        public VernierIMGUI() : this(Color.white)
        {
        }

        private readonly IMGUIContainer imguiContainer;

        public VernierIMGUI(Color lineColor)
        {
            LineColor = lineColor;

            style.height = 40;
            
            style.backgroundColor = new StyleColor(new Color(0.3f, 0.3f, 0.3f));

            imguiContainer = new IMGUIContainer(OnGUIHandler);

            Add(imguiContainer);

            imguiContainer.style.width = new StyleLength(new Length(100, LengthUnit.Percent));

            imguiContainer.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
        }

        private void OnGUIHandler()
        {
            Handles.BeginGUI();

            var rect = imguiContainer.layout;

            Handles.color = LineColor;

            var step = 8;

            var Index = 0;

            for (var i = rect.x + 2; i < rect.width; i += step)
            {
                if (Index % 5 == 0)
                {
                    Handles.DrawLine(new Vector3(i, rect.y + rect.height - 20),
                        new Vector3(i, rect.y + rect.height - 5));

                    string str = Index.ToString();

                    if (str.Length > 2)
                    {
                        GUI.Label(new Rect(i - 15, rect.y + 12, 30, 12), str);
                    }
                    else if (str.Length > 1)
                    {
                        GUI.Label(new Rect(i - 10, rect.y + 12, 20, 12), str);
                    }
                    else
                    {
                        GUI.Label(new Rect(i - 5, rect.y + 12, 12, 12), str);
                    }
                }
                else
                {
                    Handles.DrawLine(new Vector3(i, rect.y + rect.height - 15),
                        new Vector3(i, rect.y + rect.height - 10));
                }
                
                Index++;
            }

            Handles.EndGUI();
        }

        private Color lineColor;

        public Color LineColor
        {
            get => lineColor;
            set
            {
                if (lineColor.Equals(value))
                    return;

                lineColor = value;
            }
        }
    }
}