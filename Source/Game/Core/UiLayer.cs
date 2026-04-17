using SapukayaEngine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RedRanger;

public sealed class UiLayer : Layer
{
	public UiRoot Root;

	public UiLayer()
	{
		Root = new();
	}

    public override void Activate()
    {
        base.Activate();

		var uiLifeBg = new UiSprite(Globals.GameAtlas.Texture, Globals.GameAtlas.GetRegion("ui_life_bg"));

		var horizontalContainer = new UiHorizontalContainer();
		horizontalContainer.Gap = 16;
		horizontalContainer.Transform.LocalPosition = new Vector2(16, 2);

		for(int i=0; i<Globals.PlayerLifes; i++)
		{
			var uiLifeBattery = new UiSprite(Globals.GameAtlas.Texture, Globals.GameAtlas.GetRegion("ui_life_battery"));
			horizontalContainer.AddChild(uiLifeBattery);
		}

		uiLifeBg.AddChild(horizontalContainer);

		Root.AddChild(uiLifeBg);
    }

	public override void Draw()
	{
		Engine.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);

			Root.Draw();

		Engine.SpriteBatch.End();
	}
}
