using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SapukayaEngine;

namespace RedRanger;

public class Game1 : GameManager
{
    public Game1() : base(1280, 720, "Red Ranger", false, true)
    {
		SetVirtualSize(640, 360);
    }

    protected override void Initialize()
    {
        base.Initialize();

		Globals.Initialize();

		var layer = AddLayer<GameLayer>();

		layer.ChangeScene<GameScene>();

		AddLayer<UiLayer>();

		if(DebugMode)
			AddLayer<DebugLayer>();
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}
