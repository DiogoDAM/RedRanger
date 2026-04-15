using System;

namespace SapukayaEngine;

public abstract class Component : IDisposable
{
	public bool Active = true;

	public bool Disposed { get; private set; }
	
	public bool CanUpdate => Active && !Disposed;

	public Entity Entity { get; private set; }

	public Component() 
	{
	}

	public virtual void Attached(Entity entity)
	{
		if(entity == null)
			throw new ArgumentNullException("entity is null");

		Entity = entity;
	}

	public virtual void Distach()
	{
		Entity = null;
	}

	public virtual void Start()
	{
	}

	public virtual void Update(float dt)
	{
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
		if(disposable)
			return;
	}
}
