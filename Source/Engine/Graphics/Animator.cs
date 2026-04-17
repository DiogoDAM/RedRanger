using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public sealed class Animator : DrawableComponent
{
	public readonly Texture2D Texture;

	public readonly int FramesWidth;
	public readonly int FramesHeight;

	public Dictionary<string, Animation> Animations { get; private set; }

	public string CurrentAnimationName { get; private set; }

	public Animation CurrentAnimation => Animations[CurrentAnimationName];

	private Rectangle[] _frames;

	public Transform Transform;

	public Animator(Texture2D texture, int startX, int startY, int width, int height, int framesWidth, int framesHeight)
	{
		Texture = texture;

		Animations = new();

		FramesWidth = framesWidth;
		FramesHeight = framesHeight;

		int cols = width / FramesWidth;
		int rows = height / FramesHeight;
		
		_frames = new Rectangle[cols * rows];

		for(int y=0; y<rows; y++)
		{
			for(int x=0; x<cols; x++)
			{
				_frames[x + y * cols] = new Rectangle(startX + (FramesWidth * x), startY + (FramesHeight * y), FramesWidth, FramesHeight);
			}
		}

		Transform = new();
	}

	public Animator(string path, int startX, int startY, int framesWidth, int framesHeight)
	{
		Texture = Engine.Content.Load<Texture2D>(path);

		Animations = new();

		FramesWidth = framesWidth;
		FramesHeight = framesHeight;

		int cols = Texture.Width / FramesWidth;
		int rows = Texture.Height / FramesHeight;

		_frames = new Rectangle[cols * rows];

		for(int y=0; y<rows; y++)
		{
			for(int x=0; x<cols; x++)
			{
				_frames[x + y * cols] = new Rectangle(startX + (FramesWidth * x), startY + (FramesHeight * y), FramesWidth, FramesHeight);
			}
		}

		Transform = new();
	}

	public Animator(string path, int startX, int startY, int framesWidth, int framesHeight, Transform parent)
	{
		Texture = Engine.Content.Load<Texture2D>(path);

		Animations = new();

		FramesWidth = framesWidth;
		FramesHeight = framesHeight;

		int cols = Texture.Width / FramesWidth;
		int rows = Texture.Height / FramesHeight;

		_frames = new Rectangle[cols * rows];

		for(int y=0; y<rows; y++)
		{
			for(int x=0; x<cols; x++)
			{
				_frames[x + y * cols] = new Rectangle(startX + (FramesWidth * x), startY + (FramesHeight * y), FramesWidth, FramesHeight);
			}
		}

		Transform = new();
		Transform.Parent = parent;
	}

	public void AddAnimation(string name, int[] framesIndexes, float duration, bool infinite=false)
	{
		if(Animations.ContainsKey(name))
			throw new Exception("Animator already has an animation named: " + name);

		Animations.Add(name, new Animation(Texture, GetFrames(framesIndexes), duration, infinite));

		if(Animations.Count == 1)
		{
			CurrentAnimationName = name;
		}
	}

	public void AddAnimation(string name, int[] framesIndexes, float[] durations, bool infinite=false)
	{
		if(Animations.ContainsKey(name))
			throw new Exception("Animator already has an animation named: " + name);

		if(framesIndexes.Length != durations.Length)
			throw new Exception("the amount of frames and durations elements is not equal");

		Animations.Add(name, new Animation(Texture, GetFrames(framesIndexes), durations, infinite));

		if(Animations.Count == 1)
		{
			CurrentAnimationName = name;
		}
	}

	public void PlayAnimation(string animationName)
	{
		if(!Animations.ContainsKey(animationName))
			throw new KeyNotFoundException("Animator haven't an animation named: " + animationName);

		if(animationName == CurrentAnimationName)
			return;

		CurrentAnimationName = animationName;
		CurrentAnimation.Restart();
	}

	public void Reset()
	{
		CurrentAnimation.Restart();
	}

	public void Stop()
	{
		CurrentAnimation.IsRunning = false;
	}

	public void Play()
	{
		CurrentAnimation.IsRunning = true;
	}

    public override void Attached(Entity entity)
    {
        base.Attached(entity);

		Transform.Parent = entity.Transform;
    }

    public override void Distach()
    {
        base.Distach();

		Transform.Parent = null;
    }

	public override void Update(float dt)
	{
		CurrentAnimation.Update(dt);
	}

	public override void Draw()
	{
		Engine.SpriteBatch.Draw(Texture,
				Transform.GlobalPosition,
				CurrentAnimation.CurrentFrame.SourceRectangle,
				Color,
				Transform.GlobalRotation,
				Origin,
				Transform.GlobalScale,
				Flip,
				Depth);
	}

	private Rectangle[] GetFrames(params int[] indexes)
	{
		Rectangle[] frames = new Rectangle[indexes.Length];

		for(int i=0; i<frames.Length; i++)
		{
			frames[i] = _frames[indexes[i]];
		}

		return frames;

	}

	protected override void Dispose(bool disposable)
	{
		base.Dispose(disposable);

		foreach(var anim in Animations.Values)
		{
			anim.Dispose();
		}

		Animations.Clear();

		Animations = null;

		_frames = null;

		Transform = null;
	}
}
