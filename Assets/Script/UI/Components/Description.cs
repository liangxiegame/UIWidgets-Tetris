using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class Description : StatelessWidget
    {
        public string        Text      { get; }
        public Widget        Child     { get; }
        public AxisDirection Direction { get; }


        public Description(string text, Widget child, AxisDirection direction = AxisDirection.down)
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
                case AxisDirection.up:
                    widget = new Column(
                        mainAxisAlignment: MainAxisAlignment.end,
                        children: new List<Widget>()
                        {
                            new Text(Text),
                            new SizedBox(height: 8),
                            Child
                        });
                    break;
                case AxisDirection.down:
                    widget = new Column(
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: new List<Widget>()
                        {
                            Child,
                            new SizedBox(height: 8),
                            new Text(Text),
                        });
                    break;
                case AxisDirection.right:
                    widget = new Row(
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: new List<Widget>()
                        {
                            Child,
                            new SizedBox(height: 8),
                            new Text(Text),
                        });
                    break;

                case AxisDirection.left:
                    widget = new Row(
                        mainAxisAlignment: MainAxisAlignment.end,
                        children: new List<Widget>()
                        {
                            new Text(Text),
                            new SizedBox(height: 8),
                            Child
                        });
                    break;
            }

            return new DefaultTextStyle(
                child: widget,
                style: new TextStyle(fontSize: 12, color: Colors.black)
            );
        }
    }
}