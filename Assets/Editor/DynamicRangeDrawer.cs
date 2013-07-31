using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DynamicRangeAttribute))]
public class DynamicRangeDrawer : PropertyDrawer  {

	public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label) {
		var reflectedAttribute = attribute as DynamicRangeAttribute;

		if (prop.propertyType == SerializedPropertyType.Float)
			EditorGUI.Slider(position, prop, reflectedAttribute.GetMin(prop), reflectedAttribute.GetMax(prop), label);
		else if(prop.propertyType == SerializedPropertyType.Integer)
			EditorGUI.IntSlider(position, prop, (int)reflectedAttribute.GetMin(prop), (int)reflectedAttribute.GetMax(prop), label);
		else
			EditorGUI.HelpBox(position, "DynamicRangeDrawer can only be used on Floats and Ints.", MessageType.Error); 	
	}

}
