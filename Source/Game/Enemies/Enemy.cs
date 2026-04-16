using Microsoft.Xna.Framework;
using SapukayaEngine;

namespace RedRanger;

public sealed class Enemy : Entity, ICollidable
{
	public Sprite Sprite;

	public float Speed = 120f;

	public int Layer { get; set; }
	public int Mask { get; set; }

	public Collisor Collisor { get; set; }

	public bool CanCollide { get; set; } = true;

	public int Health = 5;

	public Enemy(Vector2 pos) : base()
	{
		Transform.LocalPosition = pos;
	}

	public override void Added()
	{
		Add<EnemyBasicMovement>(new(Speed));

		Add<Sprite>(Sprite);
		Collisor = new(Sprite.Width, Sprite.Height, Transform);
	}

    public override void Update(float dt)
    {
        base.Update(dt);

		if(Transform.GlobalPosition.X <= -100f)
			Destroy();
    }

	public void OnCollide(Entity other)
	{
		Health -= 1;
		if(Health == 0)
			Destroy();
	}

	public bool Collides(ICollidable other)
	{
		return false;
	}
}
