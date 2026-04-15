using SapukayaEngine;

using Microsoft.Xna.Framework;

namespace RedRanger;

public sealed class Player : Entity
{ 
	public override void Added()
	{
		Add<HSprite>(Globals.GameAtlas.CreateHSprite("player"));

		Add<PlayerMovement>(new(200f));

		Add<PlayerShoot>(new(0.1f));
	}
}
