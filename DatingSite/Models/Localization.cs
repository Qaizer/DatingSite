using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace DatingSite.Models
{
    public class Localization
    {
        public static List<Language> Languages = new List<Language>
        {
            new Language("English", "en"),
            new Language("Svenska", "sv")
        };

        public void SetLanguage(string language)
        {
            if (!Exists(language))
            {
                language = Default();
            }
            var culture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture.Name);
            HttpCookie cookie = new HttpCookie("culture", language);
            cookie.Expires = DateTime.Now.AddYears(5);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string Default()
        {
            return Languages.FirstOrDefault().Suffix;
        }

        public static bool Exists(string language)
        {
            return Languages.Any(x => x.Suffix == language);
        }
    }

    public class Language
    {
        public Language(string name, string suffix)
        {
            this.Name = name;
            this.Suffix = suffix;
        }
        public string Name { get; }
        public string Suffix { get; }
    }

}



