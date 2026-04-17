using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SapukayaEngine;

public sealed class Animation : DrawableComponent
{
	public readonly Texture2D Texture;

	public readonly AnimationFrame[] Frames;

	private int CurrentFrameIndex;

	public AnimationFrame CurrentFrame => Frames[CurrentFrameIndex];

	public float CurrentFrameDuration => CurrentFrame.Duration;

	public float DurationLeft { get; private set; }

	public bool IsRunning { get; set; }
	public bool IsInfinite { get; set; }

	public delegate void AnimationEndedEventHandler();
	public event AnimationEndedEventHandler AnimationEnded;

	public Transform Transform { get; private set; }

	public Animation(Texture2D texture, Rectangle[] sourceRectangles, float duration, bool infinite=false)
	{
		Texture = texture;

		Frames = new AnimationFrame[sourceRectangles.Length];
		for(int i=0; i<Frames.Length; i++)
		{
			Frames[i] = new AnimationFrame(sourceRectangles[i], duration);
		}

		CurrentFrameIndex = 0;
		DurationLeft = CurrentFrameDuration;

		IsRunning = true;
		IsInfinite = infinite;

		Transform = new();
	}

	public Animation(HSprite sprite, float duration, bool infinite=false)
	{
		Texture = sprite.Texture;

		Frames = new AnimationFrame[sprite.Frames.Length];
		for(int i=0; i<Frames.Length; i++)
		{
			Frames[i] = new AnimationFrame(sprite.Frames[i], duration);
		}

		CurrentFrameIndex = 0;
		DurationLeft = CurrentFrameDuration;

		IsRunning = true;
		IsInfinite = infinite;

		Transform = new();

		Width = sprite.Width;
		Height = sprite.Height;
	}

	public Animation(Texture2D texture, Rectangle[] sourceRectangles, float[] durations, bool infinite=false)
	{
		Texture = texture;

		if(sourceRectangles.Length != durations.Length)
			throw new Exception("The number of frames is not same as the duration of the frames");

		Frames = new AnimationFrame[sourceRectangles.Length];
		for(int i=0; i<Frames.Length; i++)
		{
			Frames[i] = new AnimationFrame(sourceRectangles[i], durations[i]);
		}

		CurrentFrameIndex = 0;
		DurationLeft = CurrentFrameDuration;

		IsRunning = true;
		IsInfinite = infinite;

		Transform = new();
	}

	public Animation(HSprite sprite, float[] durations, bool infinite=false)
	{
		Texture = sprite.Texture;

		if(sprite.Frames.Length != durations.Length)
			throw new Exception("The number of frames is not same as the duration of the frames");

		Frames = new AnimationFrame[sprite.Frames.Length];
		for(int i=0; i<Frames.Length; i++)
		{
			Frames[i] = new AnimationFrame(sprite.Frames[i], durations[i]);
		}

		CurrentFrameIndex = 0;
		DurationLeft = CurrentFrameDuration;

		IsRunning = true;
		IsInfinite = infinite;

		Transform = new();

		Width = sprite.Width;
		Height = sprite.Height;
	}

	public void Restart()
	{
		CurrentFrameIndex = 0;
		IsRunning = true;
		DurationLeft = CurrentFrameDuration;
	}

	public void Stop()
	{
		IsRunning = false;
	}

	public void Resume()
	{
		IsRunning = true;
	}

    public override void Attached(Entity entity)
    {
        base.Attached(entity);
		
		Transform.Parent = Entity.Transform;
    }

    public override void Distach()
    {
        base.Distach();

		Transform.Parent = null;
    }

	public override void Update(float dt)
	{
		if(IsRunning)
		{
			DurationLeft -= dt;
			
			if(DurationLeft <= 0)
			{
				CurrentFrameIndex++;

				if(CurrentFrameIndex == Frames.Length)
				{
					AnimationEnded?.Invoke();

					if(!IsInfinite)
					{
						IsRunning = false;
						CurrentFrameIndex--;
						return;
					}
					else
						CurrentFrameIndex = 0;

				}

				DurationLeft = CurrentFrameDuration;
			}
		}
	}

	public override void Draw()
	{
		Engine.SpriteBatch.Draw(Texture,
				Transform.GlobalPosition,
				CurrentFrame.SourceRectangle,
				Color,
				Transform.GlobalRotation,
				Origin,
				Transform.GlobalScale,
				Flip,
				Depth);
	}

    protected override void Dispose(bool disposable)
    {
        base.Dispose(disposable);

		IsRunning = false;
		IsInfinite = false;

		AnimationEnded = null;

		Transform = null;
	}
}
