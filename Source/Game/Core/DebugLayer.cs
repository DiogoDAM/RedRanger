using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SapukayaEngine;

namespace RedRanger;

public sealed class DebugLayer : Layer
{
	public DebugLayer() : base()
	{
	}

	public override void Activate() 
	{
		Active = true;
		Running = true;
	}

    public override void Draw()
    {
		List<Entity> entities = GameManager.Instance.GetLayer<GameLayer>().ActiveScene.All;

		Engine.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
		
			foreach(var entity in entities)
			{
				if(entity is Enemy enemy)
				{
					CircleCollider col = (CircleCollider)enemy.Colliders[0];
					Texture2D texture = ShapeDrawer.LineCircle((int)col.Radius, Color.Red);

					Engine.SpriteBatch.Draw(texture,
							col.Transform.GlobalPosition,
							new Rectangle(0, 0, (int)col.Diameter, (int)col.Diameter),
							Color.White);
				}
				else 
				{
					if(entity.Colliders.Count == 0)
						continue;

					BoxCollider col = (BoxCollider)entity.Colliders[0];
					Texture2D texture = ShapeDrawer.LineRectangle(col.Width, col.Height, Color.Green);

					Engine.SpriteBatch.Draw(texture,
							col.Transform.GlobalPosition,
							new Rectangle(0, 0, col.Width, col.Height),
							Color.White);
				}
			}

		Engine.SpriteBatch.End();
    }
}
