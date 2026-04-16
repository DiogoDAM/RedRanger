using System;
using System.Collections.Generic;

namespace SapukayaEngine;

public abstract class Scene : IDisposable
{
	public bool Active;
	public bool Visible;
	public bool Running;

	public bool Disposed { get; private set; }

	public bool CanUpdate => Active && Running && !Disposed;
	public bool CanDraw => Visible && Running && !Disposed;

	protected EntitiesManager _entities;

	public int EntitiesCount => _entities.Count;

	public Scene()
	{
		_entities = new(this);
	}

	public void Resume()
	{
		Active = true;
		Visible = true;
		Running = true;
	}

	public void Pause()
	{
		Active = false;
		Visible = false;
		Running = false;
	}

	public virtual void Disable() 
	{
		Pause();
	}

	public virtual void Activate() 
	{
		Resume();
	}

	public void Add(Entity e)
	{
		if(e == null)
			throw new ArgumentNullException("e Entity is null");

		_entities.Add(e);
	}

	public bool Remove(Entity e)
	{
		if(e == null)
			throw new ArgumentNullException("e Entity is null");

		return _entities.Remove(e);
	}

	public virtual void Start() { }

	public virtual void Update(float dt) 
	{
		_entities.Update(dt);
	}

	public virtual void Draw() 
	{
		foreach(Entity entity in _entities)
		{
			if(entity.CanDraw)
				entity.Draw();
		}
	}


	public void Dispose()
	{
		Dispose(true);
		Disposed = true;
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposable)
	{
		if(disposable)
			return;
	}
}
