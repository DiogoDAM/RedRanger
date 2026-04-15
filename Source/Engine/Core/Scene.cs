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

	private List<Entity> _entities;

	public Scene()
	{
		_entities = new();
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

		e.Added();
		e.Start();
		e.Resume();
	}

	public void Remove(Entity e)
	{
		if(e == null)
			throw new ArgumentNullException("e Entity is null");

		var entity = _entities.Find(i => i == e);

		_entities.Remove(entity);

		entity.Removed();
	}

	public virtual void Start() { }

	public virtual void Update(float dt) 
	{
		List<Entity> toRemove = new();

		for(int i=0; i<_entities.Count; i++)
		{
			var entity = _entities[i];

			if(entity.CanUpdate)
				entity.Update(dt);

			if(entity.ToDestroy)
			{
				toRemove.Add(entity);
			}
		}

		foreach(var entity in toRemove)
		{
			_entities.Remove(entity);
			entity.Removed();
			entity.Dispose();
		}
	}

	public virtual void Draw() 
	{
		foreach(var entity in _entities)
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
