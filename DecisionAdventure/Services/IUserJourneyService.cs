using DecisionAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecisionAdventure.Services
{
    public interface IUserJourneyService
    {
        Task<dynamic> StartJourney(Guid adventureID, string userName);
        Task<AdventurePath> SelectNextStep(Guid userJourneyID, Guid adventureID, Guid currentPathID, Guid selectedOptionID);
        Task<DecisionTreeNode> GetDecisionTree(Guid userJourneyID);
        Task<List<UserAdventure>> GetAllJourneys();
    }
}
