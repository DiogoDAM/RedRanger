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
		Add<Sprite>(Globals.GameAtlas.CreateSprite("player_projectile"));
		Get<Sprite>().SetOrigin(OriginPosition.LeftCenter);

		Add<ProjectileMovement>(new(Speed, Direction));

		Timer timer = new(_time);
		timer.TimeUp += OnTimeUp;
		Add<Timer>(timer);
    }

	private void OnTimeUp()
	{
		Destroy();
	}
}
