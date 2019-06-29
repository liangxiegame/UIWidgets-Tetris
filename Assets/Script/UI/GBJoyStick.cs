using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Transform = Unity.UIWidgets.widgets.Transform;

namespace TetrisApp
{
    public class GBJoyStick : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new SizedBox(
                height: 200,
                child: new Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: new List<Widget>()
                    {
                        new Expanded(child: new LeftButtons()),
                        new Expanded(child: new DirectionButtons())
                    }
                )
            );
        }
    }

    public class DirectionButtons : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return SizedBox.fromSize(size: AppConstants.DIRECTION_BUTTON_SIZE * 2.8f,
                child: Transform.rotate(
                    origin: new Offset(90, 90),
                    degree: Mathf.PI / 4,
                    child: new Column(
                        mainAxisSize: MainAxisSize.min,
                        children: new List<Widget>()
                        {
                            new SizedBox(height: AppConstants.DIRECTION_BUTTON_SPACE),
                            new Row(
                                children: new List<Widget>()
                                {
                                    new SizedBox(width: AppConstants.DIRECTION_BUTTON_SPACE),
                                    new GBButton(
                                        size: AppConstants.DIRECTION_BUTTON_SIZE,
                                        () => { Game.of(context).Drop(); }
                                    ),
                                    new SizedBox(width: AppConstants.DIRECTION_BUTTON_SPACE),
                                    new GBButton(
                                        size: AppConstants.DIRECTION_BUTTON_SIZE,
                                        () => { Game.of(context).Right(); }
                                    )
                                }
                            ),
                            new SizedBox(height: AppConstants.DIRECTION_BUTTON_SPACE),
                            new Row(
                                children: new List<Widget>()
                                {
                                    new SizedBox(width: AppConstants.DIRECTION_BUTTON_SPACE),
                                    new GBButton(
                                        size: AppConstants.DIRECTION_BUTTON_SIZE,
                                        () => { Game.of(context).Left(); }
                                    ),
                                    new SizedBox(width: AppConstants.DIRECTION_BUTTON_SPACE),
                                    new GBButton(
                                        size: AppConstants.DIRECTION_BUTTON_SIZE,
                                        () => { Game.of(context).Down(); }
                                    )
                                }
                            ),
                            new SizedBox(height: AppConstants.DIRECTION_BUTTON_SPACE)
                        }
                    )
                )
            );
        }
    }


    public class LeftButtons : StatelessWidget
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
                            child: new RotateButton()
                        )
                    )
                }
            );
        }
    }

    public class SystemButtonGroup : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: new List<Widget>()
                {
                    new Description(
                        text: L.of(context).Sounds,
                        child: new GBButton(
                            size: new Size(28, 28),
                            () => { Game.of(context).SoundSwitch(); },
                            enableLongPress: false
                        )
                    ),
                    new Description(
                        text: L.of(context).PauseOrResume,
                        child: new GBButton(
                            size: new Size(28, 28),
                            () => { Game.of(context).PauseOrResume(); },
                            enableLongPress: false
                        )
                    ),
                    new Description(
                        text: L.of(context).Reset,
                        child: new GBButton(
                            size: new Size(28, 28),
                            () => { Game.of(context).Reset(); },
                            enableLongPress: false,
                            color: Colors.red
                        )
                    )
                }
            );
        }
    }

    public class RotateButton : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new GBButton(
                enableLongPress: false,
                size: new Size(90, 90),
                onTap: () => { Game.of(context).Rotate(); }
            );
        }
    }
}