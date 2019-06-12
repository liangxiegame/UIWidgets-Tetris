using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class Brick : StatelessWidget
    {
        private readonly Color mColor;

        public static Brick Empty()
        {
            return new Brick(AppConstants.BRICK_NULL);
        }

        public static Brick Normal()
        {
            return new Brick(AppConstants.BRICK_NORMAL);
        }

        public static Brick Highlight()
        {
            return new Brick(AppConstants.BRICK_HIGHLIGHT);
        }

        public Brick(Color color)
        {
            mColor = color;
        }

        public override Widget build(BuildContext context)
        {
            var width = 20;

            return SizedBox.fromSize(
                size: new Size(width, width),
                child: new Container(
                    margin: EdgeInsets.all(width * 0.05f),
                    padding: EdgeInsets.all(width * 0.1f),
                    decoration: new BoxDecoration(
                        border: Border.all(
                            color: mColor,
                            width: width * 0.1f
                        )
                    ),
                    child: new Container(
                        color: mColor
                    )
                )
            );
        }
    }
}