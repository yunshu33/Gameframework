using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Expand
{
    public static class StyleExpand
    {
        public static void BorderColor(this IStyle style, StyleColor color)
        {
            style.borderBottomColor = color;
            style.borderTopColor = color;
            style.borderLeftColor = color;
            style.borderRightColor = color;
        }

        public static void BorderWidth(this IStyle style, StyleFloat width)
        {
            style.borderBottomWidth = width;
            style.borderTopWidth = width;
            style.borderLeftWidth = width;
            style.borderRightWidth = width;
        }

        public static void MarginLength(this IStyle style, StyleLength length)
        {
            style.marginLeft = length;
            style.marginRight = length;
            style.marginTop = length;
            style.marginBottom = length;
        }

        public static void PaddingLength(this IStyle style, StyleLength length)
        {
            style.paddingLeft = length;
            style.paddingRight = length;
            style.paddingTop = length;
            style.paddingBottom = length;
            
        }

        public static void BorderRadiusLength(this IStyle style, StyleLength length)
        {
            style.borderBottomLeftRadius = length;
            style.borderBottomRightRadius = length;
            style.borderTopLeftRadius = length;
            style.borderTopRightRadius = length;
        } 
    }
}