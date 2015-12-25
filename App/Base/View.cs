using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster))]
public class View : MonoBehaviour {

	public Controller Controller;

	protected virtual void Start(){

		if (Controller == null) {
			Debug.LogError ("View " + this.name + " does not have a controller assigned.");
		}
	}
}
