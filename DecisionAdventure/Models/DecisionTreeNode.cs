using System;
using System.Collections.Generic;

namespace DecisionAdventure.Models
{
    public class DecisionTreeNode
    {
        public DecisionTreeNode()
        {
            children = new List<DecisionTreeNode>();
        }

        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public string Label { get; set; }
        public bool IsQuestion { get; set; }
        public bool IsSelected { get; set; }
        public List<DecisionTreeNode> children { get; set; }
    }
}
