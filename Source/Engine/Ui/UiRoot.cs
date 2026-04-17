using System;

namespace SapukayaEngine;

public class UiRoot : UiElement
{
	public override int Width => GameManager.Instance.VirtualWidth;
	public override int Height => GameManager.Instance.VirtualHeight;
}
