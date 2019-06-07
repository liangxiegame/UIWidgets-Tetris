using Unity.UIWidgets.widgets;

namespace TerisGame
{
    /// <summary>
    /// 使用 QFramework 的 AudioManager
    /// </summary>
    public class Sound : StatefulWidget
    {
        public Widget Child;

        public Sound(Widget child)
        {
            Child = child;
        }

        public static string[] SOUNDS = {
            "clean.mp3",
            "drop.mp3",
            "explosion.mp3",
            "move.mp3",
            "rotate.mp3",
            "start.mp3"
        };
            
        public override State createState()
        {
            return new SoundState();
        }
    }

    class SoundState : State<Sound>
    {
        public override Widget build(BuildContext context)
        {
            return widget.Child;
        }
        
        public void Start() {}
        public void Clear() {}
        public void Fall() {}
        public void Rotate() {}
        public void Move() {}
    }
}