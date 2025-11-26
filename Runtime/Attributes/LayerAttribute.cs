using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace qb
{
    /// <summary>
    /// Usage [SerializeField, Layer] int m_EditorLayer = 31;
    /// </summary>
    public sealed class LayerAttribute:PropertyAttribute 
    {
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    internal sealed class LayerAttributeEditor : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }
    }
#endif

}
