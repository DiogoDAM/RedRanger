using System;
using SapukayaEngine;

namespace RedRanger;

public class Projectile : Entity, ICollidable
{
	public float Speed { get; private set; }
	public int Direction { get; private set; }

	public int Layer { get; set; }
	public int Mask { get; set; }

	public Collisor Collisor { get; set; }
	public bool CanCollide { get; set; } = true;

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

		Collisor = new(sprite.Width, sprite.Height, Transform);

		Add<ProjectileMovement>(new(Speed, Direction));

		Timer timer = new(_time);
		timer.TimeUp += OnTimeUp;
		Add<Timer>(timer);
    }

	private void OnTimeUp()
	{
		Destroy();
	}

	public void OnCollide(Entity other)
	{
		Destroy();
		System.Console.WriteLine("Ai me destrui");
	}

	public bool Collides(ICollidable other)
	{
		if(!other.CanCollide || !this.CanCollide || this == other)
			return false;

		return other.Collisor.Shape.Intersects(Collisor.Shape);
	}
}
