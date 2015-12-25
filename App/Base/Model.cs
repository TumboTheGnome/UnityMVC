using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Model : IModel {

	IModel _parent = null;
	ModelEvents evnts = new ModelEvents();
	protected dataStore core = new dataStore();

	public Model()
	{
		evnts.Bind ("model-load", null);
		evnts.Bind ("model-save", null);

	}

	#region IModel implementation

	public IModel Parent{
		get{
			return _parent;
		}

		set{
			_parent = value;
		}
	}

	public void Register <T>(string name)
	{
		if (!core.Has(name)) {

			if(!typeof(T).IsSerializable)
			{
				Debug.LogWarning(typeof(T)+" is not serializable!");
			}

			core.Add( new dataField(name, typeof(T)));
		}
	}

	public void SetValue<T>(string name, T value)
	{
		dataField field = core.Get (name);
		if (field != null) {
		
			if(typeof(T) == field.Type)
			{
				field.Value = value;
				evnts.Trigger(name, field.Value);

				if (typeof(T).IsAssignableFrom (typeof(IModel))) {
					IModel model = (IModel)value;
					model.Parent = this;
				}
			}
		}
	}

	public object GetValue (string name)
	{
		dataField field = core.Get (name);

		if (field != null) {
			return field.Value;
		}

		return null;
	}

	public ModelEvents Events {
		get {
			return evnts;
		}
	}

	public virtual void Save ()
	{
		evnts.Trigger ("model-save", null);
	}

	public virtual void Load ()
	{
		evnts.Trigger ("model-load", null);
	}

	#endregion

	[System.Serializable]
	protected class dataField{
		private int _id;
		private string _name;
		private Type _type;
		private object _value;

		public dataField(string name, Type type)
		{
			if(name == null)
			{
				Debug.LogError("Cannot be null");
			}

			_id = name.GetHashCode();
			_type = type;
			_name = name;
		}

		public int Id{
			get{
				return _id;
			}
		}

		public string Name{
			get{
				return _name;
			}
		}

		public Type Type{
			get{
				return _type;
			}
		}

		public object Value{
			get{
				return _value;
			}

			set{
				_value = value;
			}
		}
	}

	[System.Serializable]
	protected class dataStore{
		List<dataField> _data = new List<dataField>();

		public bool Has(string name)
		{
			return _fetch (name) != null;
		}

		public dataField Get(string name)
		{
			return _fetch (name);
		}

		public void Add(dataField entry)
		{
			_data.Add (entry);
			_data = _data.OrderBy(x => x.Id).ToList();
		}

		private dataField _fetch(string name)
		{
			if (name != null && _data.Count > 0) {
				int index = _findIndex (0, _data.Count - 1, name.GetHashCode ());

				if (index != -1) {
					return _data [index];
				}
			}

			return null;
		}

		private int _findIndex(int start, int end, int id)
		{
			if (_data[start].Id == id) {
				return start;
			} else if (_data[end].Id == id) {
				return end;
			} else {
				int length = end - start;

				if (length + 1 <= 2) {
					return -1;
				}

				int mid = start + (length/2);
				int vMid = _data[mid].Id;
				if (vMid == id) {
					return mid;
				} else if (id > vMid) {
					return _findIndex(id, mid, end);
				} else {
					return _findIndex(id, start, mid);
				}
			}

		}
	}
}
