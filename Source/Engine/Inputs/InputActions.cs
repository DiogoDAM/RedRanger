using System;
using System.Collections.Generic;

namespace SapukayaEngine;

public sealed class InputAction
{
	public List<IInputAction> Actions { get; set; }

	public string Name { get; set; }
}

public sealed class InputActions
{
	public List<InputAction> Actions { get; set; }
}


