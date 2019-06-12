using System;
using Unity.UIWidgets.service;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TetrisApp
{
    public class KeyboardController : StatefulWidget
    {
        public KeyboardController(Widget child)
        {
            Child = child;
        }

        public Widget Child { get; }

        public override State createState()
        {
            return new KeyboardControllerState();
        }
    }

    public class KeyboardControllerState : State<KeyboardController>
    {
        public override void initState()
        {
            base.initState();

            RawKeyboard.instance.addListener(OnKey);
        }

        void OnKey(RawKeyEvent @event)
        {
            if (@event is RawKeyUpEvent)
            {
                return;
            }

            var keyCode = @event.data.unityEvent.keyCode;

            if (keyCode == KeyCode.W || keyCode == KeyCode.UpArrow)
            {
                Game.of(context).Up();
            }
            else if (keyCode == KeyCode.A || keyCode == KeyCode.LeftArrow)
            {
                Game.of(context).Left();
            }
            else if (keyCode == KeyCode.S || keyCode == KeyCode.DownArrow)
            {
                Game.of(context).Down();
            }
            else if (keyCode == KeyCode.D || keyCode == KeyCode.RightArrow)
            {
                Game.of(context).Right();
            }
        }

        public override void dispose()
        {
            base.dispose();

            RawKeyboard.instance.removeListener(OnKey);
        }

        public override Widget build(BuildContext context)
        {
            return widget.Child;
        }
    }
}