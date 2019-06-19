using System;
using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.async;
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
            var data = Utils.Create2x2List(2, 4, (_, __) => 0);
            var next = Block.BLOCK_SHAPES[GameState.Of(context).Next.Type];

            for (var i = 0; i < next.Count; i++)
            {
                for (var j = 0; j < next[i].Count; j++)
                {
                    data[i][j] = next[i][j];
                }
            }

            return new Column(
                children: data
                    .Select(list => new Row(
                        children: list
                            .Select(b => b == 1 ? Brik.Normal() as Widget : Brik.Empty())
                            .ToList()
                    ) as Widget)
                    .ToList());
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

        private bool mColonEnable = true;

        private int mMinute;

        private int mHour;


        public override void initState()
        {
            base.initState();

            mTimer = Window.instance.periodic(TimeSpan.FromSeconds(1), () =>
            {
                var now = DateTime.Now;

                setState(() =>
                {
                    mColonEnable = !mColonEnable;
                    mMinute = now.Minute;
                    mHour = now.Hour;
                });
            });
        }

        public override void dispose()
        {
            mTimer.cancel();
            base.dispose();
        }

        public override Widget build(BuildContext context)
        {
            return new Row(
                children: new List<Widget>()
                {
                    new IconSound(enable: GameState.Of(context).Muted),
                    new SizedBox(width: 4),
                    new IconPause(enable: GameState.Of(context).States == GameStates.paused),
                    new Spacer(),
                    new Number(num: mHour, length: 2, padWithZero: true),
                    new IconColon(enable: mColonEnable),
                    new Number(num: mMinute, length: 2, padWithZero: true)
                }
            );
        }
    }
}