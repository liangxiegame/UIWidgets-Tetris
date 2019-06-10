using System.Collections.Generic;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TerisGame
{
    public class StatusPanel : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Container(
                padding: EdgeInsets.all(8),
                child: new Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: new List<Widget>()
                    {
                        new Text(S.of(context).Points,
                            style: new TextStyle(fontWeight: FontWeight.bold)),
                        new SizedBox(height: 4),
                        new Number(num: GameState.Of(context).Points),
                        new SizedBox(height: 10),
                        new Text(S.of(context).Cleans,
                            style: new TextStyle(fontWeight: FontWeight.bold)),
                        new SizedBox(height: 4),
                        new Number(num: GameState.Of(context).Cleared),
                        new SizedBox(height: 10),
                        new Text(S.of(context).Level,
                            style: new TextStyle(fontWeight: FontWeight.bold)
                        ),
                        new SizedBox(height: 4),
                        new Number(num: GameState.Of(context).Level),
                        new SizedBox(height: 10),
                        new Text(S.of(context).Next,
                            style: new TextStyle(fontWeight: FontWeight.bold)
                        ),
                        new SizedBox(height: 4),
                        // next block
                        new Spacer()
                        // game status
                    }
                )
            );
        }
    }
}