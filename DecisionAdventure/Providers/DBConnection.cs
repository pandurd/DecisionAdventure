using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace DecisionAdventure.Providers
{
    public class DBConnection : IDBConnection
    {
        private readonly ILogger<DBConnection> _logger;

        public DBConnection(ILogger<DBConnection> logger)
        {
            _logger = logger;
        }

        public SqlConnection GetSqlConnection()
        {
            //Should be injected via env variable
            //var connectionString = $"Server=localhost\\SQLEXPRESS;Database=Adventure;Trusted_Connection=True;TrustServerCertificate=True";
            //for github
            var encodedPassword = "UGFzc3dvcmQx";
            var decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedPassword));
            var connectionString = $"Server=tcp:advinterview.database.windows.net,1433;Initial Catalog=adventure;Persist Security Info=False;User ID=dev;Password={decodedPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            
            var connection = new SqlConnection(connectionString);

            try
            {    
                connection.Open();
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to connect to DB.", ex);
                throw;
            }

            return connection;
        }
    }
}
