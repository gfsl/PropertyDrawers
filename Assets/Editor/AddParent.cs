using UnityEngine;
using UnityEditor;
using System.Collections;

// TODO: Expand hierarchy after adding

public class AddParent : ScriptableObject
{
	[MenuItem ("GameObject/Add Parent")]
	static void MenuAddParent()
	{
		Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel |
		                                                 SelectionMode.OnlyUserModifiable);

		GameObject newParent = new GameObject("GameObject");
		Undo.RegisterCreatedObjectUndo(newParent, "Added Parent");

		Transform originalParent = transforms[0].parent;
		bool allSameParent = true;
		if (originalParent == null )
		{
			allSameParent = false;
		}
		else
		{
			foreach (var transform in transforms)
			{
				allSameParent = (originalParent == transform.parent);
				if (!allSameParent) break;
			}
		}

		foreach (var transform in transforms)
		{
			Undo.SetTransformParent(transform, newParent.transform, "Added Parent");
		}
		if (allSameParent)
		{
			Undo.SetTransformParent(newParent.transform, originalParent, "Added Parent");
		}

		Selection.activeGameObject = newParent;
	}

	[MenuItem ("GameObject/Add Parent", true)]
	static bool ValidateMenuAddParent() {
		return Selection.activeGameObject != null;
	}
}
