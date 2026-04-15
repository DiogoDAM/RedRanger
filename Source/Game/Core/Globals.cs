using SapukayaEngine;

namespace RedRanger;

public static class Globals
{
	public static SpriteAtlas GameAtlas;

	public static void Initialize()
	{
		GameAtlas = new("Textures/atlas_game");

		GameAtlas.AddHFrames("player", 0, 0, 64, 16, 4);

		GameAtlas.AddRegion("player_projectile", 0, 32, 12, 4);

		GameAtlas.AddRegion("enemy_demus", 0, 48, 32, 32);
	}
}
