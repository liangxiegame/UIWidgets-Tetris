using System.Collections.Generic;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class App : UIWidgetsPanel
    {
        protected override Widget createWidget()
        {
            return new MaterialApp(
                localizationsDelegates: new List<LocalizationsDelegate<MaterialLocalizations>>()
                {
                    CustomLocalizationsDelegete.del,
                    DefaultMaterialLocalizations.del
                },
                supportedLocales: new List<Locale>()
                {
                    new Locale("zh", "CN"),
                    new Locale("en", "US")
                },
                home:
                new Scaffold(
                    body: new GBUserInterface()
                )
            );
        }
    }
}