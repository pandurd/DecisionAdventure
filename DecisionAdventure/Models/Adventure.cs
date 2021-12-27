using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DecisionAdventure.Models
{
    public class Adventure
    {
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
