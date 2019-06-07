using System;
using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.widgets;

namespace TerisGame
{
    public class GameConstants
    {
        // the height of game pad
        public const int GAME_PAD_MATRIX_H = 20;

        // the width of game pad
        public const int GAME_PAD_MATRIX_W = 10;

        public static TimeSpan REST_LINE_DURATION = TimeSpan.FromMilliseconds(50);

        public int LEVEL_MAX = 6;

        public int LEVEL_MIN = 1;

        public static TimeSpan[] SPEED = new[]
        {
            TimeSpan.FromMilliseconds(800),
            TimeSpan.FromMilliseconds(650),
            TimeSpan.FromMilliseconds(500),
            TimeSpan.FromMilliseconds(370),
            TimeSpan.FromMilliseconds(250),
            TimeSpan.FromMilliseconds(160)
        };
    }

    // state of [GameControll]
    public enum GameStates
    {
        // 随时可以开启一把惊险而又刺激的俄罗斯方块
        none,

        // 游戏暂停中，方块的下落将会停止
        paused,

        // 游戏正在进行中,方块正在下落
        // 按键可交互
        running,

        // 游戏正在重置
        // 重置完成之后,[GameController] 状态将会迁移为 [none]
        reset,

        // 下落方块已经到达底部，此时正在将方块固定在游戏矩阵中
        // 固定完成之后，将会立即开始下一个方块的下落任务
        mixing,

        // 正在消除行
        // 消除完成之后，将会立刻开始下一个方块的下落任务
        clear,

        // 方块快速下坠到底部
        drop
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
            return new GameControl();
        }

        public static GameControl Of(BuildContext context)
        {
            var state = context.ancestorStateOfType(new TypeMatcher<GameControl>()) as GameControl;
            D.assert(state != null, () => "must wrap this context with [Game]");
            return state;
        }
    }

    public class GameControl : State<Game>, RouteAware
    {
        public GameControl()
        {
            for (var i = 0; i < GameConstants.GAME_PAD_MATRIX_H; i++)
            {
                mData.Add(Enumerable.Range(0, GameConstants.GAME_PAD_MATRIX_W).Select(index => 0).ToList());
                mMask.Add(Enumerable.Range(0, GameConstants.GAME_PAD_MATRIX_W).Select(index => 0).ToList());
            }
        }

        public override void didChangeDependencies()
        {
            base.didChangeDependencies();
            App.RouteObserver.subscribe(this, ModalRoute.of(context));
        }


        public override void dispose()
        {
            App.RouteObserver.unsubscribe(this);
            base.dispose();
        }


        // the gamer data
        List<List<int>> mData = new List<List<int>>();

        // 在 [build] 方法中于 [_data] 混合，形成一个新的矩阵
        // [mask] 矩阵的狂傲与 [_data] 一致
        // 对于任意的 mask[x,y]:
        // 如果值为 0，则对 [_data] 没有任何影响
        // 如果值为 -1，则表示 [_data] 中该行不显示
        // 如果值为  1，则表示 [_data] 中该行高亮
        private List<List<int>> mMask = new List<List<int>>();

        private int mLevel = 1;
        private int mPoints = 0;
        private int mCleared = 0;
        

        public override Widget build(BuildContext context)
        {
            List<List<int>> mixed = new List<List<int>>();

            for (var i = 0; i < GameConstants.GAME_PAD_MATRIX_H; i++)
            {
                mixed.Add(Enumerable.Range(0,GameConstants.GAME_PAD_MATRIX_W).Select(index=>0).ToList());
                for (var j = 0; j < GameConstants.GAME_PAD_MATRIX_W; j++)
                {
//                    var value =  
                    
                }
            }
            
            return new Text("hello");
        }

        public void didPopNext()
        {
        }

        public void didPush()
        {
        }

        public void didPop()
        {
        }

        public void didPushNext()
        {
        }
    }
}