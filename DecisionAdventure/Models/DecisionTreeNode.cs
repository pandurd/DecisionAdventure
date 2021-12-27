using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DecisionAdventure.Models
{
    public class DecisionTreeNode
    {
        public DecisionTreeNode()
        {
            children = new List<DecisionTreeNode>();
        }

        [Required]
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public bool IsQuestion { get; set; }

        [Required]
        public bool IsSelected { get; set; }

        public List<DecisionTreeNode> children { get; set; }
    }
}
