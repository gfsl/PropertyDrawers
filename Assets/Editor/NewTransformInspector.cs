using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Transform), true)]
public class NewTransformInspector : Editor
{
	static public NewTransformInspector instance;

	SerializedProperty mPos;
	SerializedProperty mRot;
	SerializedProperty mScale;

	void OnEnable ()
	{
		instance = this;

		mPos = serializedObject.FindProperty("m_LocalPosition");
		mRot = serializedObject.FindProperty("m_LocalRotation");
		mScale = serializedObject.FindProperty("m_LocalScale");
	}

	void OnDestroy () { instance = null; }

	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.labelWidth = 15f;

		serializedObject.Update();

		DrawPosition();
		DrawRotation();
		DrawScale();

		serializedObject.ApplyModifiedProperties();
	}

	void DrawPosition ()
	{
		GUILayout.BeginHorizontal();
		{
			bool reset = GUILayout.Button("P", GUILayout.Width(20f));

			EditorGUILayout.PropertyField(mPos.FindPropertyRelative("x"));
			EditorGUILayout.PropertyField(mPos.FindPropertyRelative("y"));
			EditorGUILayout.PropertyField(mPos.FindPropertyRelative("z"));

			if (reset) mPos.vector3Value = Vector3.zero;
		}
		GUILayout.EndHorizontal();
	}

	void DrawScale ()
	{
		GUILayout.BeginHorizontal();
		{
			bool reset = GUILayout.Button("S", GUILayout.Width(20f));

			EditorGUILayout.PropertyField(mScale.FindPropertyRelative("x"));
			EditorGUILayout.PropertyField(mScale.FindPropertyRelative("y"));
			EditorGUILayout.PropertyField(mScale.FindPropertyRelative("z"));

			if (reset) mScale.vector3Value = Vector3.one;
		}
		GUILayout.EndHorizontal();
	}
	
	// Rotations

	enum Axes : int
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 4,
		All = 7,
	}

	Axes CheckDifference (Transform t, Vector3 original)
	{
		Vector3 next = t.localEulerAngles;

		Axes axes = Axes.None;

		if (Differs(next.x, original.x)) axes |= Axes.X;
		if (Differs(next.y, original.y)) axes |= Axes.Y;
		if (Differs(next.z, original.z)) axes |= Axes.Z;

		return axes;
	}

	Axes CheckDifference (SerializedProperty property)
	{
		Axes axes = Axes.None;

		if (property.hasMultipleDifferentValues)
		{
			Vector3 original = property.quaternionValue.eulerAngles;

			foreach (Object obj in serializedObject.targetObjects)
			{
				axes |= CheckDifference(obj as Transform, original);
				if (axes == Axes.All) break;
			}
		}
		return axes;
	}

	static bool FloatField (string name, ref float value, bool hidden, GUILayoutOption opt)
	{
		float newValue = value;
		GUI.changed = false;

		var style = new GUIStyle(GUI.skin.textField);
		var offset = style.margin;
		offset.left = 0;
		style.margin = offset;

		if (!hidden)
		{
			newValue = EditorGUILayout.FloatField(name, newValue, style, opt);
		}
		else
		{
			GUI.contentColor = new Color(.7f,.7f,.7f);
			float.TryParse(EditorGUILayout.TextField(name, "\u2014", style, opt), out newValue);
			GUI.contentColor = Color.white;
		}

		if (GUI.changed && Differs(newValue, value))
		{
			value = newValue;
			return true;
		}
		return false;
	}

	/// Because Mathf.Approximately is too sensitive.
	static bool Differs (float a, float b) { return Mathf.Abs(a - b) > 0.0001f; }

	// Because -360
	static float WrapAngle (float angle)
	{
		while (angle > 180f) angle -= 360f;
		while (angle < -180f) angle += 360f;
		return angle;
	}

	void DrawRotation ()
	{
		var style = new GUIStyle();
		style.padding = new RectOffset(0,0,4,0);

		EditorGUILayout.BeginHorizontal();
		{
			float snapSize = EditorPrefs.GetFloat("RotationSnap");
			bool reset = GUILayout.Button("R", GUILayout.Width(20f));

			Vector3 visible = (serializedObject.targetObject as Transform).localEulerAngles;

			visible.x = WrapAngle(visible.x);
			visible.y = WrapAngle(visible.y);
			visible.z = WrapAngle(visible.z);

			Axes changed = CheckDifference(mRot);
			Axes altered = Axes.None;

			GUILayoutOption opt = GUILayout.MinWidth(30f);

			EditorGUILayout.BeginVertical(style);
			{
				if (FloatField("X", ref visible.x, (changed & Axes.X) != 0, opt)) altered |= Axes.X;

				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Space(15f);
					if (GUILayout.Button("\u2191", GUILayout.Width(25f)))
						Selection.activeTransform.Rotate(new Vector3(-snapSize,0f,0f));
					if (GUILayout.Button("\u2193", GUILayout.Width(25f)))
						Selection.activeTransform.Rotate(new Vector3(snapSize,0f,0f));
				}
				EditorGUILayout.EndHorizontal();

			}
			EditorGUILayout.EndVertical();

			EditorGUILayout.BeginVertical(style);
			{
				if (FloatField("Y", ref visible.y, (changed & Axes.Y) != 0, opt)) altered |= Axes.Y;

				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Space(15f);
					if (GUILayout.Button("\u2190", GUILayout.Width(25f)))
						Selection.activeTransform.Rotate(new Vector3(0f,-snapSize,0f));
					if (GUILayout.Button("\u2192", GUILayout.Width(25f)))
						Selection.activeTransform.Rotate(new Vector3(0f,snapSize,0f));
				}
				EditorGUILayout.EndHorizontal();
				
			}
			EditorGUILayout.EndVertical();

			EditorGUILayout.BeginVertical(style);
			{
				if (FloatField("Z", ref visible.z, (changed & Axes.Z) != 0, opt)) altered |= Axes.Z;

				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Space(15f);
					if (GUILayout.Button("\u21BB", GUILayout.Width(25f)))
						Selection.activeTransform.Rotate(new Vector3(0f,0f,-snapSize));
					if (GUILayout.Button("\u21BA", GUILayout.Width(25f)))
						Selection.activeTransform.Rotate(new Vector3(0f,0f,snapSize));
				}
				EditorGUILayout.EndHorizontal();
				
			}
			EditorGUILayout.EndVertical();

			if (reset)
			{
				mRot.quaternionValue = Quaternion.identity;
			}
			else if (altered != Axes.None)
			{
				UnityEditor.Undo.RecordObjects(serializedObject.targetObjects, "Change Rotation");

				foreach (Object obj in serializedObject.targetObjects)
				{
					Transform t = obj as Transform;
					Vector3 v = t.localEulerAngles;

					if ((altered & Axes.X) != 0) v.x = visible.x;
					if ((altered & Axes.Y) != 0) v.y = visible.y;
					if ((altered & Axes.Z) != 0) v.z = visible.z;

					t.localEulerAngles = v;
					EditorUtility.SetDirty(obj);
				}
			}
		}
		EditorGUILayout.EndHorizontal();
	}
}

