using System.Collections.Generic;
using System.Linq;
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
                        new SizedBox(height: 10),
                        new Text("Next:", style: new TextStyle(
                            fontWeight: FontWeight.bold
                        )),
                        new SizedBox(height: 8),
                        new NextBlock()
                    }
                )
            );
        }
    }

    public class NextBlock : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            var data = Utils.Create2DList(2, 4, (_, __) => 0);
            var next = Block.BLOCK_SHAPES[GameState.of(context).Next.Type];

            for (var i = 0; i < next.Count; i++)
            {
                for (var j = 0; j < next[i].Count; j++)
                {
                    data[i][j] = next[i][j];
                }
            }

            return new Column(
                children: data.Select(line => new Row(
                    children: line.Select(b => b == 1 ? Brick.Normal() as Widget : Brick.Empty()).ToList()
                ) as Widget).ToList()
            );
        }
    }
}