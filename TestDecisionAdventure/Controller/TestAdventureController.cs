using DecisionAdventure.Services;
using DecisionAdventure.Repos;
using DecisionAdventure.Models;
using DecisionAdventure.Controllers;


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace TestDecisionAdventure.Controller
{
    public class TestAdventureController
    {
        [Fact]
        public async void TestCreateAdventure()
        {
            // Arrange
            var mockService = new Mock<IAdventureService>();
            mockService.Setup(service => service.CreateAdventure(It.IsAny<string>())).ReturnsAsync(new Adventure() { ID = Guid.NewGuid(), Name = "Doughnut" }).Verifiable();
            var mockLogger = new Mock<ILogger<AdventureController>>();
            var adventureController = new AdventureController(mockService.Object, mockLogger.Object);

            // Act
            var result = await adventureController.CreateAdventure(new Adventure() { Name = "Doughnut" });

            // Assert
            mockService.Verify();
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var createdAdventure = (Adventure)model.Value;

            Assert.Equal("Doughnut", createdAdventure.Name);
            Assert.NotEqual(Guid.Empty, createdAdventure.ID);
        }

        [Fact]
        public async void TestAddAdventurePath_BadRequest()
        {
            // Arrange
            var req = new AdventurePath();
            var mockService = new Mock<IAdventureService>();
            mockService.Setup(service => service.AddAdventurePath(It.IsAny<AdventurePath>())).Verifiable();
            var mockLogger = new Mock<ILogger<AdventureController>>();
            var adventureController = new AdventureController(mockService.Object, mockLogger.Object);

            // Act
            mockService.VerifyNoOtherCalls();
            var result = await adventureController.AddAdventurePath(req);
            var model = Assert.IsAssignableFrom<ObjectResult>(result);

            // Assert
            Assert.Equal(400, model.StatusCode);
        }

        [Fact]
        public async void TestAddAdventurePath_OkResponse()
        {
            // Arrange
            var req = new AdventurePath() { ID = Guid.NewGuid(), Question = "Need?" };
            var mockService = new Mock<IAdventureService>();
            mockService.Setup(service => service.AddAdventurePath(It.IsAny<AdventurePath>())).Verifiable();
            var mockLogger = new Mock<ILogger<AdventureController>>();
            var adventureController = new AdventureController(mockService.Object, mockLogger.Object);

            // Act
            var result = await adventureController.AddAdventurePath(req);

            // Assert
            mockService.Verify();
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public async void TestGetAdventures()
        {
            // Arrange
            var res = new List<Adventure>() { new Adventure { ID=Guid.NewGuid(), Name = "Dough" } };
            
            var mockService = new Mock<IAdventureService>();
            mockService.Setup(service => service.GetAdventures()).ReturnsAsync(res).Verifiable();
            
            var mockLogger = new Mock<ILogger<AdventureController>>();
            var adventureController = new AdventureController(mockService.Object, mockLogger.Object);

            // Act
            var result = await adventureController.GetAdventures();

            // Assert
            mockService.Verify();
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var resultAdventures = (List<Adventure>)okResult.Value;
            Assert.Single(resultAdventures);
            Assert.Equal("Dough", resultAdventures[0].Name);
        }
    }
}
