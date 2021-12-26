using DecisionAdventure.Models;
using DecisionAdventure.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecisionAdventure.Services
{
    public class UserJourneyService : IUserJourneyService
    {
        private readonly IAdventureRepo _adventureRepo;
        private readonly IUserJourneyRepo _userJourneyRepo;
        public UserJourneyService(IAdventureRepo adventureRepo, IUserJourneyRepo userJourneyRepo)
        {
            _adventureRepo = adventureRepo;
            _userJourneyRepo = userJourneyRepo;
        }

        public async Task<dynamic> StartJourney(Guid adventureID, string userName)
        {
            var newJourneyID = Guid.NewGuid();
            await _userJourneyRepo.AddNewUserJourney(newJourneyID, userName, adventureID);

            var firstPath = await _adventureRepo.GetNextAdventurePath(adventureID, null, null);
            return new { newJourneyID, firstPath };
        }
        public async Task<AdventurePath> SelectNextStep(Guid userJourneyID, Guid adventureID, Guid currentPathID, Guid selectedOptionID)
        {
            await _userJourneyRepo.AddAdventureSelection(Guid.NewGuid(), userJourneyID, currentPathID, selectedOptionID);
            var nextPath = await _adventureRepo.GetNextAdventurePath(adventureID, currentPathID, selectedOptionID);
            
            if(nextPath.Options.Count == 0)
                await _userJourneyRepo.AddAdventureSelection(Guid.NewGuid(), userJourneyID, nextPath.ID, null);

            return nextPath;
        }

        public async Task<DecisionTreeNode> GetDecisionTree(Guid userJourneyID)
        {
            var result = await _userJourneyRepo.GetDecisionTree(userJourneyID);

            var root = new DecisionTreeNode() 
            { 
                Label = result.PathOptions[0].Question,
                ID = result.PathOptions[0].ID,
                ParentID = Guid.Empty,
                IsQuestion = true
            };

            foreach (var item in result.PathOptions)
            {
                var parentNode = SearchTree(root, item.ParentID, item.PreviousAnswer);

                if (parentNode != null)
                {
                    var isQuestion = !parentNode.IsQuestion;

                    parentNode.children.Add(new DecisionTreeNode()
                    {
                        ID = item.AnswerID,
                        IsQuestion = isQuestion,
                        IsSelected = item.IsSelected,
                        Label = isQuestion ? item.Question : item.Label,
                        ParentID = root.ID
                    });
                }
                else
                {
                    var answerNode = SearchTree(root, item.PreviousAnswer, item.PreviousAnswer);

                    answerNode.children.Add(new DecisionTreeNode()
                    {
                        ID = item.ID,
                        IsQuestion = true,
                        IsSelected = false,
                        Label = item.Question,
                        ParentID = answerNode.ID
                    });

                    if (item.Label != null)
                    {
                        answerNode.children[0].children.Add(new DecisionTreeNode()
                        {
                            ID = item.AnswerID,
                            IsQuestion = false,
                            IsSelected = item.IsSelected,
                            Label = item.Label,
                            ParentID = item.ID
                        });
                    }
                }
            }
            
            return root;
        }

        public async Task<List<UserAdventure>> GetAllJourneys()
        {
            var result = await _userJourneyRepo.GetAllJourneys();
            return result;
        }

        private DecisionTreeNode SearchTree(DecisionTreeNode tree, Guid ParentID, Guid? PreviousID)
        {
            DecisionTreeNode result = null;

            if (tree.ID == ParentID)
                return tree;

            else if (tree.children != null)
            {
                for (int i = 0; result == null && i < tree.children.Count; i++)
                {
                    result = SearchTree(tree.children[i], ParentID, PreviousID);
                }
            }

            return result;
        }
    }
}
