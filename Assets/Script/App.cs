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
                    child: new GBScreen()
                )
            );
        }
    }
}