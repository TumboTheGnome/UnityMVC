using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BattleController))]
public class BattleEditor : ControllerEditor {

	public override void OnInspectorGUI ()
	{
		extra = ()=>{
			
			GUILayout.TextField("Stuff");
			
		};
		
		base.OnInspectorGUI ();
	}
}
