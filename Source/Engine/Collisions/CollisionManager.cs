using System;
using System.Collections.Generic;

namespace SapukayaEngine;

public sealed class CollisionManager : IDisposable
{
	private Scene _scene;

	public CollisionManager(Scene scene)
	{
		_scene = scene;
	}

	public void Update(float dt)
	{
		List<Entity> entities = _scene.All;

		foreach(var e1 in entities)
		{
			foreach(var e2 in entities)
			{
				if(e1.Equals(e2))
					continue;

				if((e1.Layers & e2.Masks) == 0) 
					continue;

				if(e1.Collides(e2))
				{
					e1.OnCollide(e2);
					e2.OnCollide(e1);
				}
			}
		}
	}

	public void Dispose()
	{
		_scene = null;

		GC.SuppressFinalize(this);
	}
}
