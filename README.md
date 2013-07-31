PropertyDrawers
===============

A collection of useful (maybe) PropertyDrawers for all to enjoy.

[Compact]
* Nicer Vector2, Vector3 and Quaternion drawers, similar to the default Transform inspector.

[DynamicRange]
* Like [Range], but pulls Min and Max from introspected VarName_min and VarName_max variables.
* Supports Floats and Ints.

[Popup("String1", "String2", ...]
* Creates an Enum-style dropdown list of Strings.

[Regex(regex, "Error text")]
* Validates a String input against a regexp.
* Lifted directly from here: http://blogs.unity3d.com/2012/09/07/property-drawers-in-unity-4/
