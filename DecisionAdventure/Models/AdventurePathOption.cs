using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecisionAdventure.Models
{
    public class AdventurePathOption
    {
        public Guid ID { get; set; }
        public Guid PathID { get; set; }
        public string Label { get; set; }
    }
}
