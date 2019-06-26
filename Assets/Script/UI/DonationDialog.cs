using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;
using Image = Unity.UIWidgets.widgets.Image;

namespace TetrisApp
{
    public class DonationDialog : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new SimpleDialog(
                contentPadding: EdgeInsets.only(left: 8, top: 8, right: 8, bottom: 4),
                children:
                new List<Widget>()
                {
                    new SizedBox(width: MediaQuery.of(context).size.width),
                    new Container(padding: EdgeInsets.all(8), child: new Text("开发不易，捐助一下给开发者。")),
                    new ActionTile("微信捐赠", () =>
                    {
                        DialogUtils.showDialog(context: context,
                                builder: ctx => ReceiptDialog.Wechat())
                            .Then(_ => { Navigator.pop(context); });
                    }),
                    new ActionTile("支付宝捐赠", () =>
                    {
                        DialogUtils.showDialog(context: context,
                                builder: ctx => ReceiptDialog.Alipay())
                            .Then(_ => { Navigator.pop(context); });
                    })
                }
            );
        }
    }

    public class ActionTile : StatelessWidget
    {
        private string       mText;
        private VoidCallback mOnTap;

        public ActionTile(string text, VoidCallback onTap)
        {
            mText = text;
            mOnTap = onTap;
        }

        public override Widget build(BuildContext context)
        {
            return new InkWell(
                onTap: () => { mOnTap?.Invoke(); },
                child: new Container(
                    height: 40,
                    child: new Row(
                        children: new List<Widget>()
                        {
                            new SizedBox(width: 16),
                            new Text(mText, style: new TextStyle(fontWeight: FontWeight.bold))
                        }
                    )
                )
            );
        }
    }

    public class ReceiptDialog : StatelessWidget
    {
        private string mImage;

        public ReceiptDialog(string image)
        {
            mImage = image;
        }

        public static ReceiptDialog Wechat()
        {
            return new ReceiptDialog("wechat");
        }

        public static ReceiptDialog Alipay()
        {
            return new ReceiptDialog("alipay");
        }


        public override Widget build(BuildContext context)
        {
            return new Dialog(
                shape: new RoundedRectangleBorder(borderRadius: BorderRadius.circular(5)),
                child: new ClipRRect(borderRadius: BorderRadius.circular(5), child: Image.asset(mImage))
            );
        }
    }
}