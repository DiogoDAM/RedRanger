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

		var box = new BoxCollider(12, 4, Transform);
		AddCollider(box);
		box.Transform.LocalPosition = Vector2.One;

		Add<ProjectileMovement>(new(Speed, Direction));

		Timer timer = new(_time);
		timer.TimeUp += OnTimeUp;
		Add<Timer>(timer);
    }

	private void OnTimeUp()
	{
		Destroy();
	}

	public override void OnTrigger(Entity other)
	{
		Explosion explosion = new(Transform.GlobalPosition, ExplosionType.Small);
		Scene.Add(explosion);

		Destroy();
	}
}
