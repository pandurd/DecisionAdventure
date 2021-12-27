using DecisionAdventure.Services;
using DecisionAdventure.Repos;
using DecisionAdventure.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace TestDecisionAdventure.Services
{
    public class TestUserJourneyService
    {
        [Fact]
        public async void TestStartJourney()
        {
            // Arrange
            var newJourneyID = Guid.NewGuid();
            var firstPath = new AdventurePath() { Question = "Do you want a doughnut"};
            var res =  new { newJourneyID, firstPath };

            var mockRepo = new Mock<IAdventureRepo>();
            var mockJourneyRepo = new Mock<IUserJourneyRepo>();
            mockJourneyRepo.Setup(repo => repo.AddNewUserJourney(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Guid>())).Verifiable();
            mockRepo.Setup(repo => repo.GetNextAdventurePath(It.IsAny<Guid>(), null, null)).Returns(Task.FromResult(firstPath)).Verifiable();
            var service = new UserJourneyService(mockRepo.Object, mockJourneyRepo.Object);
            var inputName = "doughnut";

            // Act
            var result = await service.StartJourney(newJourneyID, inputName);

            // Assert
            mockJourneyRepo.Verify();
            mockRepo.Verify();

            var nameOfProperty = "newJourneyID";
            var propertyInfo = result.GetType().GetProperty(nameOfProperty);
            var value = propertyInfo.GetValue(result, null);
            Assert.NotEqual(Guid.Empty, value);

            var nameOffirstPathProperty = "firstPath";
            var firstPathpropertyInfo = result.GetType().GetProperty(nameOffirstPathProperty);
            var firstPathvalue = firstPathpropertyInfo.GetValue(result, null);
            Assert.Equal("Do you want a doughnut", firstPathvalue.Question);
        }


        [Fact]
        public async void TestSelectNextStep()
        {
            // Arrange
            var advId = Guid.NewGuid();
            var firstPath = new AdventurePath() { Question = "Finish", Options = new List<AdventurePathOption>() };
          
            var mockRepo = new Mock<IAdventureRepo>();
            var mockJourneyRepo = new Mock<IUserJourneyRepo>();
            mockJourneyRepo.Setup(repo => repo.AddAdventureSelection(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Verifiable();
            mockRepo.Setup(repo => repo.GetNextAdventurePath(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult(firstPath)).Verifiable();
            var service = new UserJourneyService(mockRepo.Object, mockJourneyRepo.Object);
    
            // Act
            var result = await service.SelectNextStep(Guid.NewGuid(), advId, Guid.NewGuid(), Guid.NewGuid());

            //Assert
            mockJourneyRepo.Verify();
            mockRepo.Verify();

            Assert.NotNull(result);
            Assert.Equal("Finish", result.Question);
        }

        [Fact]
        public async void TestGetAllJourneys()
        {
            // Arrange
            var mockRepo = new Mock<IAdventureRepo>();
            var mockJourneyRepo = new Mock<IUserJourneyRepo>();
            mockJourneyRepo.Setup(repo => repo.GetAllJourneys()).ReturnsAsync(new List<UserAdventure>());
            var service = new UserJourneyService(mockRepo.Object, mockJourneyRepo.Object);

            // Act
            var result = await service.GetAllJourneys();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async void TestGetDecisionTree()
        {
            //Main logic which displays choosen Decision Tree

            // Arrange
            var newJourneyID = Guid.NewGuid();
            var res = new UserJourney()
            {
                PathOptions = new List<UserJourneyPath>() {
                    new UserJourneyPath {
                        ID = new Guid("9A19C212-A28A-4CA3-BC91-453BA2529419"),
                        Question = "one",
                        Label = "ans2",
                        IsSelected = false,
                        AnswerID = new Guid("201C0C12-DB6A-44A0-BE5B-C7A14BBD09A2"),
                        ParentID = new Guid("9A19C212-A28A-4CA3-BC91-453BA2529419"),
                        PreviousAnswer = Guid.Empty
                    },

                    new UserJourneyPath {
                        ID = new Guid("9A19C212-A28A-4CA3-BC91-453BA2529419"),
                        Question = "one",
                        Label = "ans1",
                        IsSelected = true,
                        AnswerID = new Guid("5A007228-3834-423F-B8DC-F7AB213A7573"),
                        ParentID = new Guid("9A19C212-A28A-4CA3-BC91-453BA2529419"),
                        PreviousAnswer = Guid.Empty
                    },

                    new UserJourneyPath {
                        ID = new Guid("281447F4-A7D6-4603-99D3-30938714BC74"),
                        Question = "Need",
                        Label = "one",
                        IsSelected = true,
                        AnswerID = new Guid("6ECBA513-CCB3-4138-9CF1-6FFFF27EE525"),
                        ParentID = new Guid("281447F4-A7D6-4603-99D3-30938714BC74"),
                        PreviousAnswer = new Guid("5A007228-3834-423F-B8DC-F7AB213A7573"),
                    },

                    new UserJourneyPath {
                        ID = new Guid("281447F4-A7D6-4603-99D3-30938714BC74"),
                        Question = "Need",
                        Label = "two",
                        IsSelected = false,
                        AnswerID = new Guid("5D6B46B3-6176-4D28-B237-CD0B62A8BDD0"),
                        ParentID = new Guid("281447F4-A7D6-4603-99D3-30938714BC74"),
                        PreviousAnswer = new Guid("5A007228-3834-423F-B8DC-F7AB213A7573"),
                    },

                    new UserJourneyPath {
                        ID = new Guid("0D5F4443-07C7-4BFA-9D13-80432A03CA61"),
                        Question = "Approved",
                        Label = null,
                        IsSelected = false,
                        AnswerID = Guid.Empty,
                        ParentID = new Guid("0D5F4443-07C7-4BFA-9D13-80432A03CA61"),
                        PreviousAnswer = new Guid("6ECBA513-CCB3-4138-9CF1-6FFFF27EE525")
                    }
                }
            };

            var mockRepo = new Mock<IAdventureRepo>();
            var mockJourneyRepo = new Mock<IUserJourneyRepo>();
            mockJourneyRepo.Setup(repo => repo.GetDecisionTree(It.IsAny<Guid>())).ReturnsAsync(res).Verifiable();
            var service = new UserJourneyService(mockRepo.Object, mockJourneyRepo.Object);

            // Act
            var result = await service.GetDecisionTree(newJourneyID);

            //Assert
            mockJourneyRepo.Verify(repo => repo.GetDecisionTree(newJourneyID));

            Assert.Equal(2, result.children.Count);
            Assert.Empty(result.children[0].children);

            Assert.Single(result.children[1].children);
            Assert.True(result.children[1].children[0].IsQuestion);

            //highlight selected
            Assert.False(result.children[0].IsSelected);
            Assert.True(result.children[1].IsSelected);

            Assert.True(result.children[1].children[0].children[0].IsSelected);
            Assert.False(result.children[1].children[0].children[1].IsSelected);

            //alernate question and answers
            Assert.False(result.children[0].IsQuestion);
            Assert.True(result.children[1].children[0].IsQuestion);
            Assert.False(result.children[1].children[0].children[0].IsQuestion);
            Assert.True(result.children[1].children[0].children[0].children[0].IsQuestion);

            //Last node
            Assert.Equal("Approved", result.children[1].children[0].children[0].children[0].Label);
        }

    }
}
