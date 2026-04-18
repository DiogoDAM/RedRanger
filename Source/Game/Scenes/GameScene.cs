using System.Collections.Generic;
using SapukayaEngine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RedRanger;

public sealed class GameScene : Scene
{
	private List<Enemy> _enemies;
	private List<Projectile> _projectiles;
	private Player _player;

	private ParallaxManager _parallax;

	public GameScene() : base()
	{
		_enemies = new();
		_projectiles = new();

		_parallax = new();
	}

    public override void Activate()
    {
        base.Activate();

		Add(_parallax);
		_parallax.AddLayer(new BackgroundLayer("Textures/spr_sky_bg", 640, 360, 100));
		_parallax.AddLayer(new BackgroundLayer("Textures/spr_trees_bg", 640, 360, 800));

		_player = new Player();
		Add(_player);

		Add(new EnemyFactory());

		_enemies = _entities.GetList<Enemy>();
		_projectiles = _entities.GetList<Projectile>();

    }

    public override void Update(float dt)
    {
        base.Update(dt);
    }

    public override void Draw()
    {
        base.Draw();
    }
}
