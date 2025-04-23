using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PlaywrightWebTemplate.Helpers
{
    internal class GeneralHelpers
    {
        public static string GetProjectPath()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().Location;

            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));

            return new Uri(actualPath).LocalPath;
        }

        public static string ReadValueInFile(string file)
        {
            //used to read value in sql file
            string text = File.ReadAllText(file);
            return text;
        }
        public static string ReplaceValuesInFile(string text, string currentValue, string newValue)
        {
            //used to replace values in sql files
            text = text.Replace(currentValue, newValue);
            return text;
        }

        public static string GetStringWithRandomNumbers(int size)
        {
            Random random = new Random();

            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, size)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GetStringWithRandomChars(int size)
        {
            Random random = new Random();

            const string chars = "abcdefghijklmnopqrstuvxzwy";
            return new string(Enumerable.Repeat(chars, size)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }

}