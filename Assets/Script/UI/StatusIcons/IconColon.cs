using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class IconColon:StatelessWidget
    {
        private bool mEnable = false;
        private Size mSize   = new Size(10, 17);

        public IconColon(bool enable = true, Size size = null)
        {
            mEnable = enable;
            mSize = size ?? mSize;
        }

        public override Widget build(BuildContext context)
        {
            return new _Material(
                size: mSize,
                srcSize: new Size(14, 24),
                srcOffset: mEnable ? new Offset(229, 25) : new Offset(243, 25)
            );
        }
    }
}