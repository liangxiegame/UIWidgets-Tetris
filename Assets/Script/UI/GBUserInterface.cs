using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class GBUserInterface : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            var size = MediaQuery.of(context).size;

            var screenWidth = size.width * 0.8f;

            return new Game(
                child: new KeyboardController(
                    child: SizedBox.expand(
                        child: new Container(
                            color: AppConstants.APP_BACKGROUND_COLOR,
                            child: new Padding(
                                padding: MediaQuery.of(context).padding,
                                child: new Column(
                                    children: new List<Widget>
                                    {
                                        new Spacer(),
                                        new ScreenDecoration(child: new GBScreen(width: screenWidth)),
                                        new Spacer(flex: 2),
                                        new GBJoyStick()
                                    }
                                )
                            )
                        )
                    )
                )
            );
        }
    }

    public class ScreenDecoration : StatelessWidget
    {
        private Widget mChild;

        public ScreenDecoration(Widget child)
        {
            mChild = child;
        }

        public override Widget build(BuildContext context)
        {
            return new Container(
                decoration: new BoxDecoration(
                    border: Border.all(new Color(0xFF987f0f), width: 3.0f)
                ),
                child: new Container(
                    padding: EdgeInsets.all(3),
                    decoration: new BoxDecoration(
                        border: Border.all(Colors.black54)
                    ),
                    child: mChild
                )
            );
        }
    }
}