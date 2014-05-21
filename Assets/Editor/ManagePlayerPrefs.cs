using UnityEngine;
using UnityEditor;
using System.Collections;

public static class ManagePlayerPrefs {

	[MenuItem("Edit/Player Preferences/Clear All")]
	static void ClearMyPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}
}
