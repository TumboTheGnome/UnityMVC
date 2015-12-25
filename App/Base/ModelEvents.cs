using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void ModelUpdateEvent(object value);
public class ModelEvents{

	private Dictionary<string, ModelUpdateEvent> _events = new Dictionary<string, ModelUpdateEvent>();

	public void Bind(string name, ModelUpdateEvent action)
	{
		if (!_events.ContainsKey (name)) {
			_events.Add(name, null);
		}

		_events [name] = _events [name] + action;
	}

	public void Unbind(string name, ModelUpdateEvent action)
	{
		if (_events.ContainsKey (name)) {
			_events[name] = _events[name] - action;
		}
	}

	public void Trigger(string name, object value)
	{
		if (_events.ContainsKey (name)) {
			if(_events[name] != null)
			{
				_events[name](value);
			}
		}
	}
}
