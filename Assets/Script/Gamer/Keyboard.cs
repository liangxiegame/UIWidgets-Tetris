using Unity.UIWidgets.service;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TerisGame
{
    public class KeyboardController :StatefulWidget
    {
        public Widget Child;

        public KeyboardController(Widget child)
        {
            Child = child;
        }
        
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


        void OnKey(RawKeyEvent e)
        {
            if (e is RawKeyUpEvent)
            {
                return;
            }

            var key = e.data.unityEvent.keyCode;

            var game = Game.Of(context);


            if (key == KeyCode.UpArrow || key == KeyCode.W)
            {
                game.Drop();
            } else if (key == KeyCode.DownArrow || key == KeyCode.S)
            {
                game.Down();
            }else if (key == KeyCode.LeftArrow || key == KeyCode.A)
            {
                game.Left();
            } else if (key == KeyCode.RightArrow || key == KeyCode.D)
            {
                game.Right();
            } else if (key == KeyCode.Space || key == KeyCode.K)
            {
                game.Rotate();
            } else if (key == KeyCode.P)
            {
                game.PauseOrResume();
            } else if (key == KeyCode.LeftShift || key == KeyCode.RightShift)
            {
//                game.soundSwitch();
            } else if (key == KeyCode.LeftControl || key == KeyCode.RightControl)
            {
                game.Reset();
            }
        }

        public override void dispose()
        {
            RawKeyboard.instance.removeListener(OnKey);
            base.dispose();
        }

        public override Widget build(BuildContext context)
        {
            return widget.Child;
        }
    }
}