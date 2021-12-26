using DecisionAdventure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecisionAdventure.Repos
{
    public interface IUserJourneyRepo
    {
        Task AddNewUserJourney(Guid ID, string UserName, Guid AdventureID);
        Task AddAdventureSelection(Guid ID, Guid UserJourneyID, Guid PathID, Guid? OptionID);
        Task<List<UserAdventure>> GetAllJourneys();
        Task<UserJourney> GetDecisionTree(Guid userAdventureID);
    }
}
