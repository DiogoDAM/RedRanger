using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public sealed class Canvas
{
	public RenderTarget2D Target { get; private set; }

	private Rectangle _destinationRectangle;

	public SamplerState SamplerState = SamplerState.PointWrap;

	public Canvas()
	{
	}

	public void OnWindowSizeChanged()
	{
		Target = new(Engine.GraphicsDevice, GameManager.Instance.VirtualWidth, GameManager.Instance.VirtualHeight);
		RecalculateResolution();
	}

	private void RecalculateResolution()
	{
		var screenSize = Engine.GraphicsDevice.PresentationParameters.Bounds;

        float scaleX = (float)screenSize.Width / Target.Width;
        float scaleY = (float)screenSize.Height / Target.Height;
        float scale = Math.Min(scaleX, scaleY);

        int newWidth = (int)(Target.Width * scale);
        int newHeight = (int)(Target.Height * scale);

        int posX = (screenSize.Width - newWidth) / 2;
        int posY = (screenSize.Height - newHeight) / 2;

        _destinationRectangle = new Rectangle(posX, posY, newWidth, newHeight);
	}

	public void StartRenderTarget()
	{
		Engine.GraphicsDevice.SetRenderTarget(Target);
		Engine.GraphicsDevice.Clear(Engine.ClearColor);
	}

	public void EndRenderTarget()
	{
		Engine.GraphicsDevice.SetRenderTarget(null);
		Engine.GraphicsDevice.Clear(Color.Black);

		Engine.SpriteBatch.Begin(samplerState: SamplerState );

			Engine.SpriteBatch.Draw(Target, _destinationRectangle, Color.White);

		Engine.SpriteBatch.End();
	}
}
