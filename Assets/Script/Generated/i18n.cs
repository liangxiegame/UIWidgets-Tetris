using System.Collections.Generic;
using RSG;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TerisGame
{
    public class S : DefaultMaterialLocalizations
    {
        public static GeneratedLocalizationsDelegate Delegate = new GeneratedLocalizationsDelegate();


        public static S of(BuildContext context)
        {
            return Localizations.of<S>(context, typeof(MaterialLocalizations));
        }

        public virtual string Cleans => "Cleans";
        public virtual string Level => "Level";
        public virtual string Next => "Next";
        public virtual string PauseResume => "PAUSE/RESUME";
        public virtual string Points => "Points";
        public virtual string Reset => "RESET";
        public virtual string Reward => "Reward";
        public virtual string Sounds => "SOUNDS";
    }

    public class en : S
    {
    }

    public class zh_CN : S
    {
        public override string Next => "下一个";
        public override string Reward => "赞赏";
        public override string Sounds => "声音";
        public override string PauseResume => "暂停/恢复";
        public override string Level => "级别";
        public override string Reset => "重置";
        public override string Cleans => "消除";
        public override string Points => "分数";
    }


    public class GeneratedLocalizationsDelegate : LocalizationsDelegate<MaterialLocalizations>
    {
        public List<Locale> SupportedLocales =>
            new List<Locale>()
            {
                new Locale("zh", "CN"),
                new Locale("en", "US")
            };


        public override IPromise<object> load(Locale locale)
        {
            var lang = GetLang(locale);

            if (Application.systemLanguage == SystemLanguage.Chinese ||
                Application.systemLanguage == SystemLanguage.ChineseSimplified)
            {
                lang = "zh_CN";
            }

            if (lang != null)
            {
                switch (lang)
                {
                    case "en_US":
                        return new Promise<object>((action, action1) => action(new en()));
                    case "zh_CN":
                        return new Promise<object>((action, action1) => action(new zh_CN()));
                }
            }

            return new Promise<object>((action, action1) => action(new S()));
        }


        public override bool isSupported(Locale locale)
        {
            return locale != null && SupportedLocales.Contains(locale);
        }

        public override bool shouldReload(LocalizationsDelegate old)
        {
            return false;
        }

        string GetLang(Locale l)
        {
            return l == null
                ? null
                : l.countryCode != null && l.countryCode.isEmpty()
                    ? l.languageCode
                    : l.ToString();
        }
    }
}