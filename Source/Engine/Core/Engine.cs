using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public static class Engine
{
	public static GraphicsDeviceManager Graphics { get; private set; }
	public static GraphicsDevice GraphicsDevice { get; private set; }
	public static ContentManager Content { get; private set; }
	public static SpriteBatch SpriteBatch { get; private set; }

	public static int TileSize { get; private set; } = 32;

	public static Color ClearColor = Color.CornflowerBlue;

	public static Input Input = new();

	public static void Init(Game game)
	{
		Graphics = new GraphicsDeviceManager(game);

		Content = game.Content;
		Content.RootDirectory = "Content";

		Utilities.Random = new();
	}

	public static void Initialize(Game game)
	{
		GraphicsDevice = game.GraphicsDevice;
		SpriteBatch = new(GraphicsDevice);
	}

	public static void LoadContent()
	{
	}

	public static void SetTileSize(int value) => TileSize = value;
}
