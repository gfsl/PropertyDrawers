using UnityEngine;
using System.Collections;

// [ExecuteInEditMode]
public class Examples : MonoBehaviour {

	[DynamicRange]
	public int health = 1;
	[HideInInspector]
	public int health_min = 0;
	[HideInInspector]
	public float health_max = 10.0f;

	[Popup("Test1", "Test2", "Test3")]
	public string TestList;

  [Compact]
  public Quaternion TestQuaternion = Quaternion.identity;

	[Compact]
	public Vector2 TestVector2 = Vector2.up;
	
	[Compact]
	public Vector3 TestVector3 = Vector3.up;
	
	[Regex (@"^(?:\d{1,3}\.){3}\d{1,3}$", "Invalid IP address!\nExample: '127.0.0.1'")]
	public string serverAddress = "192.168.0.1";
	
	public void Update () {
	}
}
