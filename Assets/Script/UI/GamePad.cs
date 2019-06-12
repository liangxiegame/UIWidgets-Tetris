using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    /// <summary>
    /// 只是用来做显示用的
    /// </summary>
    public class GamePad : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            var bricks10x20 = GameState.of(context).Data;
            
            return new Column(
                children: bricks10x20
                    .Select(LineBrickDatas2RowWidget)
                    .ToList()
            );
        }

        Widget LineBrickDatas2RowWidget(List<int> lineBrickDatas)
        {
            return new Row(
                children: lineBrickDatas
                    .Select(BrickData2BrickWidget)
                    .ToList()
            );
        }

        Widget BrickData2BrickWidget(int brickData)
        {
            switch (brickData)
            {
                case 0:
                    return Brick.Empty();
                case 1:
                    return Brick.Normal();
                default:
                    return Brick.Highlight();
            }
        }
    }
}