using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor
{
    public class EdgeConnector<TEdge> : EdgeConnector where TEdge : Edge, new()
    {
        private readonly EdgeDragHelper m_EdgeDragHelper;
        
        private Edge m_EdgeCandidate;
        
        private bool m_Active;
        
        private Vector2 m_MouseDownPosition;

        protected GraphView graphView;

        public EdgeConnector(IEdgeConnectorListener listener)
        {
            graphView = (listener as EdgeConnectorListener)?.graphView;

            m_EdgeDragHelper = new EdgeDragHelper<TEdge>(listener);
            m_Active = false;
            activators.Add(new ManipulatorActivationFilter
            {
                button = MouseButton.LeftMouse
            });
        }

        public override EdgeDragHelper edgeDragHelper => m_EdgeDragHelper;


        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<MouseDownEvent>(OnMouseDown);
            target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            target.RegisterCallback<MouseUpEvent>(OnMouseUp);
            target.RegisterCallback<KeyDownEvent>(OnKeyDown);
            target.RegisterCallback<MouseCaptureOutEvent>(OnCaptureOut);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
            target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
            target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
            target.UnregisterCallback<KeyDownEvent>(OnKeyDown);
            target.UnregisterCallback<MouseCaptureOutEvent>(OnCaptureOut);
        }

        protected virtual void OnMouseDown(MouseDownEvent e)
        {
            if (m_Active)
            {
                e.StopImmediatePropagation();
            }
            else
            {
                if (!CanStartManipulation(e) || !(this.target is Port targetPort))
                    return;
                m_MouseDownPosition = e.localMousePosition;
                m_EdgeCandidate = new TEdge();
                m_EdgeDragHelper.draggedPort = targetPort;
                m_EdgeDragHelper.edgeCandidate = m_EdgeCandidate;
                if (m_EdgeDragHelper.HandleMouseDown(e))
                {
                    m_Active = true;
                    this.target.CaptureMouse();
                    e.StopPropagation();
                }
                else
                {
                    m_EdgeDragHelper.Reset();
                    m_EdgeCandidate = null;
                }
            }
        }

        private void OnCaptureOut(MouseCaptureOutEvent e)
        {
            m_Active = false;
            if (m_EdgeCandidate == null)
                return;
            Abort();
        }

        protected virtual void OnMouseMove(MouseMoveEvent e)
        {
            if (!m_Active)
                return;
            m_EdgeDragHelper.HandleMouseMove(e);
            m_EdgeCandidate.candidatePosition = e.mousePosition;
            m_EdgeCandidate.UpdateEdgeControl();
            e.StopPropagation();
        }

        protected virtual void OnMouseUp(MouseUpEvent e)
        {
            if (!m_Active || !CanStopManipulation(e))
                return;
            if (CanPerformConnection(e.localMousePosition))
                m_EdgeDragHelper.HandleMouseUp(e);
            else
                Abort();
            m_Active = false;
            m_EdgeCandidate = null;
            target.ReleaseMouse();
            e.StopPropagation();
        }

        private void OnKeyDown(KeyDownEvent e)
        {
            if (e.keyCode != KeyCode.Escape || !m_Active)
                return;
            Abort();
            m_Active = false;
            target.ReleaseMouse();
            e.StopPropagation();
        }

        private void Abort()
        {
            target?.GetFirstAncestorOfType<GraphView>()
                ?.RemoveElement(m_EdgeCandidate);
            m_EdgeCandidate.input = null;
            m_EdgeCandidate.output = null;
            m_EdgeCandidate = null;
            m_EdgeDragHelper.Reset();
        }

        private bool CanPerformConnection(Vector2 mousePosition)
        {
            return Vector2.Distance(m_MouseDownPosition, mousePosition) > 10.0;
        }
    }
}