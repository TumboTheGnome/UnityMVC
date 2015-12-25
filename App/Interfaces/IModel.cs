using UnityEngine;
using System;
using System.Collections;

public interface IModel{

	ModelEvents Events{ get; }
	void Register<T>(string name);
	void SetValue<T>(string name, T value);
	object GetValue(string name);
	void Save();
	void Load();
	IModel Parent{ get; set;}
}
