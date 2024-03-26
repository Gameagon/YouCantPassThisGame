using Godot;

public partial class OptionsSavesHandler : Control
{
    public static OptionsSavesHandler Current;

    public OptionsSave options;
    public string path = "user://options_data.res";

    public override void _EnterTree()
    {
        Current = this;

		if(ResourceLoader.Exists(path))
		{
        	options = ResourceLoader.Load<OptionsSave>(path);
		}
		else
		{
        	options = new OptionsSave();
		}
    }

    public override void _ExitTree()
    {
        if (Current == this) Current = null;

        base._ExitTree();
    }

    public void Save()
	{
        ResourceSaver.Save(options, path);
    }

	public void SetValue(string key, Variant value)
	{
        options.SetValue(key, value);
        Save();
    }

	public object GetValue(string key)
	{
        return options.GetValue(key);
    }
}
