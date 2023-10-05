using Godot;
using System;
using System.Diagnostics;

[GlobalClass]
public partial class TestComponent : ComponentBehaviour
{
    [Export] public int num = 0;

    public override void Ready()
    {
        Debug.Print("Hola");
    }
}
