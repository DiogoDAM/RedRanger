using System.Collections.Generic;
using SapukayaEngine;

namespace RedRanger;

public sealed class GameScene : Scene
{
	private List<Enemy> _enemies;
	private List<Projectile> _projectiles;
	private Player _player;

	public GameScene() : base()
	{
		_enemies = new();
		_projectiles = new();
	}

    public override void Activate()
    {
        base.Activate();

		_player = new Player();
		Add(_player);

		Add(new EnemyFactory());

		_enemies = _entities.GetList<Enemy>();
		_projectiles = _entities.GetList<Projectile>();

		System.Console.WriteLine(GameLayers.Player);
		System.Console.WriteLine(GameLayers.Enemy);
		System.Console.WriteLine(GameLayers.EnemyProjectile);
    }

    public override void Update(float dt)
    {
		CollisionHelper.Collide<Enemy, Projectile>(_enemies, _projectiles);
		CollisionHelper.Collide<Player, Enemy>(_player, _enemies);

        base.Update(dt);
    }
}
