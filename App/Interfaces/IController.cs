using UnityEngine;
using System;
using System.Collections;

public interface IController{
	void RegisterSubState<T>(T controller) where T:IController; 
	void ChangeSubState(Type state);
	bool Active{ get; set; }
	IController SubState{ get; }
	IController Parent{ get; set; }
	ModelEvents this [Type model]{ get; }
	object Value<T> (string fieldName) where T:IModel;
}

