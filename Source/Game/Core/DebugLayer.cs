using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SapukayaEngine;

namespace RedRanger;

public sealed class DebugLayer : Layer
{
	public Canvas MainCanvas;

    public override void Activate()
    {
        base.Activate();

    }

    public override void Draw()
    {
		Engine.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);

		Engine.SpriteBatch.End();
    }
}
