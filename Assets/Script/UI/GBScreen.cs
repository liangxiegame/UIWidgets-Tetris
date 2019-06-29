using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    /// <summary>
    /// GameBoy
    /// </summary>
    public class GBScreen : StatelessWidget
    {
        public static Size GetBrickSizeFromBrick10x20width(float width)
        {
            return Size.square((width - AppConstants.PLAYPER_PANEL_PADDING) / AppConstants.GAME_PAD_10X20_W);
        }

        public float Width { get; }

        public GBScreen(float width)
        {
            Width = width;
        }

        public override Widget build(BuildContext context)
        {
            var brick10x20width = Width * 0.6f;

            return new SizedBox(
                width: Width,
                height: (brick10x20width - 6) * 2 + 6,
                child: new Container(
                    color: AppConstants.SCREEN_BACKGROUND_COLOR,
                    child: new GameMaterial(
                        child: new BrickSize(
                            size: GetBrickSizeFromBrick10x20width(brick10x20width),
                            child: new Row(
                                children: new List<Widget>()
                                {
                                    new Container(
                                        padding: EdgeInsets.all(2),
                                        decoration: new BoxDecoration(
                                            border: Border.all(color: Color.black)
                                        ),
                                        child: new Stack(
                                            children: new List<Widget>()
                                            {
                                                new GamePad(),
                                                new Container(
                                                    width: GetBrickSizeFromBrick10x20width(brick10x20width).width * 10 - 4,
                                                    child: new GameUninitialized()
                                                )
                                            }
                                        )
                                    ),
                                    new StatusPad()
                                }
                            )
                        )
                    )
                )
            );
        }
    }

    public class GameUninitialized : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            if (GameState.of(context).States == GameStates.None)
            {
                return new Center(
                    child: new Column(
                        mainAxisSize: MainAxisSize.min,
                        children: new List<Widget>()
                        {
                            new IconDragon(),
                            new SizedBox(height: 16),
                            new Text(L.of(context).Title,
                                style: new TextStyle(fontSize: 20))
                        }
                    )
                );
            }
            else
            {
                return new Container();
            }
        }
    }
}