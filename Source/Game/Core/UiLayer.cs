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

		Root.AddChild(uiLifeBg);
    }

	public override void Draw()
	{
		Engine.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);

			Root.Draw();

		Engine.SpriteBatch.End();
	}
}
