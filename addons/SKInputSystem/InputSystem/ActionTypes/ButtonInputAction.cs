using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using static InputSystem.InputActionState;

namespace InputSystem
{
    [GlobalClass, Tool]
    public partial class ButtonInputAction : InputAction
    {
        [Export]
        public Array<string> Events
        {
            get
            {
                return new Array<string>(enumEvents.Select(e => InputEventHandler.ParseEnum(e)));
            }
            set
            {

                enumEvents = value.Select(e =>
                {
                    var a = e.Split(" ");
                    return (Enum)Enum.Parse(InputEventHandler.GetInputEnumType(a[0]), a[1]);
                }).ToList();
            }
        }

        public List<Enum> enumEvents = new();

        InputActionState _state = new(PressState.None, 0, 0);

        public ButtonInputAction()
        {
            Name = "";
            enumEvents = new();
        }

        public ButtonInputAction(string name)
        {
            Name = name;
            enumEvents = new();
        }

        public ButtonInputAction(string name, List<Enum> _events)
        {
            Name = name;
            enumEvents = _events;
        }

        public override InputActionState UpdateAndGetState(InputEvent @event, int subActionIndex)
        {
            if(_Sleeping) return _state;

            float stg = (float)InputEventHandler.EventGetStrenth(@event);

            if (@event.IsPressed() && stg > DeadZone)
            {
                if (_state.state > 0)
                {
                    _state.state = PressState.Holding;
                    _state.timeHeld = Time.GetTicksMsec() - TimeSinceLastPress;
                }
                else
                {
                    _state.state = PressState.JustPressed;
                    TimeSinceLastPress = Time.GetTicksMsec();
                    _state.timeHeld = 0;
                }

                _state.strength = stg;

            }
            else if (_state.state <= 0)
            {
                _state.state = PressState.None;
                _state.strength = 0;

                _state.timeHeld = Time.GetTicksMsec() - TimeSinceLastPress;
            }
            else
            {
                _state.state = PressState.Released;
                _state.strength = 0;

                _state.timeHeld = 0;
            }

            //GD.Print(_state.state + " with strength " + _state.strength + " -- " + subActionIndex);

            return _state;
        }

        public override void Sleep()
        {
            _Sleeping = true;
            _state.state = PressState.None;
            _state.strength = 0;
            _state.timeHeld = 0;
        }

        public override List<List<Enum>> GetEvents()
        {
            return new List<List<Enum>>() { enumEvents };
        }

        public override List<string> GetEventNames()
        {
            return new List<string>() { Name };
        }
    }
}