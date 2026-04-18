using Microsoft.Xna.Framework;
using SapukayaEngine;

namespace RedRanger;

public sealed class Enemy : Entity
{
	public Sprite Sprite;

	public float Speed = 120f;

	public int Health = 5;

	public int Score { get; set; }

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
		var collider = new CircleCollider(8);
		collider.Transform.LocalPosition = new Vector2(9,9);
		AddCollider(collider);
	}

    public override void Update(float dt)
    {
        base.Update(dt);

		if(Transform.GlobalPosition.X <= -100f)
			Destroy();
    }

	public override void OnTrigger(Entity other)
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

			Globals.Score += Score;
		}
	}
}
