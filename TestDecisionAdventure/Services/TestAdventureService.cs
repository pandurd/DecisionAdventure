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
    public class TestAdventureService
    {
        [Fact]
        public async void TestCreateAdventure()
        {
            // Arrange
            var mockRepo = new Mock<IAdventureRepo>();
            mockRepo.Setup(repo => repo.CreateAdventure(It.IsAny<Adventure>())).Verifiable();
            var service = new AdventureService(mockRepo.Object);

            // Act
            var result = await service.CreateAdventure("name");

            // Assert
            mockRepo.Verify();
            var model = Assert.IsAssignableFrom<Adventure>(result);
            Assert.Equal("name", model.Name);
            Assert.NotEqual(Guid.Empty, model.ID);
        }

        [Fact]
        public async void TestCreateAdventurePath()
        {
            // Arrange
            var mockRepo = new Mock<IAdventureRepo>();
            mockRepo.Setup(repo => repo.CreateAdventurePath(It.IsAny<AdventurePath>())).Verifiable(); ;
            var service = new AdventureService(mockRepo.Object);

            // Act
            await service.AddAdventurePath(new AdventurePath (){  });

            // Assert
            mockRepo.Verify();
        }

        [Fact]
        public async void TestGetAdventures()
        {
            // Arrange
            var res = new List<Adventure>();
            res.Add(new Adventure { ID = Guid.NewGuid(), Name = "cake" });
            res.Add(new Adventure { ID = Guid.NewGuid(), Name = "doughnut" });

            var mockRepo = new Mock<IAdventureRepo>();
            mockRepo.Setup(repo => repo.GetAdventures()).Returns(Task.FromResult(res));
           
            var service = new AdventureService(mockRepo.Object);

            // Act
            var result = await service.GetAdventures();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("cake", result[0].Name);
        }
    }
}
