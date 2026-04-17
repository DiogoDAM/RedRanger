using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public sealed class SpriteAtlas : IDisposable
{
	private Dictionary<string, Rectangle> _sourceRectangles;
	private Dictionary<string, Rectangle[]> _sourceFrames;

	public readonly Texture2D Texture;

	public SpriteAtlas(string texturePath)
	{
		Texture = Engine.Content.Load<Texture2D>(texturePath);

		_sourceRectangles = new();
		_sourceFrames = new();
	}

	public void AddRegion(string name, int x, int y, int w, int h)
	{
		if(string.IsNullOrEmpty(name))
			throw new ArgumentNullException("name is null or empty");

		_sourceRectangles[name] = new Rectangle(x, y, w, h);
	}

	public Rectangle GetRegion(string name)
	{
		if(string.IsNullOrEmpty(name))
			throw new ArgumentNullException("name is null or empty");

		if(!_sourceRectangles.ContainsKey(name))
			throw new KeyNotFoundException("Region name not found. value: " + name);

		return _sourceRectangles[name];
	}


	public Sprite CreateSprite(string name)
	{
		if(string.IsNullOrEmpty(name))
			throw new ArgumentNullException("name is null or empty");

		if(!_sourceRectangles.ContainsKey(name))
			throw new KeyNotFoundException("Region name not found. value: " + name);

		return new Sprite(Texture, _sourceRectangles[name]);
	}

	public void AddHFrames(string name, int x, int y, int framesWidth, int framesHeight, int amount)
	{
		if(string.IsNullOrEmpty(name))
			throw new ArgumentNullException("name is null or empty");

		Rectangle[] frames = new Rectangle[amount];

		for(int i=0; i<amount; i++)
			frames[i] = new Rectangle(x + (i * framesWidth), y, framesWidth, framesHeight);

		_sourceFrames[name] = frames;

	}

	public HSprite CreateHSprite(string name)
	{
		if(string.IsNullOrEmpty(name))
			throw new ArgumentNullException("name is null or empty");

		if(!_sourceFrames.ContainsKey(name))
			throw new KeyNotFoundException("Region name not found. value: " + name);

		return new HSprite(Texture, _sourceFrames[name]);
	}

	public void Dispose()
	{
		_sourceRectangles.Clear();
	}
}
