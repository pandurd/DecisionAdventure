using DecisionAdventure.Models;
using DecisionAdventure.Repos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecisionAdventure.Services
{
    public class AdventureService : IAdventureService
    {
        private readonly IAdventureRepo _adventureRepo;

        public AdventureService(IAdventureRepo adventureRepo)
        {
            _adventureRepo = adventureRepo;
        }

        public async Task<Adventure> CreateAdventure(string name)
        {
            var newAdventureID = Guid.NewGuid();
            var newAdventure = new Adventure() { ID = newAdventureID, Name = name };
            
            await _adventureRepo.CreateAdventure(newAdventure);

            return newAdventure;
        }
        public async Task AddAdventurePath(AdventurePath path)
        {
            await _adventureRepo.CreateAdventurePath(path);
        }

        public async Task<List<Adventure>> GetAdventures()
        {
            return await _adventureRepo.GetAdventures();
        }
    }
}
