using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace SapukayaEngine;

public abstract class UiElement : IDisposable
{
	public bool Active;
	public bool Visible;
	public bool Alive;

	public bool Disposed { get; private set; }

	public bool CanUpdate => Active && Alive && !Disposed;
	public bool CanDraw => Visible && Alive && !Disposed;

	public List<UiElement> Children { get; protected set; }

	public UiElement Parent { get; protected set; }

	public Transform Transform { get; protected set; }

	public virtual int Width { get; protected set; }
	public virtual int Height { get; protected set; }

	public UiElement()
	{
		Children = new();
		Transform = new();
	}

	public void AddChild(UiElement child)
	{
		if(child == null)
			throw new ArgumentNullException("UiElement child is null");

		Children.Add(child);
		child.Added(this);
		child.Resume();
		child.Start();
	}

	public void RemoveChild(UiElement child)
	{
		if(child == null)
			throw new ArgumentNullException("UiElement child is null");

		Children.Remove(child);
		child.Pause();
		child.Removed();
	}

	public virtual void Added(UiElement parent)
	{
		Parent = parent;
		Transform.Parent = parent.Transform;
	}

	public virtual void Removed()
	{
		Parent = null;
		Transform.Parent = null;
	}

	public virtual void Start()
	{
	}

	public virtual void Update(float dt)
	{
		foreach(UiElement child in Children)
		{
			if(child.CanUpdate)
				child.Update(dt);
		}
	}

	public virtual void Draw()
	{
		foreach(UiElement child in Children)
		{
			if(child.CanDraw)
				child.Draw();
		}
	}

	public virtual void Resume()
	{
		Active = true;
		Visible = true;
		Alive  = true;
	}

	public virtual void Pause()
	{
		Active = false;
		Visible = false;
		Alive = false;
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

		foreach(UiElement child in Children)
		{
			child.Dispose();
		}
		Children.Clear();

		Children = null;

		Transform = null;
	}
}
