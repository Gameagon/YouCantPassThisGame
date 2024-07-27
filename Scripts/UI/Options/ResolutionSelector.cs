using Godot;
using System;

public partial class ResolutionSelector : OptionButton
{
    // Called when the node enters the scene tree for the first time.
    [Export] 
    public string key = "";

    [Export]
    public string resolutionDefault;

    [Signal]
     public delegate void ResolutionEventHandler(Vector2I resolution);
    public override void _EnterTree()
    {
        ItemSelected += OnItemSelected;
        SetResolution();
    }
    public void SetResolution()
    {

        string currentLocale;
        GD.Print(OptionsSavesHandler.Current.GetValue(key)?.ToString());
        currentLocale = OptionsSavesHandler.Current.GetValue(key)?.ToString() ?? resolutionDefault;
        //TransformtoStringRes(DisplayServer.ScreenGetSize());
        Vector2I res = TransformtoVectorRes(currentLocale);
        GD.Print("patata");
        for (int i = 0; i < ItemCount; i++)
        {
            // AddItem(GetItemText(i), i);
            SetItemMetadata(i, GetItemText(i));

            if (currentLocale == GetItemText(i))
                Select(i);
        }

        EmitSignal(SignalName.Resolution, res);

    }
    public string TransformtoStringRes(Vector2 resolution)
    {
        return string.Concat(resolution[0],"x",resolution[1]);
    }
    public Vector2I TransformtoVectorRes(string resolution)
    {
        string[] HxV = resolution.Split('x');
        return new Vector2I(Convert.ToInt32(HxV[0]), Convert.ToInt32(HxV[1]));
    }
    public void OnItemSelected(long index)
    {
        var l = (string)GetSelectedMetadata();
        OptionsSavesHandler.Current.SetValue(key, l);
        SetResolution();
    }

}
