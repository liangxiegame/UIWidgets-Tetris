using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class IconPause : StatelessWidget
    {
        private bool mEnable = false;
        private Size mSize   = new Size(18, 16);

        public IconPause(bool enable = true, Size size = null)
        {
            mEnable = enable;
            mSize = size ?? mSize;
        }

        public override Widget build(BuildContext context)
        {
            return new _Material(
                size: mSize,
                srcSize: new Size(20, 18),
                srcOffset: mEnable ? new Offset(75, 75) : new Offset(100, 75)
            );
        }
    }
}