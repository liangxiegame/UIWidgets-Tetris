using System;
using System.Collections.Generic;
using RSG;
using Unity.UIWidgets.async;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;
using Icons = Unity.UIWidgets.material.Icons;
using Material = Unity.UIWidgets.material.Material;
using Transform = Unity.UIWidgets.widgets.Transform;

namespace TerisGame
{
    public class GameController : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new SizedBox(
                height: 200,
                child: new Row(
                    children: new List<Widget>()
                    {
                        new Expanded(child: new LeftController()),
                        new Expanded(child: new DirectionController())
                    }
                )
            );
        }

        public static Size  DIRECTION_BUTTON_SIZE = new Size(48, 48);
        public static Size  SYSTEM_BUTTON_SIZE    = new Size(28, 28);
        public const  float DIRECTION_SPACE       = 16;
        public const  float IconSize              = 16;
    }

    public class DirectionController : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Stack(
                alignment: Alignment.center,
                children: new List<Widget>()
                {
                    SizedBox.fromSize(size: GameController.DIRECTION_BUTTON_SIZE * 2.8f),
                    Transform.rotate(
                        degree: Mathf.PI / 4,
                        child: new Column(
                            mainAxisSize: MainAxisSize.min,
                            children: new List<Widget>()
                            {
                                new Row(
                                    mainAxisSize: MainAxisSize.min,
                                    children: new List<Widget>()
                                    {
                                        Transform.scale(
                                            scale: 1.5f,
                                            child: Transform.rotate(
                                                degree: -Mathf.PI / 4,
                                                child: new Icon(
                                                    Icons.arrow_drop_up,
                                                    size: GameController.IconSize
                                                )
                                            )
                                        ),
                                        Transform.scale(
                                            scale: 1.5f,
                                            child: Transform.rotate(
                                                degree: -Mathf.PI / 4,
                                                child: new Icon(
                                                    Icons.arrow_right,
                                                    size: GameController.IconSize
                                                )
                                            )
                                        )
                                    }
                                ),
                                new Row(
                                    mainAxisSize: MainAxisSize.min,
                                    children: new List<Widget>
                                    {
                                        Transform.scale(
                                            scale: 1.5f,
                                            child: Transform.rotate(
                                                degree: -Mathf.PI / 4,
                                                child: new Icon(
                                                    Icons.arrow_left,
                                                    size: GameController.IconSize
                                                )
                                            )
                                        ),
                                        Transform.scale(
                                            scale: 1.5f,
                                            child: Transform.rotate(
                                                degree: -Mathf.PI / 4,
                                                child: new Icon(
                                                    Icons.arrow_drop_down,
                                                    size: GameController.IconSize
                                                )
                                            )
                                        )
                                    }
                                )
                            }
                        )
                    ),
                    Transform.rotate(
                        degree: Mathf.PI / 4,
                        child: new Column(
                            mainAxisSize: MainAxisSize.min,
                            children: new List<Widget>
                            {
                                new SizedBox(height: GameController.DIRECTION_SPACE),
                                new Row(
                                    mainAxisSize: MainAxisSize.min,
                                    children: new List<Widget>
                                    {
                                        new Button(
                                            enableLongPress: false,
                                            size: GameController.DIRECTION_BUTTON_SIZE,
                                            onTap: () =>
                                            {
                                                Game.Of(context).Rotate();
                                            }),
                                        new SizedBox(width: GameController.DIRECTION_SPACE),
                                        new Button(
                                            size: GameController.DIRECTION_BUTTON_SIZE,
                                            onTap: () =>
                                            {
                                                Game.Of(context).Right();                                                
                                            })
                                    }
                                ),
                                new SizedBox(height: GameController.DIRECTION_SPACE),
                                new Row(
                                    mainAxisSize: MainAxisSize.min,
                                    children: new List<Widget>()
                                    {
                                        new Button(
                                            size: GameController.DIRECTION_BUTTON_SIZE,
                                            onTap: () =>
                                            {
                                                Game.Of(context).Left();
                                            }
                                        ),
                                        new SizedBox(width: GameController.DIRECTION_SPACE),
                                        new Button(
                                            size: GameController.DIRECTION_BUTTON_SIZE,
                                            onTap: () =>
                                            {
                                                Game.Of(context).Down();
                                            }
                                        )
                                    }
                                ),
                                new SizedBox(height: GameController.DIRECTION_SPACE),
                            }
                        )
                    )
                });
        }
    }

    public class SystemButtonGroup : StatelessWidget
    {
        static Color mSystemButtonColor = new Color(0xFF2dc421);

        public override Widget build(BuildContext context)
        {
            return new Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: new List<Widget>()
                {
                    new Description(
                        text: S.of(context).Sounds,
                        child: new Button(
                            size: GameController.SYSTEM_BUTTON_SIZE,
                            color: mSystemButtonColor,
                            enableLongPress: false,
                            onTap: () =>
                            {
//                                Game.of(context).soundSwitch();
                            }
                        )
                    ),
                    new Description(
                        text: S.of(context).PauseResume,
                        child: new Button(
                            size: GameController.SYSTEM_BUTTON_SIZE,
                            color: mSystemButtonColor,
                            enableLongPress: false,
                            onTap: () =>
                            {
//                                Game.of(context).pauseOrResume();
                            }
                        )
                    ),
                    new Description(
                        text: S.of(context).Reset,
                        child: new Button(
                            size: GameController.SYSTEM_BUTTON_SIZE,
                            color: Colors.red, 
                            enableLongPress: false,
                            onTap: () =>
                            {
//                                Game.of(context).reset();
                            }
                        )
                    )
                }
            );
        }
    }

    public class DropButton : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Description(
                text: "Drop",
                child: new Button(
                    enableLongPress: false,
                    size: new Size(90, 90),
                    onTap: () =>
                    {
                        Game.Of(context).Drop();
                    }
                )
            );
        }
    }

    public class LeftController : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Column(
                mainAxisSize: MainAxisSize.min,
                children: new List<Widget>()
                {
                    new SystemButtonGroup(),
                    new Expanded(
                        child: new Center(
                            child: new DropButton()
                        )
                    )
                }
            );
        }
    }

    public class Button : StatefulWidget
    {
        public Size   Size;
        public Widget Icon;

        public VoidCallback OnTap;

        public Color Color;

        public bool EnableLongPress;

        public Button(
            Size size,
            VoidCallback onTap,
            Widget icon = null,
            Color color = null,
            bool enableLongPress = true)
        {
            Size = size;
            OnTap = onTap;
            Icon = icon;
            Color = color ?? Colors.blue;
            EnableLongPress = enableLongPress;
        }

        public override State createState()
        {
            return new ButtonState();
        }
    }

    public class Description : StatelessWidget
    {
        public string        Text;
        public Widget        Child;
        public AxisDirection Direction;

        public Description(
            string text,
            Widget child,
            AxisDirection direction = AxisDirection.down
        )
        {
            Text = text;
            Child = child;
            Direction = direction;
        }

        public override Widget build(BuildContext context)
        {
            Widget widget = null;
            switch (Direction)
            {
                case AxisDirection.right:
                    widget = new Row(
                        mainAxisSize: MainAxisSize.min,
                        children: new List<Widget>()
                        {
                            Child,
                            new SizedBox(width: 8),
                            new Text(Text)
                        }
                    );
                    break;
                case AxisDirection.left:
                    widget = new Row(
                        children: new List<Widget>()
                        {
                            new Text(Text),
                            new SizedBox(width: 8),
                            Child
                        }
                    );
                    break;
                case AxisDirection.up:
                    widget = new Column(
                        children: new List<Widget>()
                        {
                            new Text(Text),
                            new SizedBox(height: 8),
                            Child
                        }
                    );
                    break;
                case AxisDirection.down:
                    widget = new Column(
                        children: new List<Widget>()
                        {
                            Child,
                            new SizedBox(height: 8),
                            new Text(Text)
                        },
                        mainAxisSize: MainAxisSize.min
                    );
                    break;
            }

            return new DefaultTextStyle(
                child: widget,
                style: new TextStyle(fontSize: 12, color: Colors.black)
            );
        }
    }

    public class ButtonState : State<Button>
    {
        private Timer mTimer;

        private bool mTapEnded = false;

        private Color mColor;


        public override void didUpdateWidget(StatefulWidget oldWidget)
        {
            base.didUpdateWidget(oldWidget);
            mColor = widget.Color;
        }

        public override void initState()
        {
            base.initState();
            mColor = widget.Color;
        }

        public override Widget build(BuildContext context)
        {
            return new Material(
                color: mColor,
                elevation: 2,
                shape: new CircleBorder(),
                child: new GestureDetector(
                    behavior: HitTestBehavior.opaque,
                    onTapDown: details =>
                    {
                        this.setState(() => { mColor = widget.Color.withOpacity(0.5f); });
                        if (mTimer != null)
                        {
                            return;
                        }

                        mTapEnded = false;

                        widget.OnTap();

                        if (!widget.EnableLongPress)
                        {
                            return;
                        }

                        Promise.Delayed(TimeSpan.FromMilliseconds(300))
                            .Then(() =>
                            {
                                if (mTapEnded)
                                {
                                    return;
                                }

                                mTimer = Window.instance.periodic(TimeSpan.FromMilliseconds(60), () =>
                                {
                                    if (!mTapEnded)
                                    {
                                        widget.OnTap();
                                    }
                                    else
                                    {
                                        mTimer?.cancel();
                                        mTimer = null;
                                    }
                                });
                            });
                    },
                    onTapCancel: () =>
                    {
                        mTapEnded = true;
                        mTimer?.cancel();
                        mTimer = null;

                        setState(() => { mColor = widget.Color; });
                    },
                    onTapUp: details =>
                    {
                        mTapEnded = true;
                        mTimer?.cancel();
                        mTimer = null;

                        setState(() => { mColor = widget.Color; });
                    },
                    child: SizedBox.fromSize(
                        size: widget.Size
                    )
                )
            );
        }
    }
}