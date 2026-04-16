using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace SapukayaEngine;

public static class CollisionHelper
{
	public static void Collide<T, T2>(IList<T> cols1, List<T2> cols2) where T : Entity where T2 : Entity
	{
		foreach(var col1 in cols1)
		{
			foreach(var col2 in cols2)
			{
				if(col1.Collides(col2))
				{
					col1.OnCollide(col2);
					col2.OnCollide(col1);
				}
			}
		}
	}
}
