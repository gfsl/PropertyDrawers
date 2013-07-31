using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PopupAttribute))]
public class PopupAttributeDrawer : PropertyDrawer {

	public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label) {
		var attrib = attribute as PopupAttribute;

		int index = attrib.optionsList.IndexOf(prop.stringValue);
		if (index < 0) index = 0;

		index = EditorGUI.Popup(position, label.text, index, attrib.optionsList.ToArray());

		prop.stringValue = attrib.optionsList[index];
	}
}
