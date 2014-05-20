using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PopupAttribute))]
public class PopupDrawer : PropertyDrawer {

	public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label) {
		var attrib = attribute as PopupAttribute;

		int index = attrib.optionsList.IndexOf(prop.stringValue);
		if (index < 0) index = 0;

		int newIndex = EditorGUI.Popup(position, label.text, index, attrib.optionsList.ToArray());

		if (newIndex != index) prop.stringValue = attrib.optionsList[newIndex];
	}
}
