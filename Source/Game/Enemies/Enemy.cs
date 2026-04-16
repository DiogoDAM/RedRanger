using Microsoft.Xna.Framework;
using SapukayaEngine;

namespace RedRanger;

public sealed class Enemy : Entity
{
	public Sprite Sprite;

	public float Speed = 120f;

	public int Health = 5;

	public Enemy(Vector2 pos) : base()
	{
		Transform.LocalPosition = pos;
	}

	public override void Added()
	{
		Add<EnemyBasicMovement>(new(Speed));

		Add<Sprite>(Sprite);
		AddCollider<BoxCollider>(new (Sprite.Width, Sprite.Height, Transform));
	}

    public override void Update(float dt)
    {
        base.Update(dt);

		if(Transform.GlobalPosition.X <= -100f)
			Destroy();
    }

	public override void OnCollide(Entity other)
	{
		Health -= 1;
		if(Health == 0)
			Destroy();
	}
}
