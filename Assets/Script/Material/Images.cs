using System;
using System.Collections.Generic;
using Unity.UIWidgets.async;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Image = Unity.UIWidgets.ui.Image;

namespace TerisGame
{
    public class Number : StatelessWidget
    {
        public static Size DIGITAL_ROW_SIZE = new Size(14, 24);

        public int Length;

        public int? Num;

        public bool PadWithZero;

        public Number(int? num = null, int length = 5, bool padWithZero = false)
        {
            Length = length;
            Num = num;
            PadWithZero = padWithZero;
        }


        public override Widget build(BuildContext context)
        {
            string digitalStr = Num?.ToString() ?? "";

            if (digitalStr.Length > Length)
            {
                digitalStr = digitalStr.Substring(digitalStr.Length - Length);
            }

            digitalStr = digitalStr.PadLeft(Length, PadWithZero ? '0' : ' ');

            var children = new List<Widget>();

            for (var i = 0; i < Length; i++)
            {
                var number = digitalStr[i].ToString();

                children.Add(
                    new Digital(digital: string.IsNullOrWhiteSpace(number) ? null as int? : int.Parse(number)));
            }

            return new Row(
                mainAxisSize: MainAxisSize.min,
                children: children
            );
        }
    }

    public class IconDragon : StatefulWidget
    {
        public bool Animate;

        public IconDragon(bool animate)
        {
            Animate = animate;
        }

        public override State createState()
        {
            return new IconDragonState();
        }
    }

    public class IconDragonState : State<IconDragon>
    {
        private Timer mTimer;


        public override void didUpdateWidget(StatefulWidget oldWidget)
        {
            base.didUpdateWidget(oldWidget);
            InitAnimation();
        }

        private int mFrame = 0;

        public override void initState()
        {
            base.initState();
            InitAnimation();
        }

        void InitAnimation()
        {
            mTimer?.cancel();
            mTimer = null;

            if (!widget.Animate)
            {
                return;
            }

            mTimer = Window.instance.periodic(TimeSpan.FromMilliseconds(200), () =>
            {
                if (mFrame > 30)
                {
                    mFrame = 0;
                }

                setState(() => { mFrame++; });
            });
        }

        public override void dispose()
        {
            mTimer?.cancel();
            mTimer = null;

            base.dispose();
        }

        public override Widget build(BuildContext context)
        {
            return new _Material(
                size: new Size(80, 86),
                srcSize: new Size(80, 86),
                srcOffset: getOffset(mFrame)
            );
        }

        Offset getOffset(int frame)
        {
            int index = 0;
            if (frame < 10)
            {
                index = frame % 2 == 0 ? 0 : 1;
            }
            else
            {
                index = frame % 2 == 0 ? 2 : 3;
            }

            var dx = index * 100.0f;

            return new Offset(dx, 100);
        }
    }

    public class IconPause : StatelessWidget
    {
        private bool mEnable;
        private Size mSize = new Size(18, 16);

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

    public class IconSound : StatelessWidget
    {
        public bool Enable;
        public Size Size = new Size(18, 16);

        public IconSound(bool enable = true, Size size = null)
        {
            Enable = enable;
            Size = size ?? Size;
        }

        public override Widget build(BuildContext context)
        {
            return new _Material(
                size: Size,
                srcSize: new Size(25, 21),
                srcOffset: Enable ? new Offset(150, 75) : new Offset(175, 75)
            );
        }
    }

    public class IconColon : StatelessWidget
    {
        private bool mEnable;

        private Size mSize = new Size(18, 16);

        public IconColon(bool enable = true, Size size = null)
        {
            mEnable = enable;
            mSize = size;
        }

        public override Widget build(BuildContext context)
        {
            return new _Material(
                size: mSize,
                srcOffset: mEnable ? new Offset(229, 25) : new Offset(243, 25),
                srcSize: Number.DIGITAL_ROW_SIZE
            );
        }
    }


    public class Digital : StatelessWidget
    {
        public int? digital;

        public Size size;

        public Digital(int? digital)
        {
            this.digital = digital;
            D.assert(digital == null || (digital <= 9 && digital >= 0));
        }

        public override Widget build(BuildContext context)
        {
            return new _Material(
                size: size,
                srcOffset: getDigitalOffset(),
                srcSize: Number.DIGITAL_ROW_SIZE
            );
        }

        Offset getDigitalOffset()
        {
            var offset = digital ?? 10;
            var dx = 75.0f + 14 * offset;
            return new Offset(dx, 25);
        }
    }

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
                foregroundPainter: new MaterialPainter(
                    SrcOffset, SrcSize, GameMaterial.getMaterial(context)
                ),
                child: SizedBox.fromSize(
                    size: Size
                )
            );
        }
    }

    public class MaterialPainter : CustomPainter
    {
        private Offset mOffset;
        private Size mSize;
        private Image mMaterial;

        public MaterialPainter(Offset offset, Size size, Image material)
        {
            mOffset = offset;
            mSize = size;
            mMaterial = material;
        }

        Paint mPaint = new Paint();


        public void paint(Canvas canvas, Size size)
        {
            var src =
                Rect.fromLTWH(mOffset.dx, mOffset.dy, this.mSize.width, this.mSize.height);
            canvas.scale(size.width / this.mSize.width, size.height / this.mSize.height);
            canvas.drawImageRect(mMaterial, src,
                Rect.fromLTWH(0, 0, this.mSize.width, this.mSize.height), mPaint);
        }

        public bool shouldRepaint(CustomPainter oldDelegate)
        {
            var oldPainter = oldDelegate as MaterialPainter;
            return oldPainter.mOffset != mOffset || oldPainter.mSize != mSize;
        }

        public bool? hitTest(Offset position)
        {
            return true;
        }

        public void addListener(VoidCallback listener)
        {
        }

        public void removeListener(VoidCallback listener)
        {
        }
    }
}