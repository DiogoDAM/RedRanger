using System;

namespace SapukayaEngine;

public sealed class UiVerticalContainer : UiContainer
{
	private float GetChildrenListHeight()
	{
		float h = 0f;

		foreach(var child in Children)
		{
			h += child.Height + Gap;
		}

		return h;
	}

    protected override void ArrangeChildren()
    {
        throw new NotImplementedException();
    }

    protected override void ArrangeNewChild(UiElement child)
    {
		child.Transform.LocalPosition.Y += GetChildrenListHeight();
    }
}
