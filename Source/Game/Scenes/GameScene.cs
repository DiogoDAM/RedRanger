using System.Collections.Generic;
using SapukayaEngine;

namespace RedRanger;

public sealed class GameScene : Scene
{
	public GameScene() : base()
	{
	}

    public override void Activate()
    {
        base.Activate();

		Add(new Player());

		Add(new EnemyFactory());
    }

    public override void Update(float dt)
    {
		List<Enemy> enemies = _entities.GetList<Enemy>();
		List<Projectile> projectiles = _entities.GetList<Projectile>();

		foreach(var enemy in enemies)
		{
			foreach(var projectile in projectiles)
			{
				if(projectile.Collides(enemy))
				{
					enemy.OnCollide(projectile);
					projectile.OnCollide(enemy);
				}
			}
		}

        base.Update(dt);
    }
}
