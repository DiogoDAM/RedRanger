using System;
using Microsoft.Xna.Framework;
using SapukayaEngine;

namespace RedRanger;

public class Projectile : Entity
{
	public float Speed { get; private set; }
	public int Direction { get; private set; }

	private readonly float _time;

	public Projectile(float speed, int direction, float time) : base()
	{
		Speed = speed;
		Direction = Math.Sign(direction);
		_time = time;
	}

    public override void Added()
    {
		var animation = new Animation(Globals.GameAtlas.CreateHSprite("player_projectile"), 0.05f, true);
		Add<Animation>(animation);
		animation.Stop();
		animation.SetOrigin(OriginPosition.LeftCenter);

		AddCollider<BoxCollider>(new(12, 4, Transform));
		Collider.Transform.LocalPosition = Vector2.One;

		Add<ProjectileMovement>(new(Speed, Direction));

		Timer timer = new(_time);
		timer.TimeUp += OnTimeUp;
		Add<Timer>(timer);
    }

	private void OnTimeUp()
	{
		Destroy();
	}

	public override void OnCollide(Entity other)
	{
		Explosion explosion = new(Transform.GlobalPosition, ExplosionType.Small);
		Scene.Add(explosion);

		Destroy();
	}
}
