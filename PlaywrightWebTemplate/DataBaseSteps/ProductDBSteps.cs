using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaywrightWebTemplate.Helpers;

namespace PlaywrightWebTemplate.DataBaseSteps
{
    public class ProductDBSteps
    {
        public static List<string> GetProduct(string idProduct)
        {
            string queryFile = GeneralHelpers.GetProjectPath() + @"Queries\Product\GetProduct.sql";

            string query = GeneralHelpers.ReadValueInFile(queryFile);
            query = GeneralHelpers.ReplaceValuesInFile(query, "{idProduct}", idProduct);

            List<string> result = DataBaseHelpers.GetDataQuery(query);

            return result;
        }

     

    }
}
