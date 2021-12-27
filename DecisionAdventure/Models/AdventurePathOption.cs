using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DecisionAdventure.Models
{
    public class AdventurePathOption
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public Guid PathID { get; set; }

        [Required]
        public string Label { get; set; }
    }
}
