using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TerisGame
{
    public class Number : StatelessWidget
    {
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
            return new Text("IconSound");
        }
    }


    public class Digital : StatelessWidget
    {
        public int? digital;

        public Size size;

        public Digital(int? digital)
        {
            this.digital = digital;
        }

        public override Widget build(BuildContext context)
        {
            return new Text(digital?.ToString() ?? " ");
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
                foregroundPainter: new MaterialPainter()
            );
        }
    }

    public class MaterialPainter : CustomPainter
    {
        public void paint(Canvas canvas, Size size)
        {
        }

        public bool shouldRepaint(CustomPainter oldDelegate)
        {
            return true;
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