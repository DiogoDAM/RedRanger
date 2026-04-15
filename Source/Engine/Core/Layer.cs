using System;

namespace SapukayaEngine;

public abstract class Layer : IDisposable
{
	public bool Active;
	public bool Visible;
	public bool Running;

	public bool Disposed { get; private set; }

	public bool CanUpdate => Active && Running && !Disposed;
	public bool CanDraw => Visible && Running && !Disposed;

	public Layer()
	{
	}

	public virtual void Activate() 
	{
		Active = true;
		Visible = true;
		Running = true;
	}

	public virtual void Disable()
	{
		Active = false;
		Visible = false;
		Running = false;
	}

	public virtual void Update(float dt) { }

	public virtual void Draw() { }


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
