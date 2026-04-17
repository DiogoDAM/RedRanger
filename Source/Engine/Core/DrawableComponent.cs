using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public abstract class DrawableComponent : Component
{
	public Color Color = Color.White;
	public Vector2 Origin;
	public SpriteEffects Flip;
	public float Depth;

	public bool Visible = true;

	public virtual int Width { get; protected set; }
	public virtual int Height { get; protected set; }

	public bool CanDraw => Visible && !Disposed;

	public virtual void Draw()
	{
	}

	public void SetOrigin(OriginPosition pos)
	{
		switch(pos)
		{
			case OriginPosition.None: return;
			case OriginPosition.LeftTop: Origin = new Vector2(0, 0); break;;
			case OriginPosition.MiddleTop: Origin = new Vector2(Width * 0.5f, 0); break;;
			case OriginPosition.RightTop: Origin = new Vector2(Width, 0); break;;

			case OriginPosition.LeftCenter: Origin = new Vector2(0, Height * 0.5f); break;;
			case OriginPosition.MiddleCenter: Origin = new Vector2(Width * 0.5f, Height * 0.5f); break;;
			case OriginPosition.RightCenter: Origin = new Vector2(Width, Height * 0.5f); break;;

			case OriginPosition.LeftBottom: Origin = new Vector2(0, Height); break;;
			case OriginPosition.MiddleBottom: Origin = new Vector2(Width * 0.5f, Height); break;;
			case OriginPosition.RightBottom: Origin = new Vector2(Width, Height); break;;
		}
	}
}
