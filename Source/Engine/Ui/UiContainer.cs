using System;

namespace SapukayaEngine;

public abstract class UiContainer : UiElement 
{
	public float Gap;

    public override void AddChild(UiElement child)
	{
		if(child == null)
			throw new ArgumentNullException("UiElement child is null");

		ArrangeNewChild(child);

		Children.Add(child);
		child.Added(this);
		child.Resume();
		child.Start();
    }

	protected abstract void ArrangeNewChild(UiElement child);

	protected abstract void ArrangeChildren();
}
