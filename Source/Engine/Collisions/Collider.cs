using System;

namespace SapukayaEngine;

public abstract class Collider : Component
{
	public Transform Transform { get; private set; }

	public bool CanCollide;

	public Collider()
	{
		Transform = new();
	}

    public override void Attached(Entity entity)
    {
        base.Attached(entity);

		CanCollide = true;
		Transform.Parent = entity.Transform;
    }

	public override void Distach()
	{
		base.Distach();

		CanCollide = false;
	}

	public bool Intersects(Collider col)
	{
		switch(col)
		{
			case BoxCollider box: return this.Intersects(box);
			default: throw new NotImplementedException("Collisor type is unknown");
		}
	}

	public abstract bool Intersects(BoxCollider other);

	protected override void Dispose(bool disposable)
	{
		base.Dispose(disposable);

		Transform.Parent = null;
		Transform = null;
	}
}
