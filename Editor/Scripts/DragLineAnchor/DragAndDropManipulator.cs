using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor
{
    public class DragAndDropManipulator : PointerManipulator
    {
        public DragAndDropManipulator(VisualElement target)
        {
            this.target = target;
            root = target.parent;
            target.style.position = Position.Absolute;
        }

        /// <summary>
        /// 在目标元素上注册事件回调函数
        /// </summary>
        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
            target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
            //当 VisualElement 释放指针时发送的事件
            target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
            target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
            target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
        }

        private Vector2 targetStartPosition { get; set; }

        private Vector3 pointerStartPosition { get; set; }

        private bool enabled { get; set; }

        private VisualElement root { get; }

        private void PointerDownHandler(PointerDownEvent evt)
        {
            targetStartPosition = target.transform.position;

            pointerStartPosition = evt.position;
            //捕获指针。 VisualElement 捕获指针时，无论哪个元素在该指针下，所有指针事件都发送到该元素。
            target.CapturePointer(evt.pointerId);

            //将该元素移到其父元素列表的末尾。元素将在视觉上位于任何重叠的兄弟元素前面
            target.BringToFront();

            enabled = true;
        }

        private void PointerMoveHandler(PointerMoveEvent evt)
        {
            if (enabled && target.HasPointerCapture(evt.pointerId))
            {
                Vector3 pointerDelta = evt.position - pointerStartPosition;

                target.transform.position =
                    new Vector2(
                        Mathf.Clamp(targetStartPosition.x + pointerDelta.x, 0,
                            target.panel.visualTree.worldBound.width),
                        Mathf.Clamp(targetStartPosition.y + pointerDelta.y, 0,
                            target.panel.visualTree.worldBound.height));
            }
        }

        private void PointerUpHandler(PointerUpEvent evt)
        {
            if (enabled && target.HasPointerCapture(evt.pointerId))
            {
                target.ReleasePointer(evt.pointerId);

                //放到最下层渲染
                //target.SendToBack();
            }
        }

       
        /// <summary>
        /// 指针捕获输出处理程序
        /// 当 VisualElement 释放指针时发送的事件
        /// </summary>
        /// <param name="evt"></param>
        private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
        {
            if (enabled)
            {
                enabled = false;
            }
        }
    }
}