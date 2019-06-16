using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class GBButton : StatefulWidget
    {
        public Size         Size;
        public VoidCallback OnTap;
        public Widget       Icon;
        public Color        Color;
        public bool         EnableLongPress;

        public GBButton(
            Size size,
            VoidCallback onTap,
            Widget icon = null,
            Color color = null,
            bool enableLongPress = true)
        {
            Size = size;
            OnTap = onTap;
            Icon = icon;
            Color = color ?? Colors.blue;
            EnableLongPress = enableLongPress;
        }

        public override State createState()
        {
            return new GBButtonState();
        }
    }

    class GBButtonState : State<GBButton>
    {
        private Color mColor;

        public override void initState()
        {
            base.initState();
            mColor = widget.Color;
        }

        public override Widget build(BuildContext context)
        {
            return new Material(
                color: mColor,
                elevation: 2,
                shape: new CircleBorder(),
                child: new GestureDetector(
                    behavior: HitTestBehavior.opaque,
                    onTapDown: details => { widget.OnTap(); },
                    child: SizedBox.fromSize(
                        size: widget.Size
                    )
                )
            );
        }
    }
}