using System.Collections.Generic;
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
                    children: new List<Widget>()
                    {
                        new Expanded(child:new LeftButtons()),
                        new Expanded(child:new DirectionButtons())
                    }
                )
            );
        }
    }

    public class DirectionButtons : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return Transform.rotate(
                degree: Mathf.PI / 4,
                child: new Column(
                    children: new List<Widget>()
                    {
                        new Row(
                            children: new List<Widget>()
                            {
                                // 
                                new GBButton(
                                    size: AppConstants.DIRECTION_BUTTON_SIZE,
                                    () =>
                                    {
                                        Game.of(context).Drop();
                                    }
                                ),
                                new GBButton(
                                    size: AppConstants.DIRECTION_BUTTON_SIZE,
                                    () =>
                                    {
                                        Game.of(context).Right();

                                    }
                                )
                            }
                        ),
                        new Row(
                            children: new List<Widget>()
                            {
                                new GBButton(
                                    size: AppConstants.DIRECTION_BUTTON_SIZE,
                                    () =>
                                    {
                                        Game.of(context).Left();
                                    }
                                ),
                                new GBButton(
                                    size: AppConstants.DIRECTION_BUTTON_SIZE,
                                    () =>
                                    {
                                        Game.of(context).Down();
                                    }
                                )
                            }
                        )
                    }
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
                    new Expanded(
                        child: new Center(
                            child: new RotateButton()
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