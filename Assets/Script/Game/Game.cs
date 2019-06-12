using System.Collections.Generic;
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
            Debug.Log("上");
            
            mCurrent.RowIndex--;

            if (mCurrent.RowIndex < 0)
            {
                mCurrent.RowIndex = 0;
            }
            
            setState(() => { });

        }

        public void Down()
        {
            Debug.Log("下");

            mCurrent.RowIndex++;

            if (mCurrent.RowIndex > 19)
            {
                mCurrent.RowIndex = 19;
            }
            
            setState(() => { });
        }

        public void Left()
        {
            Debug.Log("左");

            mCurrent.ColIndex--;

            if (mCurrent.ColIndex < 0)
            {
                mCurrent.ColIndex = 0;
            }
            
            setState(() => { });

        }

        public void Right()
        {
            Debug.Log("右");


            mCurrent.ColIndex++;

            if (mCurrent.ColIndex > 9)
            {
                mCurrent.ColIndex = 9;
            }
            
            setState(() => { });

        }

        public override Widget build(BuildContext context)
        {
            var data = new List<List<int>>
            {
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
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            };

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