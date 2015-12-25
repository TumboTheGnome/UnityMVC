using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(HomeController))]
public class HomeEditor : ControllerEditor {

	public override void OnInspectorGUI ()
	{
		extra = ()=>{
			
			GUILayout.TextField("Stuff");
			
		};
		
		base.OnInspectorGUI ();
	}
}
