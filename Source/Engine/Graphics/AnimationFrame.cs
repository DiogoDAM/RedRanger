using Microsoft.Xna.Framework;

namespace SapukayaEngine;

public sealed class AnimationFrame
{
	public float Duration;
	public Rectangle SourceRectangle;

	public AnimationFrame(Rectangle srcRect, float duration)
	{
		SourceRectangle = srcRect;
		Duration = duration;
	}
}
