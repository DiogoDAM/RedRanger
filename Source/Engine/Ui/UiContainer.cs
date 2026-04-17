
using Microsoft.Xna.Framework;

namespace SapukayaEngine;

public abstract class UiContainer : UiElement 
{
	public float Gap;

    public override void AddChild(UiElement child)
    {
        base.AddChild(child);

		ArrangeNewChild(child);
    }

	protected abstract void ArrangeNewChild(UiElement child);

	protected abstract void ArrangeChildren();
}
