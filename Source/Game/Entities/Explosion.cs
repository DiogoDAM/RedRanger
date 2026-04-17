using SapukayaEngine;

using Microsoft.Xna.Framework;

namespace RedRanger;

public enum ExplosionType
{
	Small,
	Big,
}

public sealed class Explosion : Entity
{
	private Animation _animation;
	private ExplosionType _type;

	public Explosion(Vector2 pos, ExplosionType type) : base()
	{
		Transform.LocalPosition = pos;
		_type = type;
	}

    public override void Added()
    {
		if(_type == ExplosionType.Small)
			_animation = new(Globals.GameAtlas.CreateHSprite("small_explosion"), new float[]{0.05f, 0.1f, 0.1f});
		else if(_type == ExplosionType.Big)
			_animation = new(Globals.GameAtlas.CreateHSprite("big_explosion"), new float[]{0.05f, 0.05f, 0.05f, 0.1f, 0.1f, 0.1f});

		_animation.AnimationEnded += OnAnimationEnded;
		Add<Animation>(_animation);
    }

	private void OnAnimationEnded()
	{
		Destroy();
	}
}
