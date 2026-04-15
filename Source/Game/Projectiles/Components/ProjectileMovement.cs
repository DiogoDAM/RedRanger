using Microsoft.Xna.Framework;

using System;

using SapukayaEngine;

namespace RedRanger;

public sealed class ProjectileMovement : Component
{
	private float _speed;
	private int _dir;

	public ProjectileMovement(float speed, int direction)
	{
		_speed = speed;
		_dir = direction;
	}

    public override void Update(float dt)
    {
		Entity.Transform.LocalPosition.X += _dir * _speed * dt;
    }

}
