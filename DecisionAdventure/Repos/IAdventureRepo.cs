using DecisionAdventure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecisionAdventure.Repos
{
    public interface IAdventureRepo
    {
        Task CreateAdventure(Adventure adventure);
        Task CreateAdventurePath(AdventurePath adventurePath);
        Task<List<Adventure>> GetAdventures();
        Task<AdventurePath> GetNextAdventurePath(Guid adventureID, Guid? currentPathID, Guid? selectedOptionID);
    }
}