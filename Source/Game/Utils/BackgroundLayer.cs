using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SapukayaEngine;

namespace RedRanger;

public sealed class BackgroundLayer
{
	public readonly Texture2D Texture;

	public Rectangle SourceRectangle { get; private set; }

	public int SpeedX;

	public int X { get; private set; }

	public readonly int Width;
	public readonly int Height;

	public BackgroundLayer(string texturePath, int width, int height, int speed)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);
		X = 0;
		SourceRectangle = new Rectangle(X, 0, width, height);
		Width = width;
		Height = height;
		SpeedX = speed;
	}

	public void Update(float dt)
	{
		X += (int)(SpeedX * dt);

		SourceRectangle = new Rectangle(X, 0, Width, Height);
	}

	public void Draw()
	{
		Engine.SpriteBatch.Draw(Texture,
				Vector2.Zero,
				SourceRectangle,
				Color.White);
	}
} 
