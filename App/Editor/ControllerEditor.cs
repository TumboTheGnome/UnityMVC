using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public delegate void MoreContent();

[CustomEditor(typeof(Controller))]
public class ControllerEditor : Editor {

	protected MoreContent extra;

	public override void OnInspectorGUI ()
	{
		Controller c = (Controller)target;

		if (c.Config.Count > 0) {
		
			List<string> names = new List<string>();

			foreach(Controller sub in c.Config)
			{
				if(sub != null)
				{
				names.Add(sub.GetType().ToString());
				}else{
					names.Add("");
				}
			}

			if(c.Default < 0 || c.Default >= c.Config.Count)
			{
				c.Default = 0;
			}


			c.Default = EditorGUILayout.Popup("Default:", c.Default, names.ToArray());
		}


		if (GUILayout.Button ("Add Controller")) {
			c.Config.Add(null);
		}

		for (int i = 0; i< c.Config.Count; i++) {
			EditorGUILayout.BeginHorizontal();
			c.Config[i] = (Controller)EditorGUILayout.ObjectField(c.Config[i],typeof(Controller), true);
			if(GUILayout.Button("x"))
			{
				c.Config.RemoveAt(i);
				i--;
			}
			EditorGUILayout.EndHorizontal();
		}

		if (GUILayout.Button ("Add View")) {
			c.Views.Add (null);
		}

		for (int i = 0; i < c.Views.Count; i++) {
		
			EditorGUILayout.BeginHorizontal ();

			c.Views [i] = (View)EditorGUILayout.ObjectField (c.Views [i], typeof(View), true);
			if (GUILayout.Button ("X")) {
				c.Views.RemoveAt (i);
				i--;
			}
			EditorGUILayout.EndHorizontal ();
		}

		if (extra != null) {
			extra();
		}
	}
}
