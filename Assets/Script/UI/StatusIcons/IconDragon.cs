using System;
using Unity.UIWidgets.async;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class IconDragon : StatefulWidget
    {
        public override State createState()
        {
            return new IconDragonState();
        }
    }

    public class IconDragonState : State<IconDragon>
    {
        private Timer mTimer;

        private int mFrame;

        public override void didUpdateWidget(StatefulWidget oldWidget)
        {
            base.didUpdateWidget(oldWidget);

//            InitAnimation();
        }

        public override void initState()
        {
            base.initState();

            InitAnimation();
        }

        void InitAnimation()
        {
            mTimer = Window.instance.periodic(TimeSpan.FromMilliseconds(200), () =>
            {
                if (mFrame >= 30)
                {
                    mFrame = 0;
                }

                this.setState(() => { mFrame++; });
            });
        }

        public override void dispose()
        {
            mTimer?.cancel();
            mTimer = null;
            base.dispose();
        }

        public override Widget build(BuildContext context)
        {
            return new _Material(
                size: new Size(80, 86),
                srcSize: new Size(80, 86),
                srcOffset: GetOffset()
            );
        }

        Offset GetOffset()
        {
            int index = 0;
            if (mFrame < 10)
            {
                index = mFrame % 2 == 0 ? 0 : 1;
            }
            else
            {
                index = mFrame % 2 == 0 ? 2 : 3;
            }

            return new Offset(index * 100, 100);
        }
    }
}