
using Langchips.Models.Helpers;
using Langchips.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace langchips_project.Services
{
    public static class InputHelper //TODO: copied from old proj- adjust
    {
        public static bool GetValidBooleanInput(string message)
        {
            bool isValid = false;
            bool result = false;
            do
            {
                Console.WriteLine(message);
                string userInput = Console.ReadLine().Trim().ToLower();
                if (userInput == "true" || userInput == "false" || userInput == "yes" || userInput == "no")
                {
                    isValid = true;
                    result = userInput == "true" || userInput == "yes";
                }
                else if (userInput == "1" || userInput == "0")
                {
                    isValid = true;
                    result = userInput == "1"; 
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'true', 'false', 'yes', 'no' (case-insensitive), or '1' or '0'.");
                }
            } while (!isValid);
            return result;
        }
        public static string ValidateEntry(string message)
        {
            string entry;
            do
            {
                Console.WriteLine(message);
                entry = Console.ReadLine().Trim();
                if (entry.Length == 0)
                {
                    Console.WriteLine("Entry can't be empty. Please try again.");
                }
            } while (entry.Length == 0);
            return entry;
        }

        public static Guid ParseGuid(string input)
        {
            if (Guid.TryParse(input, out Guid result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Input string is not a valid GUID.", nameof(input));
            }
        }
        public static Language GetLanguage(IEnumerable<Language> languages, string input)
        {
            Language language;

            if (!LanguageHelper.TryParseLanguage(input, out language) || !languages.Contains(language))
            {
                throw new ArgumentException($"Invalid language. Please enter a valid language from the allowed languages.");
            }

            return language;
        }

    }
}
