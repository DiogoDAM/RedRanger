using System;
using System.Collections.Generic;

using SapukayaEngine;

namespace RedRanger;

public sealed class ParallaxManager : Entity
{
	public List<BackgroundLayer> BackgroundLayers { get; private set; }

	public ParallaxManager() : base()
	{
		BackgroundLayers = new();
	}

	public void AddLayer(BackgroundLayer layer)
	{
		BackgroundLayers.Add(layer);
	}

    public override void Update(float dt)
    {
		foreach(var layer in BackgroundLayers)
		{
			layer.Update(dt);
		}
    }

    public override void Draw()
    {
		foreach(var layer in BackgroundLayers)
		{
			layer.Draw();
		}
    }
}
