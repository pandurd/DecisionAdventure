using System;
using System.ComponentModel.DataAnnotations;

namespace DecisionAdventure.Models
{
    public class StartRequest
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
