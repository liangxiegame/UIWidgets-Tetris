using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TetrisApp
{
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
        private Block mCurrent = new Block();

        public void Up()
        {
            mCurrent.RowIndex--;

            if (mCurrent.RowIndex < 0)
            {
                mCurrent.RowIndex = 0;
            }

            setState(() => { });
        }

        public void Down()
        {
            var next = mCurrent.Down();

            if (next.RowIndex > 19 || mData[next.RowIndex][next.ColIndex] == 1)
            {
                // 进行积累
                MixCurrentBlockIntoData();
            }
            else
            {
                mCurrent = next;
            }

            setState(() => { });
        }

        public void Left()
        {
            mCurrent.ColIndex--;

            if (mCurrent.ColIndex < 0)
            {
                mCurrent.ColIndex = 0;
            }

            setState(() => { });
        }

        public void Right()
        {


            mCurrent.ColIndex++;

            if (mCurrent.ColIndex > 9)
            {
                mCurrent.ColIndex = 9;
            }

            setState(() => { });
        }

        void MixCurrentBlockIntoData()
        {
            mData[mCurrent.RowIndex][mCurrent.ColIndex] = 1;

            mCurrent = new Block();

            
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
                
            }
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
            var data = new List<List<int>>();

            mData.ForEach(line =>
            {
                var lineDatas = new List<int>();

                line.ForEach(brickData => { lineDatas.Add(brickData); });

                data.Add(lineDatas);
            });


            data[mCurrent.RowIndex][mCurrent.ColIndex] = 1;

            var mixed = data;

            return new GameState(data: mixed, child: widget.Child);
        }
    }

    public class GameState : InheritedWidget
    {
        public GameState(List<List<int>> data, Widget child) : base(child: child)
        {
            Data = data;
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