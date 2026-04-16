using System;
using Microsoft.Xna.Framework;

namespace SapukayaEngine;

public sealed class Collisor : IDisposable
{
	public Transform Transform;

	public Rectangle Shape => new Rectangle((int)Transform.GlobalPosition.X, (int)Transform.GlobalPosition.Y, Width, Height);

	public int Width;
	public int Height;

	public Collisor(int w, int h, Transform parent)
	{
		Width = w;
		Height = h;
		Transform = new();
		Transform.Parent = parent;
	}

	public void Dispose()
	{
		Transform.Parent = null;
		Transform = null;

		GC.SuppressFinalize(this);
	}
}
