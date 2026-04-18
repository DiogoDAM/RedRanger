using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace SapukayaEngine;

public sealed class UiLabel : UiElement 
{
	public readonly SpriteFont Font;

	public string Text { get; private set; }

	public override int Width => (int)Font.MeasureString(Text).X;
	public override int Height => (int)Font.MeasureString(Text).Y;

	public Color Color = Color.White;
	public Vector2 Origin;
	public SpriteEffects Flip;
	public float Depth;

	public UiLabel(SpriteFont font)
	{
		Font = font;
	}

	public UiLabel(SpriteFont font, string text)
	{
		Font = font;
		SetText(text);
	}

	public UiLabel(string fontPath)
	{
		Font = Engine.Content.Load<SpriteFont>(fontPath);
	}

	public UiLabel(string fontPath, string text)
	{
		Font = Engine.Content.Load<SpriteFont>(fontPath);
		SetText(text);
	}

	public void SetText(string text)
	{
		if(String.IsNullOrEmpty(text))
			throw new ArgumentNullException("text is null or empty");

		Text = text;
	}

	public override void Draw()
	{
		Engine.SpriteBatch.DrawString(Font,
				Text,
				Transform.GlobalPosition,
				Color,
				Transform.GlobalRotation,
				Origin,
				Transform.GlobalScale,
				Flip,
				Depth);
	}
}
