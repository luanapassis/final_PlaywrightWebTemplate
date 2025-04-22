using Microsoft.Data.SqlClient;

namespace PlaywrightWebTemplate.Helpers
{
    public class DataBaseHelpers
    {
        private SqlConnection GetDBConnection()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = $"{JsonHelpers.GetParameterAppSettings("DB_URL")},1433",
                InitialCatalog = JsonHelpers.GetParameterAppSettings("DB_NAME"),
                UserID = JsonHelpers.GetParameterAppSettings("DB_USER"),
                Password = JsonHelpers.GetParameterAppSettings("DB_PASSWORD"),
                TrustServerCertificate = true
            };

            return new SqlConnection(builder.ConnectionString);
        }

        public void ExecuteQuery(string query)
        {
            using (var connection = GetDBConnection())
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.CommandTimeout = int.Parse(JsonHelpers.GetParameterAppSettings("DB_CONNECTION_TIMEOUT"));
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> GetDataQuery(string query, string[,] parametersArray = null)
        {
            var resultList = new List<string>();

            using (var connection = GetDBConnection())
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.CommandTimeout = int.Parse(JsonHelpers.GetParameterAppSettings("DB_CONNECTION_TIMEOUT"));

                if (parametersArray != null)
                {
                    for (int i = 0; i < parametersArray.GetLength(0); i++)
                    {
                        cmd.Parameters.AddWithValue(parametersArray[i, 0], parametersArray[i, 1]);
                    }
                }

                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            resultList.Add(reader[i]?.ToString());
                        }
                    }
                }
            }

            return resultList.Count > 0 ? resultList : null;
        }

        public List<string> GetListOfDataQuery(string query)
        {
            var resultList = new List<string>();

            using (var connection = GetDBConnection())
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.CommandTimeout = int.Parse(JsonHelpers.GetParameterAppSettings("DB_CONNECTION_TIMEOUT"));

                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) return null;

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            resultList.Add(reader[i]?.ToString());
                        }
                    }
                }
            }

            return resultList.Count > 0 ? resultList : null;
        }
    }
}
