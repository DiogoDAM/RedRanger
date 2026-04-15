using Microsoft.Xna.Framework;
using SapukayaEngine;

namespace RedRanger;

public sealed class Enemy : Entity
{
	public Sprite Sprite;

	public float Speed = 120f;

	public Enemy(Vector2 pos) : base()
	{
		Transform.LocalPosition = pos;
	}

	public override void Added()
	{
		Add<EnemyBasicMovement>(new(Speed));

		Add<Sprite>(Sprite);
	}

    public override void Update(float dt)
    {
        base.Update(dt);

		if(Transform.GlobalPosition.X <= -100f)
			Destroy();
    }
}
