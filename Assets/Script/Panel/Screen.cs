using System;
using System.Collections.Generic;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Transform = Unity.UIWidgets.widgets.Transform;

namespace TerisGame
{
    public class Screen : StatelessWidget
    {
        public float Width;

        public Screen(float width)
        {
            this.Width = width;
        }

        public static Screen fromHeight(float height)
        {
            return new Screen(width: ((height - 6) / 2 + 6) / 0.6f);
        }


        public override Widget build(BuildContext context)
        {
            // play panel need 60%
            var playerPanelWidth = Width * 0.6f;
            return
//                new Shake(
//                shake: GameState.Of(context).States == GameStates.drop,
//                child: 
            new SizedBox(
                    height: (playerPanelWidth - 6) * 2 + 6,
                    width: Width,
                    child: new Container(
                        color: App.SCREEN_BACKGROUND,
//                        child: 
//                        new GameMaterial(
                            child: new BrikSize(
                                size: PlayerPanel.GetBrikSizeForScreenWidth(playerPanelWidth),
                                child: new Row(
                                    children: new List<Widget>()
                                    {
                                        new PlayerPanel(width: playerPanelWidth),
                                        new SizedBox(
                                            width: Width - playerPanelWidth,
                                            child: new StatusPanel()
                                        )
                                    }
                                )
//                            )
                        )
                    )
//                )
            );
        }
    }


    public class Shake : StatefulWidget
    {
        public Widget Child;

        public bool shake;

        public Shake(Widget child, bool shake)
        {
            this.Child = child;
            this.shake = shake;
        }

        public override State createState()
        {
            return new ShakeState();
        }
    }


    // 摇晃屏幕
    public class ShakeState : TickerProviderStateMixin<Shake>
    {
        private AnimationController mController;

        public override void initState()
        {
            mController = new AnimationController(vsync: this, duration: TimeSpan.FromMilliseconds(150));
            mController.addListener(() => { setState(() => { }); });
            base.initState();
        }

        public override void didUpdateWidget(StatefulWidget oldWidget)
        {
            base.didUpdateWidget(oldWidget);
            if (widget.shake)
            {
                mController.forward(from: 0);
            }
        }

        public override void dispose()
        {
            mController.dispose();
            base.dispose();
        }

        Offset GetTranslation()
        {
            var progress = mController.value;
            var offset = Mathf.Sin(progress * Mathf.PI) * 1.5F;
            return new Offset(0, offset);
        }


        public override Widget build(BuildContext context)
        {
            return new Transform(
                transform: Matrix3.makeTrans(GetTranslation()),
                child: widget.Child
            );
        }
    }
}