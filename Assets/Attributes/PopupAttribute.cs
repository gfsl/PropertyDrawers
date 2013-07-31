using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PopupAttribute : PropertyAttribute {

	public List<string> optionsList;

	public PopupAttribute(params string[] args) {
		this.optionsList = new List<string>(args);
	}

}
