using System.Collections.Generic;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;

namespace TerisGame
{
    public class App : UIWidgetsPanel
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"), "Material Icons");
        }

        public static RouteObserve<ModalRoute> RouteObserver = new RouteObserve<ModalRoute>();

        protected override Widget createWidget()
        {
            return new MaterialApp( //
                title: "tetris",
                localizationsDelegates: new List<LocalizationsDelegate<MaterialLocalizations>>()
                {
                    S.Delegate,
                    DefaultMaterialLocalizations.del,
                },
                navigatorObservers: new List<NavigatorObserver> {RouteObserver},
                supportedLocales: S.Delegate.SupportedLocales,
                theme: new ThemeData(
                    primarySwatch: Colors.blue
                ),
                home: new Scaffold(
                    body: new Sound(child:
                        new Game(child:
                            new KeyboardController(child:
                                new HomePage()
                            )
                        )
                    )
                )
            );
        }


        public const float SCREEN_BORDER_WIDTH = 3.0f;


        public static Color BACKGROUND_COLOR  = new Color(0xffefcc19);
        public static Color SCREEN_BACKGROUND = new Color(0xff9ead86);

        class HomePage : StatelessWidget
        {
            public override Widget build(BuildContext context)
            {
                //only Android/iOS support land mode
                bool supportLandMode = Application.platform == RuntimePlatform.Android ||
                                       Application.platform == RuntimePlatform.IPhonePlayer;
                bool land = supportLandMode &&
                            MediaQuery.of(context).orientation == Orientation.landscape;

                return land ? new PageLand() as Widget : new PagePortrait();
            }
        }
    }
}