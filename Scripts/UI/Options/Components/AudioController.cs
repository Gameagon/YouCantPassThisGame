using Godot;
using System;

public partial class AudioController : AudioStreamPlayer
{
    [Export]
    private string VolumeKey;

    [Export]
    public string BusName;

    [Export]
    public int DefaultVolume;


    int AudioIndex;
    public override void _EnterTree()
    {
        base._EnterTree();

        AudioIndex = AudioServer.GetBusIndex(BusName);
        AudioServer.SetBusVolumeDb(AudioIndex, Mathf.LinearToDb(OptionsSavesHandler.Current.GetValue(VolumeKey)?.As<float>() ?? Mathf.DbToLinear(DefaultVolume)));
        OptionsSavesHandler.Current.onOptionsChanged += SetVolume;
    }
    public void SetVolume(string key, Variant value) 
    {
        if (key == VolumeKey)
            AudioServer.SetBusVolumeDb(AudioIndex, Mathf.LinearToDb(value.As<float>()));
        GD.Print(Mathf.LinearToDb(value.As<float>()));
    }
}
