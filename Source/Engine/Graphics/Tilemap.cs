using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public abstract class Tilemap 
{
	public readonly Texture2D Texture;

	protected int[] _data;
	protected Rectangle[] _sourceRectangles;
	protected Tile[] _tiles;

	public int Width { get; private set; }
	public int Height { get; private set; }

	public readonly int TilesWidth;
	public readonly int TilesHeight;

	public Vector2 Position = Vector2.Zero;

	public Tilemap(Texture2D texture, int[] data, int width, int height)
	{
		Texture = texture;

		Width = width;
		Height = height;

		TilesWidth = Engine.TileSize;
		TilesHeight = Engine.TileSize;

		_data = data;
		_tiles = new Tile[Width * Height];

		int w = (Texture.Width/TilesWidth);
		int h = (Texture.Height/TilesHeight);
		_sourceRectangles = new Rectangle[w * h];

		for(int y=0; y<h; y++)
		{
			for(int x=0; x<w; x++)
			{
				_sourceRectangles[x + y * w] = new Rectangle(x * TilesWidth, y * TilesHeight, TilesWidth, TilesHeight);
			}
		}

	}

	public Tilemap(string texturePath, int[] data, int width, int height)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);

		Width = width;
		Height = height;

		TilesWidth = Engine.TileSize;
		TilesHeight = Engine.TileSize;

		_data = data;
		_tiles = new Tile[Width * Height];

		int w = (Texture.Width/TilesWidth);
		int h = (Texture.Height/TilesHeight);
		_sourceRectangles = new Rectangle[w * h];

		for(int y=0; y<h; y++)
		{
			for(int x=0; x<w; x++)
			{
				_sourceRectangles[x + y * w] = new Rectangle(x * TilesWidth, y * TilesHeight, TilesWidth, TilesHeight);
			}
		}
	}

	public abstract void Draw();
}
