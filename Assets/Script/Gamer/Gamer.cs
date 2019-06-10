using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.async;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TerisGame
{
    public class GameConstants
    {
        // the height of game pad
        public const int GAME_PAD_MATRIX_H = 20;

        // the width of game pad
        public const int GAME_PAD_MATRIX_W = 10;

        public static TimeSpan REST_LINE_DURATION = TimeSpan.FromMilliseconds(50);

        public const int LEVEL_MAX = 6;

        public const int LEVEL_MIN = 1;

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


        // the gamer data
        List<List<int>> mData = new List<List<int>>();

        // 在 [build] 方法中于 [_data] 混合，形成一个新的矩阵
        // [mask] 矩阵的狂傲与 [_data] 一致
        // 对于任意的 mask[x,y]:
        // 如果值为 0，则对 [_data] 没有任何影响
        // 如果值为 -1，则表示 [_data] 中该行不显示
        // 如果值为  1，则表示 [_data] 中该行高亮
        private List<List<int>> mMask = new List<List<int>>();

        private int mLevel   = 1;
        private int mPoints  = 0;
        private int mCleared = 0;

        private Block mCurrent;

        private Block mNext = Block.GetRandom();

        private GameStates mStates = GameStates.none;

        Block GetNext()
        {
            var next = mNext;
            mNext = Block.GetRandom();
            return next;
        }


//        private SoundState mSound => Sound.of(context);


        public void Rotate()
        {
            if (mStates == GameStates.running && mCurrent != null)
            {
                var next = mCurrent.Rotate();
                
                if (next.IsValidInMatrix(mData))
                {
                    mCurrent = next;
//                    _sound.rotate();
                }
            }

            setState(() => { });
        }

        public void Right()
        {
            if (mStates == GameStates.none && mLevel < GameConstants.LEVEL_MAX)
            {
                mLevel++;
            }
            else if (mStates == GameStates.running && mCurrent != null)
            {
                var next = mCurrent.Right();
                if (next.IsValidInMatrix(mData))
                {
                    mCurrent = next;
//                    _sound.move();
                }
            }

            setState(() => { });
        }

        public void Left()
        {
            if (mStates == GameStates.none && mLevel > GameConstants.LEVEL_MIN)
            {
                mLevel--;
            }
            else if (mStates == GameStates.running && mCurrent != null)
            {
                var next = mCurrent.Left();
                if (next.IsValidInMatrix(mData))
                {
                    mCurrent = next;
                    //                    _sound.move();
                }
            }

            setState(() => { });
        }

        
        public void Drop()
        {
            if (mStates == GameStates.running && mCurrent != null)
            {
                for (int i = 0; i < GameConstants.GAME_PAD_MATRIX_H; i++)
                {
                    var fall = mCurrent.Fall(i + 1);
                    if (!fall.IsValidInMatrix(mData))
                    {
                        mCurrent = mCurrent.Fall(step: i);
                        mStates = GameStates.drop;
                        setState(() => { });
//                        await Future.delayed(const Duration(milliseconds: 100));
                        MixCurrentIntoData();
                        break;
                    }
                }
            }
            else if (mStates == GameStates.paused || mStates == GameStates.none)
            {
                StartGame();
            }
        }

        public void Down(bool enableSounds = true)
        {
            if (mStates == GameStates.running && mCurrent != null)
            {
                var next = mCurrent.Fall();

                if (next.IsValidInMatrix(mData))
                {
                    mCurrent = next;
                    if (enableSounds)
                    {
//                        mSound.Move();
                    }
                }
                else
                {
                    MixCurrentIntoData();
                }

                setState(() => { });
            }
        }

        private Timer mAutoFallTimer = null;

        void MixCurrentIntoData()
        {
            if (mCurrent == null)
            {
                return;
            }

            AutoFall(false);

            ForTable((i, j) =>
            {
                mData[i][j] = mCurrent.Get(j, i) ?? mData[i][j];
                return false;
            });

            //消除行
            var clearLines = new List<int>();

            for (int i = 0; i < GameConstants.GAME_PAD_MATRIX_H; i++)
            {
                if (mData[i].All(d => d == 1))
                {
                    clearLines.Add(i);
                }
            }


            if (clearLines.isNotEmpty())
            {
                setState(() => mStates = GameStates.clear);

//                _sound.clear();

                ///消除效果动画
                for (int count = 0; count < 5; count++)
                {
                    clearLines.ForEach(line =>
                    {
                        mMask[line] = Enumerable
                            .Range(0, GameConstants.GAME_PAD_MATRIX_W)
                            .Select(index => count % 2 == 0 ? -1 : 1)
                            .ToList();
                    });

                    setState(() => { });
//                    await Future.delayed(Duration(milliseconds: 100));
                }

                clearLines.ForEach(line =>
                    mMask[line] = Enumerable.Range(0, GameConstants.GAME_PAD_MATRIX_W).Select(_ => 0).ToList());

                //移除所有被消除的行
                clearLines.ForEach(line=> {
                    
                    mData.Insert(0, Enumerable.Range(0, GameConstants.GAME_PAD_MATRIX_W).Select(_ => 0).ToList());
                });
//                debugPrint("clear lines : $clearLines");
//
                mCleared += clearLines.Count;
                mPoints += clearLines.Count * mLevel * 5;
//
//                //up level possible when cleared
                int level = (mCleared / 50) + GameConstants.LEVEL_MIN;
                mLevel = level <= GameConstants.LEVEL_MAX && level > mLevel ? level : mLevel;
            }
            else
            {
                mStates = GameStates.mixing;
//                if (mixSound != null) mixSound();
                ForTable((i, j) =>
                {
                    mMask[i][j] = mCurrent.Get(j, i) ?? mMask[i][j];

                    return false;
                });

                setState(() => { });
//                await Future.delayed(const Duration(milliseconds: 200));
                ForTable((i, j) =>
                {
                    mMask[i][j] = 0;
                    return false;
                });

                setState(() => { });
            }

            //_current已经融入_data了，所以不再需要
            mCurrent = null;

            //检查游戏是否结束,即检查第一行是否有元素为1
            if (mData[0].Contains(1))
            {
//                reset();
                return;
            }
            else
            {
                //游戏尚未结束，开启下一轮方块下落
                StartGame();
            }
        }

        static void ForTable(Func<int, int, bool> func)
        {
            for (int i = 0; i < GameConstants.GAME_PAD_MATRIX_H; i++)
            {
                for (int j = 0; j < GameConstants.GAME_PAD_MATRIX_W; j++)
                {
                    var b = func(i, j);

                    if (b) break;
                }
            }
        }

        void AutoFall(bool enable)
        {
            if (!enable && mAutoFallTimer != null)
            {
                mAutoFallTimer.cancel();
                mAutoFallTimer = null;
            }
            else if (enable)
            {
                mAutoFallTimer?.cancel();
                mCurrent = mCurrent ?? GetNext();
                mAutoFallTimer = Window.instance.periodic(GameConstants.SPEED[mLevel - 1],
                    () => { Down(enableSounds: false); });
            }
        }

        public void StartGame()
        {
            if (mStates == GameStates.running && mAutoFallTimer == null)
            {
                return;
            }

            mStates = GameStates.running;
            AutoFall(true);
            setState(() => { });
        }

        public override Widget build(BuildContext context)
        {
            var mixed = new List<List<int>>();

            for (var i = 0; i < GameConstants.GAME_PAD_MATRIX_H; i++)
            {
                mixed.Add(Enumerable.Range(0, GameConstants.GAME_PAD_MATRIX_W).Select(index => 0).ToList());
                for (var j = 0; j < GameConstants.GAME_PAD_MATRIX_W; j++)
                {
                    var value = mCurrent?.Get(j, i) ?? mData[i][j];

                    if (mMask[i][j] == -1)
                    {
                        value = 0;
                    }
                    else if (mMask[i][j] == 1)
                    {
                        value = 2;
                    }

                    mixed[i][j] = value;
                }
            }

            return new GameState(
                mixed,
                states: mStates,
                level: mLevel,
                false,
                mPoints,
                mCleared,
                mNext,
                child: widget.Child
            );
        }
    }

    public class GameState : InheritedWidget
    {
        public GameState(List<List<int>> data, GameStates states, int level, bool muted, int points, int cleared,
            Block next, Widget child) : base(child: child)
        {
            Data = data;
            States = states;
            Level = level;
            Muted = muted;
            Points = points;
            Cleared = cleared;
            Next = next;
            Child = child;
        }


        public Widget Child;

        /// <summary>
        /// 屏幕展示数据
        /// 0：空砖块
        /// 1：普通砖块
        /// 2：高亮砖块
        /// </summary>
        public List<List<int>> Data;

        public GameStates States;

        public int Level;

        public bool Muted;

        public int Points;

        public int Cleared;

        public Block Next;

        public static GameState Of(BuildContext context)
        {
            return context.inheritFromWidgetOfExactType(typeof(GameState)) as GameState;
        }

        public override bool updateShouldNotify(InheritedWidget oldWidget)
        {
            return true;
        }
    }
}