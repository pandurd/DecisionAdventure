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
    public class TestUserJourneyController
    {
        [Fact]
        public async void TestStartJourney()
        {
            // Arranges
            var mockService = new Mock<IUserJourneyService>();
            mockService.Setup(service => service.StartJourney(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new { newJourneyID = Guid.NewGuid(),
                    firstPath = new AdventurePath() { Question = "Do you want a doughnut" } })
                .Verifiable();
            var mockLogger = new Mock<ILogger<UserJourneyController>>();
            var userJourneyController = new UserJourneyController(mockService.Object, mockLogger.Object);

            // Act
            var result = await userJourneyController.StartJourney(new StartRequest() { ID= Guid.NewGuid(), Name = "user" });

            // Assert
            mockService.Verify();
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var created = (dynamic)model.Value;

            var nameOfProperty = "newJourneyID";
            var propertyInfo = created.GetType().GetProperty(nameOfProperty);
            var value = propertyInfo.GetValue(created, null);
            Assert.NotEqual(Guid.Empty, value);

            var nameOffirstPathProperty = "firstPath";
            var firstPathpropertyInfo = created.GetType().GetProperty(nameOffirstPathProperty);
            var firstPathvalue = firstPathpropertyInfo.GetValue(created, null);
            Assert.Equal("Do you want a doughnut", firstPathvalue.Question);
        }

        [Fact]
        public async void TestSaveSelectedStep()
        {
            // Arranges
            var id = Guid.NewGuid();    
            var mockService = new Mock<IUserJourneyService>();
            mockService.Setup(service => service.SelectNextStep(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new AdventurePath() {
                    ID = id
                })
                .Verifiable();
            var mockLogger = new Mock<ILogger<UserJourneyController>>();
            var userJourneyController = new UserJourneyController(mockService.Object, mockLogger.Object);

            // Act
            var result = await userJourneyController.SaveSelectedStep(new SaveStepRequest()
              { userJourneyID= Guid.NewGuid(), adventureID = Guid.NewGuid(), currentPathID = Guid.NewGuid(), selectedOptionID = Guid.NewGuid() });

            // Assert
            mockService.Verify();
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var created = (AdventurePath)model.Value;
            Assert.Equal(id, created.ID);
        }

        [Fact]
        public async void TestGetAllJourneys()
        {
            // Arranges
            var id = Guid.NewGuid();
            var mockService = new Mock<IUserJourneyService>();
            mockService.Setup(service => service.GetAllJourneys())
                .ReturnsAsync(new List<UserAdventure>())
                .Verifiable();
            var mockLogger = new Mock<ILogger<UserJourneyController>>();
            var userJourneyController = new UserJourneyController(mockService.Object, mockLogger.Object);

            // Act
            var result = await userJourneyController.GetAllJourneys();

            // Assert
            mockService.Verify();
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var userAdventures = (List<UserAdventure>)model.Value;
            Assert.Empty(userAdventures);
        }

        [Fact]
        public async void TestGetDecisionTree()
        {
            // Arranges
            var id = Guid.NewGuid();
            var mockService = new Mock<IUserJourneyService>();
            mockService.Setup(service => service.GetDecisionTree(It.IsAny<Guid>()))
                .ReturnsAsync(new DecisionTreeNode()
                {
                    ID = id,
                    children = new List<DecisionTreeNode>()
                    {
                        new DecisionTreeNode() { ID = Guid.NewGuid()}
                    }
                })
                .Verifiable();
            var mockLogger = new Mock<ILogger<UserJourneyController>>();
            var userJourneyController = new UserJourneyController(mockService.Object, mockLogger.Object);

            // Act
            var result = await userJourneyController.GetDecisionTree(Guid.NewGuid());

            // Assert
            mockService.Verify();
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            var decisionTreeNode = (DecisionTreeNode)model.Value;
            Assert.Equal(id, decisionTreeNode.ID);
            Assert.Single(decisionTreeNode.children);
        }
    }
}
