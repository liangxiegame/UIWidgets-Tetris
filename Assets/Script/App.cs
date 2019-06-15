using System.Collections.Generic;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class App : UIWidgetsPanel
    {
        protected override Widget createWidget()
        {
            return new Game(
                child: new KeyboardController(
                    child: new Container(
                        color: AppConstants.SCREEN_BACKGROUND_COLOR,
                        child: SizedBox.fromSize(
                            size: new Size(100, 200),
                            child: new Row(
                                children: new List<Widget>()
                                {
                                    new Container(
                                        padding: EdgeInsets.all(2),
                                        decoration: new BoxDecoration(
                                            border: Border.all(color: Color.black)
                                        ),
                                        child: new GamePad()
                                    ),
                                    new StatusPad()
                                }
                            )
                        )
                    )
                )
            );
        }
    }
}