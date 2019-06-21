using System;
using RSG;
using Unity.UIWidgets.async;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class GBButton : StatefulWidget
    {
        public Size         Size;
        public VoidCallback OnTap;
        public Widget       Icon;
        public Color        Color;
        public bool         EnableLongPress;

        public GBButton(
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
            return new GBButtonState();
        }
    }

    class GBButtonState : State<GBButton>
    {
        private Color mColor;

        private Timer mTimer;

        private bool mTapEnd = false;


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
                        
                        mTapEnd = false;

                        widget.OnTap();

                        if (!widget.EnableLongPress)
                        {
                            return;
                        }

                        Promise.Delayed(TimeSpan.FromMilliseconds(300))
                            .Then(() =>
                            {
                                if (!mTapEnd)
                                {
                                    mTimer = Window.instance.periodic(TimeSpan.FromMilliseconds(60),
                                        () =>
                                        {
                                            if (mTapEnd)
                                            {
                                                mTimer?.cancel();
                                                mTimer = null;
                                            }
                                            else
                                            {
                                                widget.OnTap();
                                            }
                                        });
                                }
                            });

      
                    },
                    onTapCancel: () =>
                    {
                        mTimer?.cancel();
                        mTimer = null;
                        mTapEnd = true;

                        this.setState(() => { mColor = widget.Color; });
                    },
                    onTapUp: details =>
                    {
                        mTimer?.cancel();
                        mTimer = null;
                        mTapEnd = true;
                        
                        this.setState(() => { mColor = widget.Color; });
                    },
                    child: SizedBox.fromSize(
                        size: widget.Size
                    )
                )
            );
        }
    }
}