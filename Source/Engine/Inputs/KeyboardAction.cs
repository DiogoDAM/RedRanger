using System;
using Microsoft.Xna.Framework.Input;

namespace SapukayaEngine;

public sealed class KeyboardAction : IInputAction
{
	public Keys Key { get; set; }

	public Predicate<Keys> Predicate { get; private set; }

	public KeyboardAction(Keys key, Predicate<Keys> predicate)
	{
		Key = key;
		Predicate = predicate;
	}

	public bool IsInAction()
	{
		return Predicate(Key);
	}
}
