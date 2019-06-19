using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.widgets;

namespace TerisGame
{
    public class PageLand : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            var height = MediaQuery.of(context).size.height;
            height -= MediaQuery.of(context).viewInsets.vertical;

            return SizedBox.expand(
                child: new Container(
                    color: App.BACKGROUND_COLOR,
                    child: new Padding(
                        padding: MediaQuery.of(context).padding,
                        child: new Row(
                            mainAxisSize: MainAxisSize.min,
                            children: new List<Widget>()
                            {
                                new Expanded(
                                    child: new Column(
                                        crossAxisAlignment: CrossAxisAlignment.start,
                                        children: new List<Widget>()
                                        {
                                            new Spacer(),
//                                            new SystemButtonGroup(),
                                            new Spacer(),
                                            new Padding(
//                                                padding: EdgeInsets.only(left: 40, bottom: 40),
//                                                child:new DropButton()
                                            )
                                        }
                                    )),
//                                    _ScreenDecoration(child: Screen.fromHeight(height * 0.8)),
                                new Expanded(
                                    child: new Column(
                                        children: new List<Widget>()
                                        {
                                            new Row(
                                                children: new List<Widget>()
                                                {
                                                    new Spacer(),
                                                    new FlatButton(
                                                        onPressed: () =>
                                                        {
//                                                                DialogUtils.showDialog(
//                                                                    context: context,
//                                                                    builder: (buildContext => new DonationDialog())
//                                                                );
                                                        },
                                                        child: new Text(S.of(context).Reward)
                                                    )
                                                }),
                                            new Spacer(),
//                                                new DirectionCOn
                                            new SizedBox(height: 30)
                                        })
                                )
                            }
                        )
                    )
                )
            );
        }
    }
}