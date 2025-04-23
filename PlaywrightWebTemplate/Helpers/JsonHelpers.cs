using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PlaywrightWebTemplate.Helpers
{
    internal class JsonHelpers
    {
        private static JObject json;
        public static JObject GetJsonObject(string filename)
        {
            try
            {
                json = JObject.Parse(File.ReadAllText(GeneralHelpers.GetProjectPath() + filename));

                return json;
            }
            catch (Exception)
            {
                throw new Exception("Json " + filename + " não encontrado");
            }

        }

        //Do not use if the environment is controlled by environment.json
        public static string GetParameterSingleAppSettings(string param)
        {
            try
            {
                JObject json = GetJsonObject("appsettings.json");
                return json[param].Value<string>();
            }
            catch (Exception)
            {
                throw new Exception("Parâmetro " + param + " não encontrado no AppSettings");
            }

        }

        public static string GetEnvironment(string param)
        {
            try
            {
                JObject json = GetJsonObject("environment.json");
                return json[param].Value<string>();
            }
            catch (Exception)
            {
                throw new Exception("Parameter " + param + " not found at AppSettings");
            }
        }

        public static string GetParameterAppSettings(string param)
        {
            string value = "";
            string environment = GetEnvironment("ENVIRONMENT");
            if (environment == "QA")
            {
                JObject jsonQa = GetJsonObject("appsettingsQA.json");
                value = jsonQa[param].Value<string>();
            }
            else if (environment == "UAT")
            {
                JObject jsonQa = GetJsonObject("appsettingsUAT.json");
                value = jsonQa[param].Value<string>();
            }
            else if (environment == "DEV")
            {
                JObject jsonQa = GetJsonObject("appsettingsDEV.json");
                value = jsonQa[param].Value<string>();
            }
            else
            {
                throw new Exception("Environment " + environment + " not found at environment.json");
            }
            return value;
        }

        public static string GetParameterFromJsonFile(string filename, string param)
        {
            try
            {
                JObject json = GetJsonObject(filename);
                return json[param].Value<string>();
            }
            catch (Exception)
            {
                throw new Exception("Parameter " + param + "not found at " + filename);
            }

        }



    }
}
