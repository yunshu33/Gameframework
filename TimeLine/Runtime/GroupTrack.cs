using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Yun.UIElements.Runtime.Expand;

namespace Yun.TimeLine
{
    public class GroupTrack
    {
        public  VisualElement header;
        public  VisualElement body;

        public VisualElement headerContent;
        public VisualElement bodyContent;
        
        protected VisualElement headerShading;
        protected VisualElement bodyShading;

        private const int height = 40;
        private Color headerColor;
        private Color bodyColor;

        protected GroupTrack(VisualElement header, VisualElement body)
        {
            headerContent = header;
            bodyContent = body;
            
            SetHeader(header);
            SetBody(body);

            this.header.style.height = height;
            this.body.style.height = height;

            this.body.RegisterCallback<MouseEnterEvent>(OnMouseEnterEvent);
            this.body.RegisterCallback<MouseLeaveEvent>(OnMouseLeaveEvent);
            
            this.header.RegisterCallback<MouseEnterEvent>(OnMouseEnterEvent);
            this.header.RegisterCallback<MouseLeaveEvent>(OnMouseLeaveEvent);
        }
        
        protected  void SetHeader(VisualElement header)
        {
            header.style.height = 35;

            this.header = new VisualElement
            {
                style =
                {
                    width = new StyleLength(new Length(100, LengthUnit.Percent))
                }
            };
            this.header.Add(header);

            this.headerShading = new VisualElement
            {
                style =
                {
                    width = new StyleLength(new Length(100, LengthUnit.Percent)),
                    backgroundColor = Color.white,
                    height = 5
                }
            };

            this.header.Add(headerShading);
        }
        
        private void SetBody(VisualElement body)
        {
            body.style.height = 35;
            
            this.body = new VisualElement
            {
                style =
                {
                    width = new StyleLength(new Length(100, LengthUnit.Percent))
                }
            };
            this.body.Add(body);

            this.bodyShading = new VisualElement
            {
                style =
                {
                    width = new StyleLength(new Length(100, LengthUnit.Percent)),
                    backgroundColor = Color.white,
                    height = 5
                }
            };
            this.body.Add(bodyShading);
        }

        protected virtual void OnMouseLeaveEvent(MouseLeaveEvent evt)
        {
            this.headerShading.style.backgroundColor = Color.white;
            this.bodyShading.style.backgroundColor = Color.white;
        }

        protected virtual void OnMouseEnterEvent(MouseEnterEvent evt)
        {
            this.headerShading.style.backgroundColor = Color.red;
            this.bodyShading.style.backgroundColor = Color.red;
        }
    }
}