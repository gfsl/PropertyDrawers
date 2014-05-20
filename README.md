# Unity Project Template

## Custom Transform Inspector

Includes reset buttons, nicer wrapping of Euler angles, and snapping rotation buttons that (for me at least) also serve as a reminder of which axis means which rotation.

## AddChild/AddParent

Intelligently adds single or multiple children, or can group a set of GameObjects under a new parent GameObject, taking into account (as best it can) existing hierarchies.

## PropertyDrawers

Most of these are implemented directly from Unity's examples.

```C#
[Compact]
public Vector3 TestVector3 = Vector3.up;
```
* Nicer Vector2, Vector3 and Quaternion drawers, similar to the default Transform inspector.

```C#
[DynamicRange]
public int health = 1;
[HideInInspector]
public int health_min = 0;
[HideInInspector]
public float health_max = 10.0f;
```
* Like [Range], but pulls Min and Max from introspected VarName_min and VarName_max variables.
* Supports Floats and Ints.

```C#
[Popup("String1", "String2", ...]
public string TestList;
```
* Creates an Enum-style dropdown list of Strings.

```C#
[Regex (@"^(?:\d{1,3}\.){3}\d{1,3}$", "Invalid IP address!\nExample: '127.0.0.1'")]
public string serverAddress = "192.168.0.1";
```
* Validates a String input against a regexp.
* Lifted directly from here: http://blogs.unity3d.com/2012/09/07/property-drawers-in-unity-4/
