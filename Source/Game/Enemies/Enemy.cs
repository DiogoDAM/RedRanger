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

		Layers = GameLayers.Enemy;
		Masks = GameLayers.PlayerProjectile + GameLayers.Player;
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
		if(other is Player player)
		{
			Destroy();

			Explosion explosion = new(Transform.GlobalPosition, ExplosionType.Big);
			Scene.Add(explosion);

			return;
		}

		Health -= 1;
		if(Health == 0)
		{
			Explosion explosion = new(Transform.GlobalPosition, ExplosionType.Big);
			Scene.Add(explosion);
			Destroy();
		}
	}
}
