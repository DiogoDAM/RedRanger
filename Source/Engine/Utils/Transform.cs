using System;

using Microsoft.Xna.Framework;

namespace SapukayaEngine;

public sealed class Transform
{
	public Vector2 LocalPosition = Vector2.Zero;
	public Vector2 LocalScale = Vector2.One;
	public float LocalRotation = 0f;

	public Transform Parent;

	public Vector2 GlobalPosition => LocalPosition + ((Parent == null) ? Vector2.Zero : Parent.GlobalPosition);
	public Vector2 GlobalScale => LocalScale * ((Parent == null) ? Vector2.One : Parent.GlobalScale);
	public float GlobalRotation => LocalRotation + ((Parent == null) ? 0f : Parent.GlobalRotation);

	public Transform()
	{
	}

	public Transform(Vector2 pos)
	{
		LocalPosition = pos;
	}

	public Transform(Vector2 pos, float rotation)
	{
		LocalPosition = pos;
		LocalRotation = rotation;
	}

	public Transform(Vector2 pos, float rotation, Vector2 scale)
	{
		LocalPosition = pos;
		LocalRotation = rotation;
		LocalScale = scale;
	}

	public Transform(Transform other)
	{
		LocalPosition = other.LocalPosition;
		LocalRotation = other.LocalRotation;
		LocalScale = other.LocalScale;
	}
}
