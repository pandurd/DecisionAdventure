using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DecisionAdventure.Models
{
    public class UserAdventure
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public Guid AdventureID { get; set; }

        [Required]
        public string AdventureName { get; set; }
    }

    public class UserAdventurePath
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public bool IsSelected { get; set; }
    }

    public class UserAdventureJourney
    {
        public UserAdventure Adventure { get; set; }
        public List<UserAdventurePath> PathOptions { get; set; }
    }
}
