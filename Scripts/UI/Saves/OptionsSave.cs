using Godot;
using Godot.Collections;
using System;

public partial class OptionsSave : Resource
{
    [Export]
    public Dictionary<string, Variant> Options = new Dictionary<string, Variant>();

    public void SetValue(string key, Variant value)
    {
        if(Options.ContainsKey(key))
            Options[key] = value;
        else
            Options.Add(key, value);
    }

    public object GetValue(string key)
    {
        if(Options.ContainsKey(key))
            return Options[key];
        else
            return null;
    }
}
