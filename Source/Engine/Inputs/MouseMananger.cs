using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SapukayaEngine;

public sealed class MouseManager 
{
	private MouseState _prev;
	private MouseState _curr;

	public int ScrollWheelValue => _curr.ScrollWheelValue;

	public bool IsScrollWheelMoveUpwards => _curr.ScrollWheelValue > _prev.ScrollWheelValue;

	public bool IsScrollWheelMoveDown => _curr.ScrollWheelValue < _prev.ScrollWheelValue;

	public Vector2 Position => new Vector2(_curr.X, _curr.Y);

	public bool IsMouseCursorMoved => (_prev.Position - _curr.Position) != Point.Zero;

	public MouseManager()
	{
		_prev = Mouse.GetState();
		_curr = Mouse.GetState();
	}

	public void Update()
	{
		_prev = _curr;
		_curr = Mouse.GetState();
	}

	public bool IsButtonDown(byte button)
	{
		switch (button)
		{
			case 0: return _curr.LeftButton == ButtonState.Pressed;
			case 1: return _curr.RightButton == ButtonState.Pressed;
			case 2: return _curr.MiddleButton == ButtonState.Pressed;
			default: throw new NotImplementedException("Button value not implemented, only values (0, 1, 2)");
		}
	}

	public bool IsButtonUp(byte button)
	{
		switch (button)
		{
			case 0: return _curr.LeftButton == ButtonState.Released;
			case 1: return _curr.RightButton == ButtonState.Released;
			case 2: return _curr.MiddleButton == ButtonState.Released;
			default: throw new NotImplementedException("Button value not implemented, only values (0, 1, 2)");
		}
	}

	public bool IsButtonPressed(byte button)
	{
		switch (button)
		{
			case 0: return _curr.LeftButton == ButtonState.Pressed && _prev.LeftButton == ButtonState.Released;
			case 1: return _curr.RightButton == ButtonState.Pressed && _prev.RightButton == ButtonState.Released;
			case 2: return _curr.MiddleButton == ButtonState.Pressed && _prev.MiddleButton == ButtonState.Released;
			default: throw new NotImplementedException("Button value not implemented, only values (0, 1, 2)");
		}
	}

	public bool IsButtonReleased(byte button)
	{
		switch (button)
		{
			case 0: return _curr.LeftButton == ButtonState.Released && _prev.LeftButton == ButtonState.Pressed;
			case 1: return _curr.RightButton == ButtonState.Released && _prev.RightButton == ButtonState.Pressed;
			case 2: return _curr.MiddleButton == ButtonState.Released && _prev.MiddleButton == ButtonState.Pressed;
			default: throw new NotImplementedException("Button value not implemented, only values (0, 1, 2)");
		}
	}

}
