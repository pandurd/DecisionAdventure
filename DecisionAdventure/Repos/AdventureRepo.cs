using DecisionAdventure.Models;
using DecisionAdventure.Providers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Dapper;

namespace DecisionAdventure.Repos
{
    public class AdventureRepo : IAdventureRepo
    {
        private readonly IDBConnection _db;
        private readonly ILogger<AdventureRepo> _logger;
        public AdventureRepo(IDBConnection connection, ILogger<AdventureRepo> logger)
        {
            _db = connection;
            _logger =  logger;
        }

        //add no lock to all queries
        public async Task CreateAdventure(Adventure adventure)
        {
            using(var conn = _db.GetSqlConnection())
            {
                try
                {
                    var createAdventureQuery = "INSERT INTO ADVENTURE (ID, NAME) VALUES (@ID, @Name)";
                    await conn.ExecuteAsync(createAdventureQuery, new { ID = adventure.ID, Name = adventure.Name });

                }
                catch (Exception ex)
                {
                    _logger.LogError("Could not add an Adeventure", ex);
                    throw;
                }
            }
        }

        public async Task CreateAdventurePath(AdventurePath adventurePath)
        {
            var optionDataTable = new DataTable();

            optionDataTable.Columns.Add("ID");
            optionDataTable.Columns.Add("AdventureID");
            optionDataTable.Columns.Add("Label");

            foreach(var option in adventurePath.Options)
            {
                optionDataTable.Rows.Add(option.ID, option.PathID, option.Label);
            }

            using (var conn = _db.GetSqlConnection())
            {
                try
                {
                    var InsertAdventurePathQuery = "InsertAdventurePath";

                    var insertParams = new DynamicParameters();
                    insertParams.Add("ID", adventurePath.ID, DbType.Guid);
                    insertParams.Add("AdventureID", adventurePath.AdventureID, DbType.Guid, ParameterDirection.Input);
                    insertParams.Add("PreviousAnswer", adventurePath.PreviousAnswer, DbType.Guid, ParameterDirection.Input);
                    insertParams.Add("Question", adventurePath.Question, DbType.String, ParameterDirection.Input, 200);
                    insertParams.Add("Level", adventurePath.Level, DbType.Int32, ParameterDirection.Input);
                    insertParams.Add("Options", optionDataTable.AsTableValuedParameter("[dbo].[AdventurePathOption]"));
                    insertParams.Add("Error", null, DbType.String, direction: ParameterDirection.Output, 5000);

                    await conn.ExecuteAsync(
                        InsertAdventurePathQuery,
                        insertParams, null, 2400, commandType: CommandType.StoredProcedure);
                }
                catch(Exception ex)
                {
                    _logger.LogError("Error while adding a path", ex);
                    throw;
                }
            }
        }

        public async Task<AdventurePath> GetNextAdventurePath(Guid adventureID, Guid? currentPathID, Guid? selectedOptionID)
        {
            var result = new AdventurePath();

            using (var conn = _db.GetSqlConnection())
            {
                try
                {
                    var getNextPathQuery = @"SELECT ap.ID, ap.AdventureID, ap.PreviousAnswer, ap.Question, ap.Level 
                                        FROM [dbo].[AdventurePath] ap (nolock)
                                        WHERE ap.PreviousAnswer is null and ap.AdventureID = @adventureID";

                    if (selectedOptionID != null)
                        getNextPathQuery = @"SELECT ap.ID, ap.AdventureID, ap.PreviousAnswer, ap.Question, ap.Level 
                                        FROM [dbo].[AdventurePath] ap (nolock)
                                        WHERE ap.PreviousAnswer = @selectedOptionID and ap.AdventureID = @adventureID";

                    var getOptionsQuery = @"SELECT apo.ID, apo.PathID, apo.Label FROM [dbo].[AdventurePathOption] apo (nolock)
                                            WHERE apo.PathID = @pathID ";

                    var path = await conn.QueryFirstAsync<AdventurePath>(getNextPathQuery, new { selectedOptionID, adventureID });
                    var options = await conn.QueryAsync<AdventurePathOption>(getOptionsQuery, new { pathID = path.ID });

                    path.Options = options.ToList();
                    result = path;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Could not get next path", ex);
                    throw;
                }
            }

            return result;
        }

        public async Task<List<Adventure>> GetAdventures()
        {
            var result = new List<Adventure>();
            using (var conn = _db.GetSqlConnection())
            {
                try
                {
                    var getAdventuresQuery = "SELECT ID, NAME FROM ADVENTURE (nolock)";
                    var res = await conn.QueryAsync<Adventure>(getAdventuresQuery);
                    result = res.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error while retrieving GetAdventures", ex);
                    throw;

                }
            }

            return result;
        }

    }
}
