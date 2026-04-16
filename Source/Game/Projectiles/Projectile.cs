using System;
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
		var sprite = Globals.GameAtlas.CreateSprite("player_projectile");
		Add<Sprite>(sprite);
		sprite.SetOrigin(OriginPosition.LeftCenter);

		AddCollider<BoxCollider>(new(sprite.Width, sprite.Height, Transform));

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
		Destroy();
	}
}
