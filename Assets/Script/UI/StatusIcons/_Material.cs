using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Image = Unity.UIWidgets.ui.Image;

namespace TetrisApp
{
    public class _Material : StatelessWidget
    {
        public Size Size;

        public Size SrcSize;

        public Offset SrcOffset;

        public _Material(Size size, Size srcSize, Offset srcOffset)
        {
            Size = size;
            SrcSize = srcSize;
            SrcOffset = srcOffset;
        }

        public override Widget build(BuildContext context)
        {
            return new CustomPaint(
                foregroundPainter: new _MaterialPainter(
                    SrcOffset, SrcSize, GameMaterial.GetMaterial(context)
                ),
                child:
                SizedBox.fromSize(size: Size)
            );
        }
    }

    public class _MaterialPainter : CustomPainter
    {
        private Offset mOffset;
        private Size   mSize;
        private Image  mMaterial;

        public _MaterialPainter(Offset offset, Size size, Image material)
        {
            mOffset = offset;
            mSize = size;
            mMaterial = material;
        }


        public void addListener(VoidCallback listener)
        {
        }

        public void removeListener(VoidCallback listener)
        {
        }

        Paint mPaint = new Paint();

        public void paint(Canvas canvas, Size size)
        {
            var src = Rect.fromLTWH(mOffset.dx, mOffset.dy, mSize.width, mSize.height);
            canvas.scale(size.width / mSize.width, size.height / mSize.height);
            canvas.drawImageRect(mMaterial, src, Rect.fromLTWH(0, 0, mSize.width, mSize.height), mPaint);
        }

        public bool shouldRepaint(CustomPainter oldDelegate)
        {
            var oldPainter = oldDelegate as _MaterialPainter;
            return oldPainter.mOffset != mOffset || oldPainter.mSize != mSize;
        }

        public bool? hitTest(Offset position)
        {
            return true;
        }
    }
}