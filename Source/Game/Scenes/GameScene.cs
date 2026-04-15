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
}
