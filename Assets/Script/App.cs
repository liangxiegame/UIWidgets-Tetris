using System.Collections;
using System.Collections.Generic;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;

namespace TerisGame
{
    public class App : UIWidgetsPanel
    {
        public static RouteObserve<ModalRoute> RouteObserver = new RouteObserve<ModalRoute>();

        protected override Widget createWidget()
        {
            return new MaterialApp( //
                title: "tetris",
//                localizationsDelegates: new List<LocalizationsDelegate<MaterialLocalizations>>()
//                {
//                    S.Delegate
//                }
                navigatorObservers: new List<NavigatorObserver> {RouteObserver},
                supportedLocales: S.Delegate.SupportedLocales,
                theme: new ThemeData(
                    primarySwatch: Colors.blue
                ),
                home: new Scaffold(
                    body: new Sound(child: new HomePage())
                )
            );
        }


        public const  float SCREEN_BORDER_WIDTH = 3.0f;
        public static Color BACKGROUND_COLOR    = new Color(0xffefcc19);

        class HomePage : StatelessWidget
        {
            public override Widget build(BuildContext context)
            {
                //only Android/iOS support land mode
//                bool supportLandMode = Platform.isAndroid || Platform.isIOS;
//                bool land = supportLandMode &&
//                            MediaQuery.of(context).orientation == Orientation.landscape;

//                return land ? PageLand() : PagePortrait();

                return new Text("测试");
            }
        }
    }
}