using SapukayaEngine;

using Microsoft.Xna.Framework;

namespace RedRanger;

public sealed class EnemyBasicMovement : Component
{
	public float Speed;

	public EnemyBasicMovement(float speed)
	{
		Speed = speed;
	}

    public override void Update(float dt)
    {
		Entity.Transform.LocalPosition.X += -1 * Speed * dt;
    }
}
