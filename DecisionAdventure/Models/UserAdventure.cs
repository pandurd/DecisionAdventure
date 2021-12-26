using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecisionAdventure.Models
{
    public class UserAdventure
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public Guid AdventureID { get; set; }
        public string AdventureName { get; set; }
    }

    public class UserAdventurePath
    {
        public Guid ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Level { get; set; }
        public bool IsSelected { get; set; }
    }

    public class UserAdventureJourney
    {
        public UserAdventure Adventure { get; set; }
        public List<UserAdventurePath> PathOptions { get; set; }
    }
}
