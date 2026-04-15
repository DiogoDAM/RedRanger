using Microsoft.Xna.Framework;

using System;

namespace SapukayaEngine;

public sealed class Timer : Component
{
	public float Time;

	public float TimeLeft { get; private set; }

	public delegate void TimeUpEventHandler();

	public event TimeUpEventHandler TimeUp;

	public bool IsRunning;

	public bool IsInfinite;

	public Timer(float time, bool infinite=false) 
	{
		Time = time;

		TimeLeft = Time;

		IsRunning = true;
		IsInfinite = infinite;
	}

    public override void Update(float dt)
    {
		if(IsRunning)
		{
			TimeLeft -= dt;
			
			if(TimeLeft <= 0f)
			{
				TimeUp?.Invoke();

				if(!IsInfinite)
				{
					IsRunning = false;
					return;
				}

				TimeLeft = Time;
			}
		}
    }

	public void Pause() => IsRunning = false;

	public void Play() => IsRunning = true;

	public void Restart()
	{
		TimeLeft = Time;
	}
}
