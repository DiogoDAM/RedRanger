using SapukayaEngine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RedRanger;

public sealed class UiLayer : Layer
{
	public UiLayer()
	{
	}

	public override void Draw()
	{
		Engine.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);

		Engine.SpriteBatch.End();
	}
}
