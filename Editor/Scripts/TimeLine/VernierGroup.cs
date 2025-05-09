using LJVoyage.Game.Runtime.TimeLine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor.TimeLine
{
    public class VernierGroup : GroupTrack
    {
        public VernierGroup() :base(new VisualElement(),new Vernier())
        {
            InitHeader(headerContent);
        }

        private VisualElement InitHeader(VisualElement element)
        {
            var toolbar = new Toolbar
            {
                style =
                {
                    height = 35,
                    width = new StyleLength(new Length(100,LengthUnit.Percent))
                }
            };
            
            toolbar.Add(new Button());
            
            element.Add(toolbar);
            
            return element;
        }
    }
}