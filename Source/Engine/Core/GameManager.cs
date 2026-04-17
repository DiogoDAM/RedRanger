using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace SapukayaEngine;

public class GameManager : Game
{
	private static GameManager _instance;
	public static GameManager Instance => _instance;

	public int WindowWidth => Engine.Graphics.PreferredBackBufferWidth;
	public int WindowHeight => Engine.Graphics.PreferredBackBufferHeight;

	public int VirtualWidth { get; private set; }
	public int VirtualHeight { get; private set; }

	public Canvas GameCanvas;

	public bool DebugMode { get; private set; }

	public List<Layer> Layers { get; private set; }

	public GameManager(int windowWidth, int windowHeight, string windowTitle, bool fullscreen, bool debugMode=false) : base()
	{
		_instance = this;

		Engine.Init(this);

		SetWindowSize(windowWidth, windowHeight);
		SetVirtualSize(windowWidth, windowHeight);
		SetTitle(windowTitle);
		SetFullScreen(fullscreen);

		DebugMode = debugMode;

		Layers = new();

		IsMouseVisible = true;
	}

    protected override void Initialize()
    {
		Engine.Initialize(this);

        base.Initialize();

		GameCanvas = new();
		GameCanvas.OnWindowSizeChanged();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
		float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

		Engine.Input.Update(dt);

		for(int i=0; i<Layers.Count; i++)
		{
			if(Layers[i].CanUpdate)
				Layers[i].Update(dt);
		}

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
		GameCanvas.StartRenderTarget();

			for(int i=0; i<Layers.Count; i++)
			{
				if(Layers[i].CanDraw)
					Layers[i].Draw();
			}

		GameCanvas.EndRenderTarget();

        base.Draw(gameTime);
    }

	public void SetWindowSize(int width, int height)
	{
		Engine.Graphics.PreferredBackBufferWidth = width;
		Engine.Graphics.PreferredBackBufferHeight = height;
		Engine.Graphics.ApplyChanges();
	}

	public void SetVirtualSize(int width, int height)
	{
		VirtualWidth = width;
		VirtualHeight = height;
	}

	public void SetTitle(string title)
	{
		Window.Title = title;
	}

	public void SetFullScreen(bool value)
	{
		Engine.Graphics.IsFullScreen = value;

		if(Engine.Graphics.IsFullScreen)
			SetWindowSize(Window.ClientBounds.Width, Window.ClientBounds.Height);

		Engine.Graphics.ApplyChanges();
	}

	public void ToggleFullScreen()
	{
		Engine.Graphics.ToggleFullScreen();

		if(Engine.Graphics.IsFullScreen)
			SetWindowSize(Window.ClientBounds.Width, Window.ClientBounds.Height);

		Engine.Graphics.ApplyChanges();
	}

	public T AddLayer<T>() where T : Layer, new()
	{
		T layer = (T)Layers.Find(l => l is T);

		if(layer != null)
			return null;

		layer = new T();

		Layers.Add(layer);
		layer.Activate();

		return layer;
	}

	public T RemoveLayer<T>() where T : Layer
	{
		T layer = null;

		for(int i=0; i<Layers.Count; i++)
		{
			if(Layers[i] is T)
			{
				layer = (T)Layers[i];
				Layers.RemoveAt(i);
				layer.Disable();
			}
		}

		return layer;
	}

	public bool TryGetLayer<T>(out T outLayer) where T : Layer
	{
		outLayer = (T)Layers.Find(l => l is T);

		return (outLayer == null) ? false : true;
	}

	public T GetLayer<T>() where T : Layer
	{
		T layer = (T)Layers.Find(l => l is T);

		return layer;
	}

	public T ChangeLayer<T,T2>() where T : Layer where T2 : Layer, new()
	{
		T oldLayer = null;

		for(int i=0; i<Layers.Count; i++)
		{
			if(Layers[i] is T)
			{
				oldLayer = (T)Layers[i];
				oldLayer.Disable();

				T2 newLayer = new T2();
				Layers[i] = newLayer;
				newLayer.Activate();
			}
		}

		return oldLayer;
	}
}
