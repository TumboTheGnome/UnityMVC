using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SplashController))]
public class SplashEditor:ControllerEditor {
	public override void OnInspectorGUI ()
	{
		extra = ()=>{
			
			GUILayout.TextField("Stuff");
			
		};
		
		base.OnInspectorGUI ();
	}
}
