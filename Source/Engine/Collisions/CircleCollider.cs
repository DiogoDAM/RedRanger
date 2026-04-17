using System;

using Microsoft.Xna.Framework;

namespace SapukayaEngine;

public sealed class CircleCollider : Collider
{
	public float Radius;

	public Vector2 Center => new Vector2(Transform.GlobalPosition.X + Radius, Transform.GlobalPosition.Y + Radius);

	public float Diameter => Radius*2;

	public CircleCollider(float radius) : base()
	{
		Radius = radius;
	}

    public override bool Intersects(BoxCollider other)
    {
		float closestX = Math.Clamp(Center.X, other.Left, other.Right);
		float closestY = Math.Clamp(Center.Y, other.Top, other.Bottom);

		float dx = Center.X - closestX;
		float dy = Center.Y - closestY;

		return (dx * dx + dy * dy) <= Radius * Radius;}

    public override bool Intersects(CircleCollider other)
    {
		float dx = Center.X - other.Center.X;
		float dy = Center.Y - other.Center.Y;

		return (dx * dx + dy * dy) <= Radius * Radius;}
}
