using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.service;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;
using Image = Unity.UIWidgets.widgets.Image;

namespace TerisGame
{
    public class DonationDialog : StatelessWidget
    {
        const string HONG_BAO = "打开支付宝首页搜“621412820”领红包，领到大红包的小伙伴赶紧使用哦！";


        public override Widget build(BuildContext context)
        {
            return new SimpleDialog(
                contentPadding: EdgeInsets.only(top: 8, left: 8, right: 8, bottom: 4),
                children: new List<Widget>()
                {
                    new SizedBox(width: MediaQuery.of(context).size.width),
                    new Container(padding: EdgeInsets.all(16), child: new Text("开发不易,赞助一下开发者。")),
                    new ActionTile(
                        text: "微信捐赠",
                        onTap: () =>
                        {
                            DialogUtils.showDialog(
                                context: context,
                                builder: (buildContext => ReceiptDialog.Wechat())
                            ).Then(_ => { Navigator.pop(context); });
                        }
                    ),
                    new ActionTile(
                        text: "支付宝捐赠",
                        onTap: () =>
                        {
                            DialogUtils.showDialog(
                                context: context,
                                builder: (buildContext => ReceiptDialog.Alipay())
                            ).Then(_ => { Navigator.pop(context); });
                        }
                    ),
                    new ActionTile(
                        text: "支付宝红包码",
                        onTap: () =>
                        {
                            Clipboard.setData(new ClipboardData(text: HONG_BAO)).Then(() =>
                                {
                                    Clipboard.getData(Clipboard.kTextPlain).Then(data =>
                                        {
                                            if (data.text == HONG_BAO)
                                            {
//                                showSimpleNotification(context, Text("已复制到粘贴板 （≧ｙ≦＊）"));
                                            }
                                            else
                                            {
                                                DialogUtils.showDialog(
                                                        context: context,
                                                        builder: (context1) => new SingleFieldDialog(text: HONG_BAO))
                                                    .Then(_ => { Navigator.of(context).pop(); });
                                            }
                                        }
                                    );
                                }
                            );
                        }
                    )
                }
            );
        }

        public class SingleFieldDialog : StatelessWidget
        {
            private string mText;

            public SingleFieldDialog(string text)
            {
                mText = text;
            }

            public override Widget build(BuildContext context)
            {
                return new Dialog(
                    child: new Container(
                        padding: EdgeInsets.all(16),
                        child: new TextField(
                            maxLines: 5,
                            autofocus: true,
                            controller: new TextEditingController(text: mText)
                        )
                    )
                );
            }
        }


        public class ReceiptDialog : StatelessWidget
        {
            public string mImage;

            public ReceiptDialog(string image)
            {
                mImage = image;
            }

            public static ReceiptDialog Wechat()
            {
                return new ReceiptDialog(image: "wechat");
            }

            public static ReceiptDialog Alipay()
            {
                return new ReceiptDialog(image: "alipay");
            }


            static BorderRadius BorderRadius = BorderRadius.circular(5);

            public override Widget build(BuildContext context)
            {
                return new Dialog(
                    shape: new RoundedRectangleBorder(borderRadius: BorderRadius),
                    child: new ClipRRect(borderRadius: BorderRadius, child: Image.asset(mImage))
                );
            }
        }

        public class ActionTile : StatelessWidget
        {
            private VoidCallback mOnTap;

            private string mText;

            public ActionTile(VoidCallback onTap, string text)
            {
                mOnTap = onTap;
                mText = text;
            }


            public override Widget build(BuildContext context)
            {
                return new InkWell(
                    onTap: () => mOnTap(),
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
    }
}