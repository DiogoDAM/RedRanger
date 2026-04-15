using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using SapukayaEngine;

namespace RedRanger;

public sealed class Camera
{
	public Vector2 Position = Vector2.Zero;
	public float Scale = 1f;
	public float Rotation = 0f;

	public float Speed = 250f;

	private GameLayer _gameLayer;

	public Camera(Vector2 pos, float scale)
	{
		Position = pos;
		Scale = scale;

		_gameLayer = GameManager.Instance.GetLayer<GameLayer>();

		_gameLayer.TransformMatrix = GetMatrix();
	}

	public Matrix GetMatrix() => Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
		Matrix.CreateRotationZ(Rotation) *
		Matrix.CreateScale(Scale, Scale, 1f);

	public void Update(float dt)
	{
		Vector2 dir = Vector2.Zero;

		if(Engine.Input.Keyboard.IsKeyDown(Keys.D))
			dir.X = 1;
		else if(Engine.Input.Keyboard.IsKeyDown(Keys.A))
			dir.X = -1;

		if(Engine.Input.Keyboard.IsKeyDown(Keys.S))
			dir.Y = 1;
		else if(Engine.Input.Keyboard.IsKeyDown(Keys.W))
			dir.Y = -1;

		if(dir != Vector2.Zero)
		{
			dir.Normalize();

			Position += dir * Speed * dt;

			_gameLayer.TransformMatrix = GetMatrix();
		}

		if(Engine.Input.Mouse.IsScrollWheelMoveUpwards)
		{
			ZoomIn(10f * dt);
			_gameLayer.TransformMatrix = GetMatrix();
		}
		else if(Engine.Input.Mouse.IsScrollWheelMoveDown)
		{
			ZoomOut(10f * dt);
			_gameLayer.TransformMatrix = GetMatrix();
		}
	}

	private void ZoomIn(float value)
	{
		Scale += value;
		if(Scale > 10f) 
			Scale = 10f;
	}

	private void ZoomOut(float value)
	{
		Scale -= value;
		if(Scale < 1f) 
			Scale = 1f;
	}
}
