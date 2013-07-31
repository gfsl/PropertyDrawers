using UnityEngine;
using UnityEditor; 

public class DynamicRangeAttribute : PropertyAttribute  {

	public DynamicRangeAttribute() { }

	public float GetMin(SerializedProperty prop){
		var minProp = prop.serializedObject.FindProperty(prop.name + "_min");

		if(minProp == null){
			Debug.LogWarning(prop.name + "_min not found.");
			return 0.0f;
		}
		return ValueForProperty(minProp); 
	} 

	public float GetMax(SerializedProperty prop){
		var maxProp = prop.serializedObject.FindProperty(prop.name + "_max");

		if(maxProp == null){
			Debug.LogWarning(prop.name + "_max not found.");
			return 0.0f;
		}
		return ValueForProperty(maxProp); 
	}

	private float ValueForProperty(SerializedProperty prop){
		switch(prop.propertyType){
			case SerializedPropertyType.Integer:
				return prop.intValue;
			case SerializedPropertyType.Float:
				return prop.floatValue;
			default:
				return 0.0f;
		}
	}

}
