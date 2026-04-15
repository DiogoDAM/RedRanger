using Microsoft.Xna.Framework;

using SapukayaEngine;

namespace RedRanger;

public sealed class PlayerShoot : Component
{
	public float TimeToShoot;

	private float _timeLeft;

	public PlayerShoot(float timeToShoot)
	{
		TimeToShoot = timeToShoot;
	}

	public void Shoot()
	{
		var proj = new Projectile(400f, 1, 2f);
		proj.Transform = new(Entity.Transform);
		proj.Transform.LocalPosition += new Vector2(40, 12);

		GameManager.Instance.GetLayer<GameLayer>().ActiveScene.Add(proj);
	}

    public override void Update(float dt)
    {
		if(Engine.Input.Mouse.IsButtonDown(0) && _timeLeft <= 0)
		{
			_timeLeft = TimeToShoot;
			Shoot();
		}

		_timeLeft -= dt;
    }
}
