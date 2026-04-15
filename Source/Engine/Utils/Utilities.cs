using System;

using Microsoft.Xna.Framework;

namespace SapukayaEngine;

public static class Utilities
{
	public static Random Random;

	public static Point IsometricWorldToGrid(Vector2 worldPos)
	{
		float halfWidth = Engine.TileSize / 2f;
		float halfHeight = Engine.TileSize / 4f;

		int gridX = (int)((worldPos.X / halfWidth + worldPos.Y / halfHeight) / 2);
		int gridY = (int)((worldPos.Y / halfHeight - (worldPos.X / halfWidth)) / 2);

		return new Point(gridX, gridY);
	}

	public static Vector2 GridToIsometricWorld(Point gridPos)
	{
		float halfWidth = Engine.TileSize / 2f;
		float halfHeight = Engine.TileSize / 4f;

		float worldX = (gridPos.X - gridPos.Y) * halfWidth;
		float worldY = (gridPos.X + gridPos.Y) * halfHeight;

		return new Vector2(worldX-16, worldY);
	}

	public static Vector2 GridToIsometricWorld(int x, int y)
	{
		float halfWidth = Engine.TileSize / 2f;
		float halfHeight = Engine.TileSize / 4f;

		float worldX = (x - y) * halfWidth;
		float worldY = (x + y) * halfHeight;

		return new Vector2(worldX-16, worldY);
	}
}
