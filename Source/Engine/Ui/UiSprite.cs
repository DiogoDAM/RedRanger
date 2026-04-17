using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public class UiSprite : UiElement
{
	public readonly Texture2D Texture;

	public Rectangle SourceRectangle;

	public override int Width => SourceRectangle.Width;
	public override int Height => SourceRectangle.Height;

	public Color Color = Color.White;
	public Vector2 Origin;
	public SpriteEffects Flip;
	public float Depth;

	public UiSprite(Texture2D texture, int x, int y, int w, int h) : base()
	{
		Texture = texture;

		SourceRectangle = new(x, y, w, h);
	}

	public UiSprite(Texture2D texture, Rectangle srcRect) : base()
	{
		Texture = texture;

		SourceRectangle = srcRect;
	}

	public UiSprite(string texturePath, int x, int y, int w, int h) : base()
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);

		SourceRectangle = new(x, y, w, h);
	}

	public override void Draw()
	{
		Engine.SpriteBatch.Draw(Texture,
				Transform.GlobalPosition,
				SourceRectangle,
				Color,
				Transform.GlobalRotation,
				Origin,
				Transform.GlobalScale,
				Flip,
				Depth);
	}
}
