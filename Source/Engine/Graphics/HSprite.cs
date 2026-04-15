using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public sealed class HSprite : DrawableComponent
{
	public readonly Texture2D Texture;

	private readonly Rectangle[] _frames;

	public readonly int FramesCount;

	public readonly int FramesWidth;
	public readonly int FramesHeight;

	private int _currentFrame = 0;

	public HSprite(Texture2D texture, int x, int y, int framesWidth, int framesHeight, int amount)
	{
		Texture = texture;

		_frames = new Rectangle[amount];

		FramesCount = amount;

		FramesWidth = framesWidth;
		FramesHeight = framesHeight;

		for(int i=0; i<amount; i++)
			_frames[i] = new Rectangle(x + (i * FramesWidth), y, FramesWidth, FramesHeight);
	}

	public HSprite(Texture2D texture, Rectangle[] frames)
	{
		Texture = texture;

		_frames = frames;

		FramesCount = frames.Length;

		FramesWidth = frames[0].Width;
		FramesHeight = frames[0].Height;
	}

	public void SetFrame(int index)
	{
		if(index < 0 | index >= FramesCount)
			throw new IndexOutOfRangeException($"index is out of range: (0, {FramesCount}). value: {index}");

		_currentFrame = index;
	}

	public override void Draw()
	{
		Engine.SpriteBatch.Draw(Texture,
				Entity.Transform.GlobalPosition,
				_frames[_currentFrame],
				Color,
				Entity.Transform.GlobalRotation,
				Origin,
				Entity.Transform.GlobalScale,
				Flip,
				Depth);
	}
}
