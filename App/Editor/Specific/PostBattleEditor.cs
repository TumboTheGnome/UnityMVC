using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PostBattleController))]
public class PostBattleEditor : ControllerEditor {

	public override void OnInspectorGUI ()
	{
		extra = ()=>{
			
			GUILayout.TextField("Stuff");
			
		};
		
		base.OnInspectorGUI ();
	}
}
