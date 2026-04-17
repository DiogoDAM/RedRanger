using SapukayaEngine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RedRanger;

public sealed class Player : Entity
{ 
	public int Lifes { get; private set; } = Globals.PlayerLifes;

	private BoxCollider _collider;

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

		_collider = new(42, 5);
		_collider.Transform.LocalPosition = new Vector2(16, 5);
		AddCollider(_collider);
	}

    public override void Draw()
    {
        base.Draw();

		Texture2D texture = ShapeDrawer.LineRectangle(_collider.Width, _collider.Height, Color.Magenta);
		Engine.SpriteBatch.Draw(texture,
				_collider.Transform.GlobalPosition,
				new Rectangle(0, 0, _collider.Width, _collider.Height),
				Color.White);
    }

    public override void OnTrigger(Entity other)
    {
		Lifes--;

		GameManager.Instance.GetLayer<UiLayer>().Root.GetChild<UiHorizontalContainer>().PopChild();
    }
}
