using SapukayaEngine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RedRanger;

public sealed class UiLayer : Layer
{
	public UiRoot Root;

	private UiLabel _scoreLabel;

	public UiLayer()
	{
		Root = new();
	}

    public override void Activate()
    {
        base.Activate();

		var horizontalContainer = new UiHorizontalContainer();
		horizontalContainer.Gap = 16;
		horizontalContainer.Transform.LocalPosition = new Vector2(300, 10);

		for(int i=0; i<Globals.PlayerLifes; i++)
		{
			var uiLifeBattery = new UiSprite(Globals.GameAtlas.Texture, Globals.GameAtlas.GetRegion("ui_life_battery"));
			horizontalContainer.AddChild(uiLifeBattery);
		}

		Root.AddChild(horizontalContainer);

		_scoreLabel = new("Fonts/fnt_default");
		_scoreLabel.SetText($"Scores: {Globals.Score}");

		_scoreLabel.Transform.LocalPosition = new Vector2(Root.Width-_scoreLabel.Width-24f, 5f);

		Root.AddChild(_scoreLabel);
    }

    public override void Update(float dt)
    {
		_scoreLabel.SetText($"Scores: {Globals.Score}");
    }

	public override void Draw()
	{
		Engine.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);

			Root.Draw();

		Engine.SpriteBatch.End();
	}
}
