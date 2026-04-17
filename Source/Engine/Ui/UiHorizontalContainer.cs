using System;

namespace SapukayaEngine;

public sealed class UiHorizontalContainer : UiContainer
{
	private float GetChildrenListWidth()
	{
		float w = 0f;

		foreach(var child in Children)
		{
			w += child.Width + Gap;
		}

		return w;
	}

    protected override void ArrangeChildren()
    {
        throw new NotImplementedException();
    }

    protected override void ArrangeNewChild(UiElement child)
    {
		child.Transform.LocalPosition.X += GetChildrenListWidth();
    }
}
