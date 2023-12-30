using Godot;
using System;

[GlobalClass, Tool]
public partial class EventActuator : Node
{
	[Signal]
	public delegate void onInvokeEventHandler();

	public void Invoke(params Variant[] args)
	{
		EmitSignal(SignalName.onInvoke, args);
	}
}
