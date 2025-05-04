using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Yun.Tools
{
    public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

    [Serializable]
    public class Ingredient
    {
        public string name;
        public int amount = 1;
        public IngredientUnit unit;
    }

    public class GameRecipe : MonoBehaviour
    {
        public Ingredient potionResult;
        public Ingredient[] potionIngredients;

    }

    [CustomPropertyDrawer(typeof(Ingredient))]

    public class IngredientDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property); // 开始绘制属性

            var indent = EditorGUI.indentLevel; // 用来缓存修改之前的缩进值
            EditorGUI.indentLevel = 0;          // 修改缩进为 0，不缩进

            // 获取属性前值 label, 就是显示在此属性前面的那个名称 label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            // 定义此属性的子框体区域
            var amountRect = new Rect(position.x, position.y, 30, position.height);
            var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
            var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

            // 绘制子属性：PropertyField参数依次是: 框体位置，对应属性，显示的label
            // 如果你要显示一个label，第三个参数: new GUIContent("xxx")
            EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
            EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

            EditorGUI.indentLevel = indent;   // 恢复缩进

            EditorGUI.EndProperty();          // 完成绘制
        }
    }
}