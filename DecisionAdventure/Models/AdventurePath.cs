using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecisionAdventure.Models
{
    public class AdventurePath
    {
        public Guid ID { get; set; }
        public Guid AdventureID { get; set; }
        public Guid? PreviousAnswer { get; set; }
        public string Question { get; set; }
        public int Level { get; set; }
        public List<AdventurePathOption> Options { get; set; }
    }
}
