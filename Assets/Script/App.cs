using Unity.UIWidgets.engine;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class App : UIWidgetsPanel
    {
        protected override Widget createWidget()
        {
            return new Container(
                color: AppConstants.SCREEN_BACKGROUND_COLOR,
                child: SizedBox.fromSize(
                    size: new Size(100, 200),
                    child: new Container(
                        padding: EdgeInsets.all(2),
                        decoration: new BoxDecoration(
                            border: Border.all(color: Color.black)
                        ),
                        child: new Game(
                            child: new KeyboardController(
                                child: new GamePad()
                            )
                        )
                    )
                )
            );
        }
    }
}