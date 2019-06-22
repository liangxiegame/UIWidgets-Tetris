using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class IconSound : StatelessWidget
    {
        private bool mEnable = false;
        private Size mSize   = new Size(18, 16);

        public IconSound(bool enable = true, Size size = null)
        {
            mEnable = enable;
            mSize = size ?? mSize;
        }

        public override Widget build(BuildContext context)
        {
            return new _Material(
                size: mSize,
                srcSize: new Size(25, 21),
                srcOffset: mEnable ? new Offset(150, 75) : new Offset(175, 75)
            );
        }
    }
}