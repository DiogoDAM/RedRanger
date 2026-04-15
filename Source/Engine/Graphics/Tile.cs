using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public sealed class Tile
{
	public Rectangle SourceRectangle;
	public Color Color = Color.White;
	public Vector2 Position;

	public Tile(Vector2 position, Rectangle srcRect)
	{
		Position = position;
		SourceRectangle = srcRect;
	}
}
