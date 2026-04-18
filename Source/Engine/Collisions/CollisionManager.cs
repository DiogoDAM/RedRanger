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
		int count = entities.Count;

		for(int i=0; i<count; i++)
		{
			Entity e1 = entities[i];
			for(int j=i+1; j<count; j++)
			{
				Entity e2 = entities[j];

				if(ReferenceEquals(e1, e2))
					continue;

				if(e1.Colliders.Count <= 0 || e2.Colliders.Count <= 0)
					continue;

				if((e1.Layers & e2.Masks) == 0 &&
					(e2.Layers & e1.Masks) == 0) 
					continue;

				foreach(var col1 in e1.Colliders)
				{
					foreach(var col2 in e2.Colliders)
					{
						if(!col1.Intersects(col2))
							continue;

						if(col1.IsTrigger || col2.IsTrigger)
						{
							e1.OnTrigger(e2);
							e2.OnTrigger(e1);
						}
						else
						{
							CollisionHelper.ResolveCollision(col1, col2);

							e1.OnCollide(e2);
							e2.OnCollide(e1);
						}
					}
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
