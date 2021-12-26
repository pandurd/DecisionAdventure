using System;

namespace DecisionAdventure.Models
{
    public class SaveStepRequest
    {
        public Guid userJourneyID { get; set; }
        public Guid adventureID { get; set; }
        public Guid currentPathID { get; set; }
        public Guid selectedOptionID { get; set; }
    }
}
