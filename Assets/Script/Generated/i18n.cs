using System.Collections.Generic;
using RSG;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TerisGame
{
    public class S : WidgetsLocalizations
    {
        public static GeneratedLocalizationsDelegate Delegate = new GeneratedLocalizationsDelegate();


        public static S of(BuildContext context)
        {
            return Localizations.of<S>(context, typeof(S));
        }

        public virtual string Cleans      => "Cleans";
        public virtual string Level       => "Level";
        public virtual string Next        => "Next";
        public virtual string PauseResume => "PAUSE/RESUME";
        public virtual string Points      => "Points";
        public virtual string Reset       => "RESET";
        public virtual string Reward      => "Reward";
        public virtual string Sounds      => "SOUNDS";
    }

    public class en : S
    {
    }

    public class zh_CN : S
    {
        public override string Next        => "下一个";
        public override string Reward      => "赞赏";
        public override string Sounds      => "声音";
        public override string PauseResume => "暂停/恢复";
        public override string Level       => "级别";
        public override string Reset       => "重置";
        public override string Cleans      => "消除";
        public override string Points      => "分数";
    }

    public class GeneratedLocalizationsDelegate : LocalizationsDelegate<S>
    {
        public List<Locale> SupportedLocales =>
            new List<Locale>()
            {
                new Locale("en", ""),
                new Locale("zh", "CN")
            };


        public LocaleListResolutionCallback ListResolution(Locale fallback)
        {
            return (locales, supported) =>
            {
                if (locales == null || locales.isEmpty())
                {
                    return fallback ?? supported.first();
                }
                else
                {
                    return Resolve(locales.first(), fallback, supported);
                }
            };
        }

        public LocaleResolutionCallback Resolution(Locale fallback)
        {
            return (locale, supported) => { return Resolve(locale, fallback, supported); };
        }

        public Locale Resolve(Locale locale, Locale fallback, List<Locale> supported)
        {
            if (locale == null && !isSupported(locale))
            {
                return fallback ?? supported.first();
            }

            var languageLocale = new Locale(locale.languageCode, "");
            if (supported.Contains(locale))
            {
                return locale;
            }
            else if (supported.Contains(languageLocale))
            {
                return languageLocale;
            }
            else
            {
                Locale fallbackLocale = fallback ?? supported.first();
                return fallbackLocale;
            }
        }


        public override IPromise<object> load(Locale locale)
        {
            var lang = GetLang(locale);
            if (lang != null)
            {
                switch (lang)
                {
                    case "en":

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