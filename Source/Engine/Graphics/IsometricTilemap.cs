using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public sealed class IsometricTilemap : Tilemap
{
	public IsometricTilemap(Texture2D texture, int[] data, int width, int height) : base(texture, data, width, height)
	{
		for(int y=0; y<Height; y++)
		{
			for(int x=0; x<Width; x++)
			{
				Vector2 tilePos = Utilities.GridToIsometricWorld(new Point(x, y));
				_tiles[x + y * Width] = new Tile(tilePos, _sourceRectangles[_data[x + y * Width]]);
			}
		}
	}

	public IsometricTilemap(string texturePath, int[] data, int width, int height) : base(texturePath, data, width, height)
	{
		for(int y=0; y<Height; y++)
		{
			for(int x=0; x<Width; x++)
			{
				Vector2 tilePos = Utilities.GridToIsometricWorld(new Point(x, y));
				_tiles[x + y * Width] = new Tile(tilePos, _sourceRectangles[_data[x + y * Width]]);
			}
		}
	}

	public override void Draw()
	{
		for(int y=0; y<Height; y++)
		{
			for(int x=0; x<Width; x++)
			{
				int index = _data[x + y * Width];

				if(index == 0)
					return;

				Tile tile = _tiles[x + y * Width];

				Engine.SpriteBatch.Draw(Texture,
						tile.Position + Position,
						tile.SourceRectangle,
						tile.Color,
						0f,
						Vector2.Zero,
						Vector2.One,
						SpriteEffects.None,
						0f);
			}
		}
	}
}
