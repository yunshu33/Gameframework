using System.Collections.Generic;
using LJVoyage.Game;
using LJVoyage.Game.Editor;
using LJVoyage.Game.Runtime.TimeLine;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor.TimeLine
{
    public class TimeLineWindow : EditorWindow
    {
        [MenuItem("LJVoyage/TimeLineWindow")]
        public static void ShowExample()
        {
            var wnd = GetWindow<TimeLineWindow>();
            wnd.titleContent = new GUIContent("TimeLine");
        }

        private List<GroupTrack> groupTracks = new ();

        private VisualElement trackHeadPanel;

        private VisualElement trackBodyPanel;


        private TwoPaneView paneView;

        public void CreateGUI()
        {
            var root = rootVisualElement;

            paneView = new TwoPaneView
            {
                Orientation = E_Orientation.Horizontal,
            };

            root.Add(paneView);
        }


        private void AddTrack(GroupTrack track)
        {
           
        }
    }
}