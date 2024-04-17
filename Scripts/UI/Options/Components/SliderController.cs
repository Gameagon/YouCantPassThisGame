using Godot;
using System.Diagnostics;
using static Godot.Slider;

namespace Options
{
    public partial class SliderController : Control
    {
        [Export]
        public string key;

        [Export]
        public float defaultValue;

        [Export]
        public SpinBox spinBox;

        [Export]
        public Slider slider;

        [Export]
        public float multyplier = 100;

        public override void _EnterTree()
        {
            base._EnterTree();

            double value = OptionsSavesHandler.Current.GetValue(key)?.As<double>() ?? defaultValue;

            slider.SetValueNoSignal(value);
            if (spinBox != null)
            {
                spinBox.ValueChanged += SetValue;
                spinBox.Value = value * multyplier;
            }

            slider.ValueChanged += HandleValueChanged;

            slider.DragEnded += HandleDragEnded;
        }

        private void HandleDragEnded(bool valueChanged)
        {
            if (valueChanged)
                OptionsSavesHandler.Current.SetValue(key, slider.Value);
        }

        private void HandleValueChanged(double value)
        {
            spinBox?.SetValueNoSignal(value * multyplier);
        }

        private void SetValue(double value)
        {

            slider.Value = value / multyplier;
            OptionsSavesHandler.Current.SetValue(key, slider.Value);


        }
    }
}

