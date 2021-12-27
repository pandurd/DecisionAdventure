using System;
using System.ComponentModel.DataAnnotations;

namespace DecisionAdventure.Models
{
    public class SaveStepRequest
    {
        [Required]
        public Guid userJourneyID { get; set; }

        [Required]
        public Guid adventureID { get; set; }

        [Required]
        public Guid currentPathID { get; set; }

        [Required]
        public Guid selectedOptionID { get; set; }
    }
}
