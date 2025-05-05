using UnityEngine;

namespace LJVoyage.Game
{
    public static class RectTransformUtility 
    {
        /// <summary>
        /// 获得中心点
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <returns></returns>
        public static Vector2 GetCentralPoint(this RectTransform rectTransform)
        {
            var leftBottom = rectTransform.rect.size * rectTransform.pivot ;

            var center = rectTransform.rect.size * 0.5f * Vector3.one;

            var positionV3 = rectTransform.position;
            
            var position = new Vector2(positionV3.x,positionV3.y);
            
            return position -leftBottom + center;
        }
        public static Vector2 GetLeftTopPoint(this RectTransform rectTransform)
        {
            var leftBottom = rectTransform.rect.size * rectTransform.pivot ;

            var center = rectTransform.rect.size * new Vector2(0,1);

            var positionV3 = rectTransform.position;
            
            var position = new Vector2(positionV3.x,positionV3.y);
            
            return position -leftBottom + center;
        }
    }
}
