using System;
using System.Collections;
using System.Collections.Generic;

namespace SapukayaEngine;

public sealed class EntitiesManager : IDisposable, IEnumerable, IEnumerable<Entity>
{
	private List<Entity> _entities;
	private HashSet<Entity> _toAdd;
	private HashSet<Entity> _toRemove;

	private Dictionary<Type, IList> _cachedLists;

	public int Count => _entities.Count;

	private readonly Scene _scene;

	public EntitiesManager(Scene scene)
	{
		_scene = scene;

		_entities = new();
		_toAdd = new();
		_toRemove = new();

		_cachedLists = new();
	}

	public List<T> GetList<T>()
	{
		Type type = typeof(T);

		if(!_cachedLists.TryGetValue(type, out var outList))
		{
			var list = new List<T>();

			foreach(var e in _entities)
			{
				if(e is T et)
					list.Add(et);
			}

			_cachedLists.Add(type, list);

			return list;
		}

		return (List<T>)outList;
	}

	public T Get<T>() where T : Entity
	{
		foreach(var entity in _entities)
		{
			if(entity is T)
				return (T)entity;
		}

		return null;
	}

	public void Add(Entity e)
	{
		if(_toAdd.Contains(e) || _entities.Contains(e))
			return;

		_toAdd.Add(e);
	}

	public bool Remove(Entity e)
	{
		if(_toAdd.Contains(e))
		{
			_toAdd.Remove(e);
			return true;
		}

		if(_entities.Contains(e))
		{
			_toRemove.Add(e);
			return true;
		}

		return false;
	}

	public void Update(float dt)
	{
		foreach(Entity entity in _entities)
		{
			if(entity.CanUpdate)
				entity.Update(dt);
		}

		UpdateList();
	}

	private void UpdateList()
	{
		if(_toAdd.Count > 0)
		{
			foreach(Entity e in _toAdd)
			{
				_entities.Add(e);
				e.Scene = _scene;
				e.Added();
				e.Start();
				e.Resume();

				foreach(var pair in _cachedLists)
				{
					if(pair.Key.IsAssignableFrom(e.GetType()))
					{
						pair.Value.Add(e);
					}
				}

			}
			_toAdd.Clear();
		}
		if(_toRemove.Count > 0)
		{
			foreach(Entity e in _toRemove)
			{
				_entities.Remove(e);
				e.Scene = null;
				e.Removed();

				foreach(var pair in _cachedLists)
				{
					if(pair.Key.IsAssignableFrom(e.GetType()))
					{
						pair.Value.Remove(e);
					}
				}

				if(e.ToDestroy)
					e.Dispose();

			}
			_toRemove.Clear();
		}
	}

	public void Clear()
	{
		_toAdd.Clear();
		_toRemove.Clear();
		_cachedLists.Clear();

		foreach(Entity entity in _entities)
		{
			entity.Removed();
			if(entity.ToDestroy)
				entity.Dispose();
		}
		_entities.Clear();
	}

	public void Dispose()
	{
		_toAdd.Clear();
		_toRemove.Clear();
		_cachedLists.Clear();

		foreach(Entity entity in _entities)
		{
			entity.Removed();
			entity.Destroy();
			entity.Dispose();
		}
		_entities.Clear();

		_toAdd = null;
		_toRemove = null;
		_cachedLists = null;
		_entities = null;
	}

    public IEnumerator GetEnumerator()
    {
		return _entities.GetEnumerator();
    }

    IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator()
    {
		return _entities.GetEnumerator();
    }
}

