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
        public static string GetCurrentSolutionFolderPath()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().Location;

            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));

            return new Uri(actualPath).LocalPath;
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