using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkit
{
    public class Track : VisualElement
    {
       

        public new class UxmlFactory : UxmlFactory<Track, UxmlTraits>
        {
            
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription m_String =
                new UxmlStringAttributeDescription { name = "my-string", defaultValue = "default_value" };
            UxmlIntAttributeDescription m_Int =
                new UxmlIntAttributeDescription { name = "my-int", defaultValue = 2 };
            UxmlIntAttributeDescription IntT =
                new UxmlIntAttributeDescription { name = $"{nameof(Int2)}", defaultValue = 2 };


            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc); 

                if (ve is not Track target) return;
                target.myString = m_String.GetValueFromBag(bag, cc);
                target.myInt = m_Int.GetValueFromBag(bag, cc);
                target.Int2 = IntT.GetValueFromBag(bag, cc);
            }
             
        }

        public Track()
        {
            
            var button = new Button();

            this.Add(button);
        }

        
        
        protected override void ExecuteDefaultActionAtTarget(EventBase evt)
        {
            base.ExecuteDefaultActionAtTarget(evt);
        }

        // Must expose your element class to a { get; set; } property that has the same name 
        // as the name you set in your UXML attribute description with the camel case format
        public string myString { get; set; }
        public int myInt { get; set; }
        public int Int2 { get; set; }
    }
}