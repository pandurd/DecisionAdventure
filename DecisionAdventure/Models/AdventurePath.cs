using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DecisionAdventure.Models
{
    public class AdventurePath
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public Guid AdventureID { get; set; }

        public Guid? PreviousAnswer { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Question { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public List<AdventurePathOption> Options { get; set; }
    }
}
