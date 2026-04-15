using SapukayaEngine;

using System;

using Microsoft.Xna.Framework;

namespace RedRanger;

public sealed class EnemyFactory : Entity
{
    public override void Added()
    {
		Timer timer = new Timer(1f, true);
		timer.TimeUp += OnTimeUp;
		Add<Timer>(timer);
    }

	public static Enemy CreateEnemy(EnemyType type)
	{
		Enemy enemy = new(new Vector2(GameManager.Instance.VirtualWidth, Utilities.Random.Next(0, GameManager.Instance.VirtualHeight)));
		switch(type)
		{
			case EnemyType.Demus:
				enemy.Speed = 120f;
				enemy.Sprite = Globals.GameAtlas.CreateSprite("enemy_demus");
				break;
		}

		return enemy;
	}

	private void OnTimeUp()
	{
		GameManager.Instance.GetLayer<GameLayer>().ActiveScene.Add(CreateEnemy(EnemyType.Demus));
	}
}
