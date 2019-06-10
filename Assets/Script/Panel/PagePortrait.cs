using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TerisGame
{
    public class PagePortrait : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            var size = MediaQuery.of(context).size;
            var screenW = size.width * 0.8f;

            return SizedBox.expand(
                child: new Container(
                    color: App.BACKGROUND_COLOR,
                    child: new Padding(
                        padding: MediaQuery.of(context).padding,
                        child: new Column(
                            children: new List<Widget>
                            {
                                new Row(
                                    children: new List<Widget>
                                    {
                                        new Spacer(),
                                        new FlatButton(
                                            onPressed: () =>
                                            {
//                                                DialogUtils.showDialog(
//                                                    context: context,
//                                                    builder: (buildContext => new DonationDialog())
//                                                );
                                            },
                                            child: new Text(S.of(context).Reward)
                                        )
                                    }),
//                                
                                new Spacer(),
                                new ScreenDecoration(child: new Screen(width: screenW)),
                                new Spacer(flex: 2),
                                new GameController()
                            }
                        )
                    )
                )
            );
        }
    }

    class ScreenDecoration : StatelessWidget
    {
        private Widget mChild;

        public ScreenDecoration(Widget child)
        {
            mChild = child;
        }

        public override Widget build(BuildContext context)
        {
            return new Container(
                decoration: new BoxDecoration(
                    border: new Border(
                        top: new BorderSide(color: new Color(0xFF987f0f), width: App.SCREEN_BORDER_WIDTH),
                        left: new BorderSide(color: new Color(0xFF987f0f), width: App.SCREEN_BORDER_WIDTH),
                        right: new BorderSide(color: new Color(0xFF987f0f), width: App.SCREEN_BORDER_WIDTH),
                        bottom: new BorderSide(color: new Color(0xFF987f0f), width: App.SCREEN_BORDER_WIDTH)
                    )
                ),
                child: new Container(
                    decoration: new BoxDecoration(border: Border.all(color: Colors.black54)),
                    child: new Container(
                        padding: EdgeInsets.all(3),
                        color: App.SCREEN_BACKGROUND,
                        child: mChild
                    )
                )
            );
        }
    }
}