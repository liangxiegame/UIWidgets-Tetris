using System;
using System.Collections.Generic;
using System.Linq;
using RSG;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TetrisApp
{
    public enum GameStates
    {
        Running,

        Drop
    }

    public class Game : StatefulWidget
    {
        public Widget Child;

        public Game(Widget child)
        {
            Child = child;
        }

        public override State createState()
        {
            return new GameController();
        }

        public static GameController of(BuildContext context)
        {
            return context.ancestorStateOfType(new TypeMatcher<GameController>()) as GameController;
        }
    }

    public class GameController : State<Game>
    {
        private Block mCurrent;

        private GameStates mStates = GameStates.Running;

        private int mLines = 0;

        private int mPoints = 0;

        public override void initState()
        {
            base.initState();
            mCurrent = Block.Next;

            Window.instance.periodic(TimeSpan.FromMilliseconds(800), () => { Down(); });
        }

        public void Drop()
        {
            if (mStates == GameStates.Running)
            {
                for (var i = 0; i < 20; i++)
                {
                    var next = mCurrent.Down(i + 1);

                    if (!next.IsValidateInData(mData))
                    {
                        mCurrent = mCurrent.Down(i);

                        this.setState(() => { });
                        mStates = GameStates.Drop;

                        Promise.Delayed(TimeSpan.FromMilliseconds(100))
                            .Then(() => { MixCurrentBlockIntoData(); });

                        break;
                    }
                }
            }
        }

        public void Rotate()
        {
            var next = mCurrent.Rotate();

            if (next.IsValidateInData(mData))
            {
                mCurrent = next;
            }

            setState(() => { });
        }

        public void Down()
        {
            if (mStates == GameStates.Running)
            {
                var next = mCurrent.Down();

                if (next.IsValidateInData(mData))
                {
                    mCurrent = next;
                }
                else
                {
                    // 进行积累
                    MixCurrentBlockIntoData();
                }

                setState(() => { });
            }
        }

        public void Left()
        {
            var next = mCurrent.Left();

            if (next.IsValidateInData(mData))
            {
                mCurrent = next;
            }

            setState(() => { });
        }

        public void Right()
        {
            var next = mCurrent.Right();

            if (next.IsValidateInData(mData))
            {
                mCurrent = next;
            }

            setState(() => { });
        }

        private List<List<int>> GetMixedData()
        {
            var mixed = new List<List<int>>();

            for (var rowIndex = 0; rowIndex < mData.Count; rowIndex++)
            {
                var line = mData[rowIndex];

                var lineDatas = new List<int>();

                for (var colIndex = 0; colIndex < line.Count; colIndex++)
                {
                    var brickData = mCurrent.Get(rowIndex, colIndex) == 1 ? 1 : line[colIndex];
                    lineDatas.Add(brickData);
                }

                mixed.Add(lineDatas);
            }

            return mixed;
        }


        void MixCurrentBlockIntoData()
        {
            mData = GetMixedData();

            mCurrent = Block.Next;

            var clearLines = new List<int>();

            for (var i = 0; i < mData.Count; i++)
            {
                if (mData[i].All(brickData => brickData == 1))
                {
                    clearLines.Add(i);
                }
            }

            if (clearLines.Count > 0)
            {
                // 进行消除操作
                Debug.Log("进行消除操作");

                clearLines.Reverse();

                clearLines.ForEach(lineIndex => mData.RemoveAt(lineIndex));

                clearLines.ForEach(__ => mData.Insert(0, Enumerable.Range(0, 10).Select(_ => 0).ToList()));

                mLines += clearLines.Count;
                mPoints += Rules.PointsForClearLines(clearLines.Count);

                Debug.Log($"Lines:{mLines} Points:{mPoints}");
            }

            mStates = GameStates.Running;

            setState(() => { });
        }

        private List<List<int>> mData = new List<List<int>>
        {
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 0
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 1
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 2
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 3
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 4
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        public override Widget build(BuildContext context)
        {
            return new GameState(
                data: GetMixedData(),
                clearLines: mLines,
                points: mPoints,
                child: widget.Child
            );
        }
    }

    public class GameState : InheritedWidget
    {
        public int ClearLines = 0;
        public int Points     = 0;

        public GameState(List<List<int>> data, int clearLines, int points, Widget child) : base(child: child)
        {
            Data = data;
            ClearLines = clearLines;
            Points = points;
        }

        public List<List<int>> Data { get; }


        public static GameState of(BuildContext context)
        {
            return context.inheritFromWidgetOfExactType(typeof(GameState)) as GameState;
        }

        public override bool updateShouldNotify(InheritedWidget oldWidget)
        {
            return true;
        }
    }
}