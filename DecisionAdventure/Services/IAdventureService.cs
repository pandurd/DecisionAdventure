using DecisionAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecisionAdventure.Services
{
    public interface IAdventureService
    {
        Task<Adventure> CreateAdventure(string name);
        Task AddAdventurePath(AdventurePath path);
        Task<List<Adventure>> GetAdventures();
    }
}
