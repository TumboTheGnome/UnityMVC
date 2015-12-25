using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class App : Controller {

	public static App Self;

	protected override void Awake ()
	{
		if (Self == null) {
			Self = this;
			base.Awake ();
			Active = true;
		} else {
			Destroy(gameObject);
		}
	}
}
