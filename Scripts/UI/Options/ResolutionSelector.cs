using Godot;
using System;

public partial class ResolutionSelector : OptionButton
{
    // Called when the node enters the scene tree for the first time.
    string key = "RESOLUTION";

    [Export]
    public string resolutionDefault;
    public override void _EnterTree()
    {
        ItemSelected += OnItemSelected;
        SetResolution();
    }
    public void SetResolution()
    {
        string currentLocale;
        currentLocale = OptionsSavesHandler.Current.GetValue(key)?.ToString() ?? TransformtoStringRes(DisplayServer.ScreenGetSize());
        Vector2I res = TransformtoVectorRes(currentLocale);
        for (int i = 0; i < ItemCount; i++)
        {
            // AddItem(GetItemText(i), i);
            SetItemMetadata(i, GetItemText(i));

            if (currentLocale == GetItemText(i))
                Select(i);
        }
        GD.Print(DisplayServer.WindowGetSize());
        GetWindow().ContentScaleSize = res;
        DisplayServer.WindowSetSize(res,0);
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
