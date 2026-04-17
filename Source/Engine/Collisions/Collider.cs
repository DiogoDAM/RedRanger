using System;

namespace SapukayaEngine;

public abstract class Collider : IDisposable
{
	public Transform Transform { get; private set; }

	public bool CanCollide;

	public string Tag;

	public bool IsTrigger = true;

	public Entity Entity { get; private set; }

	public Collider()
	{
		Transform = new();
	}

    public void Attached(Entity entity)
    {
		Entity = entity;

		CanCollide = true;
		Transform.Parent = entity.Transform;
    }

	public void Distach()
	{
		Entity = null;

		CanCollide = false;
	}

	public bool Intersects(Collider col)
	{
		if(ReferenceEquals(this, col))
			return false;

		if(!col.CanCollide || !CanCollide)
			return false;

		switch(col)
		{
			case BoxCollider box: return this.Intersects(box);
			case CircleCollider circle: return this.Intersects(circle);
			default: throw new NotImplementedException("Collisor type is unknown");
		}
	}

	public abstract bool Intersects(BoxCollider other);
	public abstract bool Intersects(CircleCollider other);

	public void Dispose()
	{
		Transform.Parent = null;
		Transform = null;

		GC.SuppressFinalize(this);
	}
}
