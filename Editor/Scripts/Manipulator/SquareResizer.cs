using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor
{
    public class SquareResizer : MouseManipulator
    {
        private bool m_Active;

        private Vector2 m_Start;

        private readonly int m_Direction;

        public E_Orientation m_Orientation;

        private readonly Action<float> Resizer;

        /// <summary>
        /// 矩形大小调整
        /// </summary>
        /// <param name="orientation"> 方向 </param>
        /// <param name="resizer"> 回调 </param>
        public SquareResizer(E_Orientation orientation, Action<float> resizer)
        {
            Resizer = resizer;

            m_Orientation = orientation;

            m_Active = false;

            activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
        }

        protected override void RegisterCallbacksOnTarget()
        {
            var pos = target.transform.position;

            target.style.position = Position.Relative;

            target.transform.position = pos;

            target.RegisterCallback<MouseDownEvent>(OnMouseDown);
            target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            target.RegisterCallback<MouseUpEvent>(OnMouseUp);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
            target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
            target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
        }

        private void OnMouseDown(MouseDownEvent e)
        {
            if (m_Active)
            {
                e.StopImmediatePropagation();

                return;
            }

            if (!CanStartManipulation(e)) return;

            m_Start = e.localMousePosition;

            m_Active = true;

            target.CaptureMouse();

            target.BringToFront();

            e.StopPropagation();
        }

        private void OnMouseMove(MouseMoveEvent e)
        {
            
            if (!m_Active || !target.HasMouseCapture())
                return;

            var diff = e.localMousePosition - m_Start;

            var delta = diff.x;

            if (m_Orientation == E_Orientation.Vertical)
                delta = diff.y;

            
            // Debug.Log($"{e.originalMousePosition}\n {target.transform.position} \n{target.layout} \n");
            
            Resizer(delta);

            e.StopPropagation();
        }

        private void OnMouseUp(MouseUpEvent e)
        {
            if (!m_Active || !target.HasMouseCapture() || !CanStopManipulation(e))
                return;

            m_Active = false;

            target.ReleaseMouse();

            e.StopPropagation();
        }
    }
}