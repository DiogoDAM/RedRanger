using System;

using Microsoft.Xna.Framework;

namespace SapukayaEngine;

public sealed class BoxCollider : Collider
{
	public int Width;
	public int Height;

	public float Left => Transform.GlobalPosition.X;
	public float Right => Transform.GlobalPosition.X + Width;
	public float Top => Transform.GlobalPosition.Y;
	public float Bottom => Transform.GlobalPosition.Y + Height;

	public BoxCollider(int width, int height) : base()
	{
		Width = width;
		Height = height;
	}

	public override bool Intersects(BoxCollider other)
	{
		return Left <= other.Right &&
			Right >= other.Left &&
			Top <= other.Bottom &&
			Bottom >= other.Top;
	}

    public override bool Intersects(CircleCollider other)
    {
		float closestX = Math.Clamp(other.Center.X, Left, Right);
		float closestY = Math.Clamp(other.Center.Y, Top, Bottom);

		float dx = other.Center.X - closestX;
		float dy = other.Center.Y - closestY;

		return (dx * dx + dy * dy) <= other.Radius * other.Radius;}
}
