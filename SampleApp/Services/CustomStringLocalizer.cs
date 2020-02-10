using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace SampleApp.Services
{
    public class CustomStringLocalizer : IStringLocalizer
    {
        // ключи ресурсов
        private const string HEADER = "Header";
        private const string MESSAGE = "Message";
        private Dictionary<string, Dictionary<string, string>> resources;

        public CustomStringLocalizer()
        {
            // словарь для английского языка
            var enDict = new Dictionary<string, string>
            {
                {HEADER, "Welcome"},
                {MESSAGE, "Hello World!"}
            };
            // словарь для русского языка
            var ruDict = new Dictionary<string, string>
            {
                {HEADER, "Добо пожаловать"},
                {MESSAGE, "Привет мир!"}
            };
            // словарь для немецкого языка
            var deDict = new Dictionary<string, string>
            {
                {HEADER, "Willkommen"},
                {MESSAGE, "Hallo Welt!"}
            };
            // создаем словарь ресурсов
            resources = new Dictionary<string, Dictionary<string, string>>
            {
                {"en", enDict},
                {"ru", ruDict},
                {"de", deDict}
            };
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return this;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var currentCulture = CultureInfo.CurrentUICulture;
                string val = "";
                if (resources.ContainsKey(currentCulture.Name))
                {
                    if (resources[currentCulture.Name].ContainsKey(name))
                    {
                        val = resources[currentCulture.Name][name];
                    }
                }
                return new LocalizedString(name, val);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();
    }
}