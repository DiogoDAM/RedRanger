using SapukayaEngine;

namespace RedRanger;

public static class GameLayers
{
	public const int Player = 1 << 1;
	public const int Enemy = 1 << 2;
	public const int PlayerProjectile = 1 << 3;
	public const int EnemyProjectile = 1 << 4;
}

public static class Globals
{
	public static SpriteAtlas GameAtlas;
	
	public static int PlayerLifes = 3;

	public static int Score = 0;

	public static void Initialize()
	{
		GameAtlas = new("Textures/atlas_game");

		GameAtlas.AddHFrames("player", 0, 0, 64, 16, 4);

		GameAtlas.AddHFrames("player_projectile", 0, 32, 14, 6, 2);

		GameAtlas.AddHFrames("small_explosion", 0, 48, 16, 16, 3);
		GameAtlas.AddHFrames("big_explosion", 0, 288, 32, 32, 6);

		GameAtlas.AddRegion("ui_life_battery", 0, 128, 32, 32);

		GameAtlas.AddRegion("enemy_demus", 0, 64, 32, 32);
	}
}
