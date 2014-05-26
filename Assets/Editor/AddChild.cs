using UnityEngine;
using UnityEditor;
using System.Collections;

public class AddChild : ScriptableObject
{
	const string menuTitle = "GameObject/Add Child*";
	const int menuId = 31;

	[MenuItem (menuTitle,false,menuId)]
	static void MenuAddChild()
	{
		Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);
		
		foreach(Transform transform in transforms)
		{

			GameObject newChild = new GameObject(transform.gameObject.name + "_child");
			Undo.RegisterCreatedObjectUndo(newChild, "Added Child");
			newChild.transform.parent = transform;
			newChild.transform.localPosition = Vector3.zero;
			newChild.transform.localRotation = Quaternion.identity;
			newChild.transform.localScale = Vector3.zero;
			Selection.activeGameObject = newChild;
		}
	}

	[MenuItem (menuTitle,true,menuId)]
	static bool ValidateMenuAddChild() {
		return Selection.activeGameObject != null;
	}
}
