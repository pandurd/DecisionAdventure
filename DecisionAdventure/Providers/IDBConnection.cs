using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DecisionAdventure.Providers
{
    public interface IDBConnection
    {
        SqlConnection GetSqlConnection();
    }
}
