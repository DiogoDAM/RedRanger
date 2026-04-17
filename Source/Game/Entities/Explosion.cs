using SapukayaEngine;

using Microsoft.Xna.Framework;

namespace RedRanger;

public sealed class Explosion : Entity
{
	private Animation _animation;

	public Explosion(Vector2 pos) : base()
	{
		Transform.LocalPosition = pos;
	}

    public override void Added()
    {
		_animation = new(Globals.GameAtlas.CreateHSprite("small_explosion"), new float[]{0.05f, 0.1f, 0.1f});
		_animation.AnimationEnded += OnAnimationEnded;
		Add<Animation>(_animation);
    }

	private void OnAnimationEnded()
	{
		Destroy();
	}
}
