using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Controller :MonoBehaviour, IController {
	public List<View> Views = new List<View> ();
	public int Default = -1;
	public List<Controller> Config = new List<Controller>();

	bool _active;
	Dictionary<Type, IController> _states = new Dictionary<Type, IController>();
	IController _current;
	IController _parent;


	Dictionary<Type, IModel> _models = new Dictionary<Type, IModel>();


	protected virtual void Awake()
	{
		if (Views.Count > 0) {
			foreach (View view in Views) {
				view.gameObject.SetActive (false);
			}
		}

		if (Config != null && Config.Count > 0) {
			foreach(Controller c in Config)
			{
				RegisterSubState(c);
			}

			if(Default >= 0 && Default < Config.Count)
			{
				ChangeSubState(Config[Default].GetType());
			}
		}
	}

	public object Value<T>(string fieldName) where T:IModel{
		if (_models.ContainsKey (typeof(T))) {
			return _models [typeof(T)].GetValue (fieldName);
		} else {
			return null;
		}
	}

	public ModelEvents this [Type model] {
		get {
			if (_models.ContainsKey (model)) {
				return _models [model].Events;
			} else {
				return null;
			}
		}
	}

	public IController Parent {
		get {
			return _parent;
		}

		set{
			_parent = value;
		}
	}

	public IController SubState{
		get{
			return _current;
		}
	}

	public bool Active{
		get{
			return _active;
		}

		set{

			if (value && !_active) {
				OnActivate ();
			} else {
				OnDeactivate ();
			}

			_active = value;
		}
	}

	public void RegisterSubState<T> (T controller) where T : IController
	{
		if (controller != null) {
			Type type = typeof(T);

			if (!_states.ContainsKey (type)) {
				controller.Parent = this;
				_states.Add (type, controller);
			} else {
				Debug.LogWarning ("Duplicate controller. Controller instances must be unique.");
			}
		}
	}

	public void ChangeSubState (System.Type state)
	{
		if (_states.ContainsKey (state)) {

			_current.Active = false;
			_states[state].Active = true;
			_current = _states[state];
		}
	}

	protected bool registerModel<T>(T model) where T:IModel
	{
		if (!_models.ContainsKey (typeof(T))) {
			_models.Add (typeof(T), model);
			return true;
		} else {
			return false;
		}
	}

	protected T getModel<T>(T name) where T:IModel{
		if(_models.ContainsKey(typeof(T))){
			return (T)_models [typeof(T)];
		}else{
			return default(T);
		}
	}

	protected virtual void OnActivate(){
	}

	protected virtual void OnDeactivate(){
	}
}