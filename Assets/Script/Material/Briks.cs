using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TerisGame
{
    public class BrikSize : InheritedWidget
    {
        public static Color COLOR_NORMAL    = Colors.black87;
        public static Color COLOR_NULL      = Colors.black12;
        public static Color COLOR_HIGHLIGHT = new Color(0xFF560000);

        public BrikSize(Size size, Widget child) : base(child: child)
        {
            Size = size;
        }

        public Size Size;

        public static BrikSize Of(BuildContext context)
        {
            var brikSize = context.inheritFromWidgetOfExactType(typeof(BrikSize)) as BrikSize;
            D.assert(brikSize != null, () => "....");
            return brikSize;
        }


        public override bool updateShouldNotify(InheritedWidget oldWidget)
        {
            var old = oldWidget as BrikSize;
            return old.Size != Size;
        }
    }

    public class Brik : StatelessWidget
    {
        public Color Color;

        public static Brik _(Color color)
        {
            return new Brik
            {
                Color = color
            };
        }

        public static Brik Normal()
        {
            return _(BrikSize.COLOR_NORMAL);
        }

        public static Brik Empty()
        {
            return _(BrikSize.COLOR_NULL);
        }

        public static Brik Highlight()
        {
            return _(BrikSize.COLOR_HIGHLIGHT);
        }

        public override Widget build(BuildContext context)
        {
            var width = BrikSize.Of(context).Size.width;
            return SizedBox.fromSize(
                size: BrikSize.Of(context).Size,
                child: new Container(
                    margin: EdgeInsets.all(0.05f * width),
                    padding: EdgeInsets.all(0.1f * width),
                    decoration: new BoxDecoration(
                        border: Border.all(width: 0.10f * width, color: Color)),
                    child: new Container(
                        color: Color
                    )
                )
            );
        }
    }
}