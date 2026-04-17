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
		GameAtlas.AddHFrames("small_explosion", 0, 48, 16, 16, 3);

		GameAtlas.AddRegion("ui_life_bg", 0, 128, 192, 48);
		GameAtlas.AddRegion("ui_life_battery", 192, 128, 32, 48);

		GameAtlas.AddRegion("enemy_demus", 0, 80, 32, 32);
	}
}
