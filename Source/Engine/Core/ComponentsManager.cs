using System;
using System.Collections.Generic;

namespace SapukayaEngine;

public sealed class ComponentsManager : IDisposable
{
	public Dictionary<Type, Component> Components { get; private set; }
	private List<DrawableComponent> _drawables;

	private Entity _entity;

	public ComponentsManager(Entity e)
	{
		Components = new();
		_drawables = new();
		_entity = e;
	}

	public void Add<T>(T component) where T : Component
	{
		Components.Add(typeof(T), component);

		component.Attached(_entity);
		component.Start();

		if(component is DrawableComponent dc)
		{
			_drawables.Add(dc);
		}
	}

	public void Remove<T>(bool toDispose=false) where T : Component
	{
		Type type = typeof(T);

		if(!Components.ContainsKey(type))
			return;

		T component = (T)Components[type];
		Components.Remove(type);

		if(typeof(DrawableComponent).IsAssignableFrom(typeof(T)))
		{
			_drawables.Remove(_drawables.Find(c => c is T));
		}

		component.Distach();
		if(toDispose)
			component.Dispose();
	}

	public T Get<T>() where T : Component
	{
		return (T)Components[typeof(T)];
	}

	public void Clear()
	{
		foreach(var component in Components.Values)
		{
			component.Distach();
			component.Dispose();
		}
		Components.Clear();
	}

	public void Update(float dt)
	{
		foreach(var component in Components.Values)
		{
			if(component.CanUpdate)
				component.Update(dt);
		}
	}

	public void Draw()
	{
		foreach(var component in _drawables)
		{
			if(component.CanDraw)
				component.Draw();
		}
	}

	public void Dispose()
	{
		foreach(var component in Components.Values)
		{
			component.Distach();
			component.Dispose();
		}
		Components.Clear();

		Components = null;

		_entity = null;
	}
}
