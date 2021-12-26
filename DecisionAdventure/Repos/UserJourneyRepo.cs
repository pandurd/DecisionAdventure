using DecisionAdventure.Providers;
using System;
using System.Threading.Tasks;
using Dapper;
using DecisionAdventure.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DecisionAdventure.Repos
{
    public class UserJourneyRepo : IUserJourneyRepo
    {
        private readonly IDBConnection _db;

        private readonly ILogger<UserJourneyRepo> _logger;
        public UserJourneyRepo(IDBConnection connection, ILogger<UserJourneyRepo> logger)
        {
            _db = connection;
            _logger = logger;
        }

        public async Task AddNewUserJourney(Guid ID, string UserName, Guid AdventureID)
        {
            using (var conn = _db.GetSqlConnection())
            {
                try
                {
                    var insertAdventureJourneyQuery = @"INSERT INTO [dbo].[UserJourney] (ID, UserName, AdventureID)
                                                    VALUES (@ID, @UserName, @AdventureID)";
                    await conn.ExecuteAsync(insertAdventureJourneyQuery,
                        new { ID, UserName, AdventureID });
                
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error while starting a new journey ", ex);
                    throw;
                }
            }         
        }

        public async Task AddAdventureSelection(Guid ID, Guid UserJourneyID, Guid PathID, Guid? OptionID)
        {
            using (var conn = _db.GetSqlConnection())
            {
                try
                {
                    var insertAdventureSelectionQuery = @"INSERT INTO [dbo].[UserAdventure] (ID, UserJourneyID, PathID, OptionID)
                                                        VALUES (@ID, @UserJourneyID, @PathID, @OptionID)";
                    await conn.ExecuteAsync(insertAdventureSelectionQuery,
                        new { ID, UserJourneyID, PathID, OptionID });
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error while adding an selection ", ex);
                    throw;
                }
            }
        }

        public async Task<UserJourney> GetDecisionTree(Guid userJourneyID)
        {
            var result = new UserJourney();

            using (var conn = _db.GetSqlConnection())
            {
                try
                {
                    var getAdventureQuery = @"SELECT [ID], [UserName], [AdventureID]
                                FROM [dbo].[UserJourney] (nolock)
                                WHERE ID = @userJourneyID
                            ";
                    var adventure = await conn.QueryFirstAsync<UserAdventure>(getAdventureQuery, new { userJourneyID });

                    result.Adventure = adventure;

                    var getChoosenOptions = @"SELECT
	                                   ap.ID
	                                  ,ap.Question
	                                  ,apo.Label
	                                  ,CASE WHEN apo.ID = ua.OptionID then 1 
			                                ELSE 0 
	                                    END as IsSelected
                                    ,apo.ID as AnswerID
                                    ,ap.ID as ParentID
                                    ,ap.PreviousAnswer as PreviousAnswer

                              FROM [dbo].[UserAdventure] ua (nolock)
                              INNER JOIN [dbo].[AdventurePath] ap (nolock) on ap.ID = ua.PathID
                              LEFT OUTER JOIN [dbo].[AdventurePathOption] apo (nolock) on apo.PathID = ua.PathID
                              WHERE ua.UserJourneyID = @UserJourneyID
                              ORDER BY ap.Level
                           ";

                    var pathOptions = await conn.QueryAsync<UserJourneyPath>(getChoosenOptions, new { userJourneyID });
                    result.PathOptions = pathOptions.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error while retrieving DecisionTree", ex);
                    throw;
                }
            }

            return result;
        }

        public async Task<List<UserAdventure>> GetAllJourneys()
        {
            var result = new List<UserAdventure>();
            using (var conn = _db.GetSqlConnection())
            {
                try
                {
                    var getAdventuresQuery = @"SELECT uj.ID, uj.UserName, uj.AdventureID, a.Name as AdventureName 
                                    FROM [dbo].[USERJOURNEY] uj(nolock)
                                    INNER JOIN[dbo].[Adventure] a(nolock) on a.ID = uj.AdventureID";
                    var res = await conn.QueryAsync<UserAdventure>(getAdventuresQuery);
                    result = res.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error while retrieving GetAllJourneys", ex);
                    throw;
                }
            }

            return result;
        }
    }
}
