using System.Collections.Generic;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    /// <summary>
    /// GameBoy
    /// </summary>
    public class GBScreen : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Container(
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
            );
        }
    }
}