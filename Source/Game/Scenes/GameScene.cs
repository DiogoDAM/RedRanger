using System.Collections.Generic;
using SapukayaEngine;

namespace RedRanger;

public sealed class GameScene : Scene
{
	private List<Enemy> _enemies;
	private List<Projectile> _projectiles;

	public GameScene() : base()
	{
		_enemies = new();
		_projectiles = new();
	}

    public override void Activate()
    {
        base.Activate();

		Add(new Player());

		Add(new EnemyFactory());

		_enemies = _entities.GetList<Enemy>();
		_projectiles = _entities.GetList<Projectile>();
    }

    public override void Update(float dt)
    {
		CollisionHelper.Collide<Enemy, Projectile>(_enemies, _projectiles);

        base.Update(dt);
    }
}
