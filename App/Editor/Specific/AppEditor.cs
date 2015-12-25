using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(App))]
public class AppEditor : ControllerEditor {

	public override void OnInspectorGUI ()
	{
		extra = ()=>{

			GUILayout.TextField("Stuff");

		};

		base.OnInspectorGUI ();
	}
}
