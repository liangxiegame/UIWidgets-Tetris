using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class StatusPad : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            var gameState = GameState.of(context);

            return new Container(
                padding: EdgeInsets.all(8),
                child: new Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: new List<Widget>()
                    {
                        new Text("Points:", style: new TextStyle(
                            fontWeight: FontWeight.bold
                        )),
                        new SizedBox(height: 4),
                        new Text(gameState.Points.ToString()),
                        new SizedBox(height: 4),
                        new Text("Clears:", style: new TextStyle(
                            fontWeight: FontWeight.bold
                        )),
                        new SizedBox(height: 4),
                        new Text(gameState.ClearLines.ToString()),
                    }
                )
            );
        }
    }
}