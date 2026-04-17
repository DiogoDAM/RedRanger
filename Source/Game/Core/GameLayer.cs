using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SapukayaEngine;

namespace RedRanger;

public sealed class GameLayer : Layer
{
	public Scene ActiveScene { get; private set; }

	public Matrix TransformMatrix = Matrix.CreateTranslation(Vector3.Zero) * Matrix.CreateRotationZ(0) * Matrix.CreateScale(Vector3.One);

	public Vector2 MousePosition => Vector2.Transform(Engine.Input.Mouse.Position, Matrix.Invert(TransformMatrix));

	public T GetScene<T>() where T : Scene => (T)ActiveScene;

    public void ChangeScene<T>() where T : Scene, new()
	{
		if(ActiveScene != null)
		{
			ActiveScene.Disable();
			ActiveScene.Dispose();
		}

		ActiveScene = new T();

		ActiveScene.Activate();
		ActiveScene.Start();
	}

    public override void Activate()
    {
        base.Activate();

    }

    public override void Update(float dt)
    {
		if(ActiveScene.CanUpdate)
			ActiveScene.Update(dt);
    }

    public override void Draw()
    {
		Engine.SpriteBatch.Begin(samplerState: SamplerState.PointWrap, transformMatrix: TransformMatrix);

			if(ActiveScene.CanDraw)
				ActiveScene.Draw();

		Engine.SpriteBatch.End();
    }

    protected override void Dispose(bool disposable)
    {
		if(disposable)
		{
			ActiveScene.Disable();
			ActiveScene.Dispose();
		}
    }
}
