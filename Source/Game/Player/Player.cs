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

		AddCollider<BoxCollider>(new( 42, 5, Transform));
	}

    public override void OnCollide(Entity other)
    {
		Lifes--;

		GameManager.Instance.GetLayer<UiLayer>().Root.GetChild<UiHorizontalContainer>().PopChild();
    }
}
