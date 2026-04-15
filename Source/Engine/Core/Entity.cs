using System;
using System.Collections.Generic;

namespace SapukayaEngine;

public class Entity : IDisposable
{
	public bool Active;
	public bool Visible;
	public bool Alive;

	public bool Disposed { get; private set; }
	public bool ToDestroy { get; private set; }

	public bool CanUpdate => Active && Alive && !Disposed;
	public bool CanDraw => Visible && Alive && !Disposed;

	public Transform Transform;

	private ComponentsManager _components;

	public Entity()
	{
		Transform = new();

		_components = new(this);
	}

	public void Add<T>(T component) where T : Component
	{
		_components.Add(component);
	}

	public void Remove<T>(bool toDispose=false) where T : Component 
	{
		_components.Remove<T>(toDispose);
	}

	public bool Contains<T>() where T : Component
	{
		return _components.Components.ContainsKey(typeof(T));
	}

	public T Get<T>() where T : Component
	{
		return (T)_components.Components[typeof(T)];
	}

	public bool TryGet<T>(out T outComponent) where T : Component 
	{
		if(_components.Components.ContainsKey(typeof(T)))
		{
			outComponent = (T)_components.Components[typeof(T)];
			return true;
		}

		outComponent = null;
		return false;
	}

	public void Clear()
	{
		_components.Clear();
	}

	public virtual void Added()
	{
	}

	public virtual void Removed()
	{
	}

	public virtual void Start()
	{
	}

	public virtual void Update(float dt)
	{
		_components.Update(dt);
	}

	public virtual void Draw()
	{
		_components.Draw();
	}

	public virtual void Resume()
	{
		Active = true;
		Visible = true;
		Alive = true;
	}

	public virtual void Pause()
	{
		Active = false;
		Visible = false;
		Alive = false;
	}

	public void Destroy()
	{
		ToDestroy = true;
		Pause();
	}

	public void Dispose()
	{
		if(!Disposed)
		{
			Dispose(true);
			Disposed = true;
			GC.SuppressFinalize(this);
		}
	}

	protected virtual void Dispose(bool disposable)
	{
		if(!disposable)
			return;

		Transform.Parent = null;

		Transform = null;

		_components.Dispose();

		_components = null;
	}

}
