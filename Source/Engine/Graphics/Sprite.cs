using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public sealed class Sprite : DrawableComponent
{
	public Texture2D Texture;

	public Rectangle SourceRectangle;

	public override int Width { get { return SourceRectangle.Width; } }
	public override int Height { get { return SourceRectangle.Height; } }
	public int X => SourceRectangle.X;
	public int Y => SourceRectangle.Y;

	public Sprite(string texturePath)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);
		SourceRectangle = new(0, 0, Texture.Width, Texture.Height);
	}

	public Sprite(string texturePath, int w, int h)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);
		SourceRectangle = new(0, 0, w, h);
	}

	public Sprite(string texturePath, int x, int y, int w, int h)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);
		SourceRectangle = new(x, y, w, h);
	}

	public Sprite(string texturePath, Rectangle sourceRectangle)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);
		SourceRectangle = sourceRectangle;
	}

	public Sprite(Texture2D texture)
	{
		Texture = texture;
		SourceRectangle = new(0, 0, Texture.Width, Texture.Height);
	}

	public Sprite(Texture2D texture, int w, int h)
	{
		Texture = texture;
		SourceRectangle = new(0, 0, w, h);
	}

	public Sprite(Texture2D texture, int x, int y, int w, int h)
	{
		Texture = texture;
		SourceRectangle = new(x, y, w, h);
	}

	public Sprite(Texture2D texture, Rectangle sourceRectangle)
	{
		Texture = texture;
		SourceRectangle = sourceRectangle;
	}

    public override void Draw()
    {
		Engine.SpriteBatch.Draw(Texture,
				Entity.Transform.GlobalPosition,
				SourceRectangle,
				Color,
				Entity.Transform.GlobalRotation,
				Origin,
				Entity.Transform.GlobalScale,
				Flip,
				Depth);
    }
}
