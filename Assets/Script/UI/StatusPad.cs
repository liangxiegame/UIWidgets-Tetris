using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class StatusPad : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            var gameState = GameState.of(context);

            return new Container(
                width: 100,
                height: 200,
                color: Colors.white,
                child: new Column(
                    children: new List<Widget>()
                    {
                        new Text("Points:"),
                        new Text(gameState.Points.ToString()),
                        new Text("Clears:"),
                        new Text(gameState.ClearLines.ToString()),
                    }
                )
            );
        }
    }
}