using Unity.UIWidgets.foundation;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class Digital : StatelessWidget
    {
        private int? mDigital;

        private Size mSize = new Size(10, 17);

        public Digital(int? digital = null)
        {
            mDigital = digital;
            D.assert(mDigital == null || mDigital >= 0 && mDigital <= 9);
        }


        public override Widget build(BuildContext context)
        {
            var offset = mDigital ?? 10;
            var dx = 75.0f + offset * 14;

            return new _Material(
                size: mSize,
                srcOffset: new Offset(dx, 25),
                srcSize: new Size(14, 24)
            );
        }
    }
}