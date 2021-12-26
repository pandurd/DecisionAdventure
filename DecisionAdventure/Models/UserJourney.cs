using System;
using System.Collections.Generic;

namespace DecisionAdventure.Models
{
    public class UserJourney
    {
        public UserAdventure Adventure { get; set; }
        public List<UserJourneyPath> PathOptions { get; set; }
    }

    public class UserJourneyPath
    {
        public Guid ID { get; set; }
        public string Question { get; set; }
        public string Label { get; set; }
        public bool IsSelected { get; set; }
        public Guid AnswerID { get; set; }
        public Guid ParentID { get; set; }
        public Guid PreviousAnswer { get; set; }
    }


}
