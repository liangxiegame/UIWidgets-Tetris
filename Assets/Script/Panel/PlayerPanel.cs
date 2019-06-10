using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TerisGame
{
    public class PlayerPanel : StatelessWidget
    {
        public const int PLAYER_PANEL_PADDING = 6;

        public static Size GetBrikSizeForScreenWidth(float width)
        {
            return Size.square((width - PLAYER_PANEL_PADDING) / GameConstants.GAME_PAD_MATRIX_W);
        }

        public Size Size;

        public PlayerPanel(float width)
        {
            Size = new Size(width, width * 2);
        }

        public override Widget build(BuildContext context)
        {
            return SizedBox.fromSize(
                size: Size,
                child: new Container(
                    padding: EdgeInsets.all(2),
                    decoration: new BoxDecoration(
                        border: Border.all(color: Colors.black)
                    ),
                    child: new Stack(
                        children: new List<Widget>()
                        {
                            new PlayerPad(),
                            new GameUninitialized()
                        }
                    )
                )
            );
        }
    }

    public class PlayerPad : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Column(
                children: GameState.Of(context).Data.Select(list => new Row(
                    children: list.Select(b => b == 1 ? Brik.Normal() as Widget
                        : b == 2 ? Brik.Highlight() : Brik.Empty()).ToList()
                ) as Widget).ToList()
            );
        }
    }

    public class GameUninitialized : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            if (GameState.Of(context).States == GameStates.none)
            {
                return new Center(
                    child: new Column(
                        mainAxisSize: MainAxisSize.min,
                        children: new List<Widget>()
                        {
//                            new IconDragon(animate:true)
                            new SizedBox(height: 16),
                            new Text("tetrix", style: new TextStyle(fontSize: 20))
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