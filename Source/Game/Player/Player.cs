using SapukayaEngine;

using Microsoft.Xna.Framework;

namespace RedRanger;

public sealed class Player : Entity
{ 
	public int Lifes { get; private set; } = Globals.PlayerLifes;

	public Player() : base()
	{
		Layers = GameLayers.Player;

		Masks = GameLayers.Enemy;
	}

	public override void Added()
	{
		Add<HSprite>(Globals.GameAtlas.CreateHSprite("player"));

		Add<PlayerMovement>(new(200f));

		Add<PlayerShoot>(new(0.1f));

		AddCollider(new BoxCollider( 42, 5, Transform));
	}

    public override void OnTrigger(Entity other)
    {
		Lifes--;

		GameManager.Instance.GetLayer<UiLayer>().Root.GetChild<UiHorizontalContainer>().PopChild();
    }
}
