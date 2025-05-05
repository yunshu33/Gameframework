using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LJVoyage.Game.Editor
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

            EditorGUI.BeginProperty(position, label, property); // ��ʼ��������

            var indent = EditorGUI.indentLevel; // ���������޸�֮ǰ������ֵ
            EditorGUI.indentLevel = 0;          // �޸�����Ϊ 0��������

            // ��ȡ����ǰֵ label, ������ʾ�ڴ�����ǰ����Ǹ����� label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            // ��������Ե��ӿ�������
            var amountRect = new Rect(position.x, position.y, 30, position.height);
            var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
            var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

            // ���������ԣ�PropertyField����������: ����λ�ã���Ӧ���ԣ���ʾ��label
            // �����Ҫ��ʾһ��label������������: new GUIContent("xxx")
            EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
            EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

            EditorGUI.indentLevel = indent;   // �ָ�����

            EditorGUI.EndProperty();          // ��ɻ���
        }
    }
}