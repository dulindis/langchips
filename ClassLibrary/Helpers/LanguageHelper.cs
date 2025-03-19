using Langchips.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Models.Helpers
{
    public static class LanguageHelper
    {
        private static readonly Dictionary<string, Language> _languageMap = new Dictionary<string, Language>(StringComparer.OrdinalIgnoreCase)
        {
            { "English", Language.English },
            { "EN", Language.English },
            { "Polish", Language.Polish },
            { "PL", Language.Polish },
            { "Swedish", Language.Swedish },
            { "SE", Language.Swedish },
            { "Spanish", Language.Spanish },
            { "ES", Language.Spanish },
            { "German", Language.German },
            { "DE", Language.German }
        };

        public static bool TryParseLanguage(string input, out Language language)
        {
            return _languageMap.TryGetValue(input, out language);
        }
    }
}
