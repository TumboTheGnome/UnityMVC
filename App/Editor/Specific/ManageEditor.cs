using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ManageController))]
public class ManageEditor : ControllerEditor {

	public override void OnInspectorGUI ()
	{
		extra = ()=>{
			
			GUILayout.TextField("Stuff");
			
		};
		
		base.OnInspectorGUI ();
	}
}
