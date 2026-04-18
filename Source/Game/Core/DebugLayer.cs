using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SapukayaEngine;

namespace RedRanger;

public sealed class DebugLayer : Layer
{
	private UiRoot _uiRoot;
	private UiLabel _uiTextEntities;

	private List<Entity> _entities;

	public DebugLayer() : base()
	{
	}

	public override void Activate() 
	{
		Active = true;
		Running = true;

		_uiRoot = new();

		_entities = GameManager.Instance.GetLayer<GameLayer>().ActiveScene.All;

		_uiTextEntities = new("Fonts/fnt_default");

		_uiRoot.AddChild(_uiTextEntities);
	}

    public override void Update(float dt)
    {
		_uiTextEntities.SetText("Entities in Scene: " + _entities.Count);
    }

    public override void Draw()
    {
		Engine.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
		
			foreach(var entity in _entities)
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

		Engine.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);

			_uiRoot.Draw();
		
		Engine.SpriteBatch.End();
    }
}
