using System;
using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.async;
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
                        new Number(gameState.Points),
                        new SizedBox(height: 4),
                        new Text("Clears:", style: new TextStyle(
                            fontWeight: FontWeight.bold
                        )),
                        new SizedBox(height: 4),
                        new Number(gameState.ClearLines),
                        new SizedBox(height: 4),
                        new Text("Level:", style: new TextStyle(
                            fontWeight: FontWeight.bold
                        )),
                        new SizedBox(height: 4),
                        new Number(gameState.Level),
                        new SizedBox(height: 10),
                        new Text("Next:", style: new TextStyle(
                            fontWeight: FontWeight.bold
                        )),
                        new SizedBox(height: 8),
                        new NextBlock(),
                        new Spacer(),
                        new GameStatus()
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

    public class GameStatus : StatefulWidget
    {
        public override State createState()
        {
            return new GameStatusState();
        }
    }

    public class GameStatusState : State<GameStatus>
    {
        private Timer mTimer;

        private int mHours;
        private int mMinutes;
        private bool mShowColon = false;
        
        public override void initState()
        {
            base.initState();

            mTimer = Window.instance.periodic(TimeSpan.FromSeconds(1), () =>
            {
                var now = DateTime.Now;

                
                this.setState(() =>
                {
                    mHours = now.Hour;
                    mMinutes = now.Minute;

                    mShowColon = !mShowColon;
                });
            });
        }

        public override void dispose()
        {
            base.dispose();
            
            mTimer?.cancel();
            mTimer = null;
        }

        public override Widget build(BuildContext context)
        {
            return new Row(
                children: new List<Widget>()
                {
                    new IconSound(),
                    new SizedBox(width: 4),
                    new IconPause(GameState.of(context).States == GameStates.Paused),
                    new SizedBox(width: 24),
                    new Number(mHours, 2, true),
                    new IconColon(mShowColon),
                    new Number(mMinutes, 2, padWithZero: true)
                }
            );
        }
    }
}