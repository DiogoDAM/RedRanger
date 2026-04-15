using SapukayaEngine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RedRanger;

public sealed class PlayerMovement : Component 
{
	public float Speed;

	public PlayerMovement(float speed)
	{
		Speed = speed;
	}

    public override void Update(float dt)
    {
		Vector2 dir = Vector2.Zero;

		if(Engine.Input.Keyboard.IsKeyDown(Keys.D))
		{
			dir.X = 1;
		}
		else if(Engine.Input.Keyboard.IsKeyDown(Keys.A))
		{
			dir.X = -1;
		}
		if(Engine.Input.Keyboard.IsKeyDown(Keys.S))
		{
			dir.Y = 1;
		}
		else if(Engine.Input.Keyboard.IsKeyDown(Keys.W))
		{
			dir.Y = -1;
		}

		if(dir != Vector2.Zero)
		{
			Entity.Get<HSprite>().SetFrame(1);

			if(dir.Y == 1)
				Entity.Get<HSprite>().SetFrame(2);
			else if(dir.Y == -1)
				Entity.Get<HSprite>().SetFrame(3);

			dir.Normalize();

			Entity.Transform.LocalPosition += dir * Speed * dt;

		}
		else
		{
			Entity.Get<HSprite>().SetFrame(0);
		}
    }
}
