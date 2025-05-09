using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Runtime.Utility
{
    public static class ImageHelper
    {
        /// <summary>
        /// 获得自适应尺寸
        /// </summary>
        /// <param name="sprite">精灵图片</param>
        /// <param name="max">最大尺寸</param>
        /// <returns></returns>
        public static Vector2 Adaptive(Sprite sprite,Vector2 max)
        {
            var size = sprite.bounds.size;

            if (max.x / size.x > max.y / size.y)
            {
                size *= max.y / size.y;
            }
            else
            {
                size *= max.x / size.x;
            }

            return size;
        }

    }

}