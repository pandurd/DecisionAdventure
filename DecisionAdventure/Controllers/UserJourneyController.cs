using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using DecisionAdventure.Models;
using DecisionAdventure.Services;


namespace DecisionAdventure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserJourneyController : ControllerBase
    {
        private readonly IUserJourneyService _userJourneyService;
        private readonly ILogger<UserJourneyController> _logger;
        public UserJourneyController(IUserJourneyService userJourneyService, ILogger<UserJourneyController> logger)
        {
            _userJourneyService = userJourneyService;
            _logger = logger;
        }

        [Route("start")]
        [HttpPost]
        public async Task<IActionResult> StartJourney(StartRequest startRequest)
        {
            if (startRequest.ID == Guid.Empty || string.IsNullOrEmpty(startRequest.Name))
            {
                _logger.LogError("Request does not have all properties");
                return BadRequest("Request does not have all properties");
            }             

            try
            {
                var result = await _userJourneyService.StartJourney(startRequest.ID, startRequest.Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Route("selectstep")]
        [HttpPost]
        public async Task<IActionResult> SaveSelectedStep(SaveStepRequest saveStepRequest)
        {
            if (saveStepRequest.selectedOptionID == Guid.Empty 
                || saveStepRequest.currentPathID == Guid.Empty
                || saveStepRequest.adventureID == Guid.Empty
                || saveStepRequest.userJourneyID == Guid.Empty)
                return BadRequest("Request does not have all properties");

            try
            {
                var nextStep = await _userJourneyService.SelectNextStep(saveStepRequest.userJourneyID, saveStepRequest.adventureID, saveStepRequest.currentPathID, saveStepRequest.selectedOptionID);
                return Ok(nextStep);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("journeys")]
        [HttpGet]
        public async Task<IActionResult> GetAllJourneys()
        {
            try
            {
                var journeys = await _userJourneyService.GetAllJourneys();
                return Ok(journeys);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }    
        }

        [Route("decisiontree")]
        [HttpGet]
        public async Task<IActionResult> GetDecisionTree(Guid userJourneyID)
        {
            if (userJourneyID == Guid.Empty)
            {
                _logger.LogError("userJourneyID is empty");
                return BadRequest("userJourneyID is empty");
            }

            try
            {
                var tree = await _userJourneyService.GetDecisionTree(userJourneyID);
                return Ok(tree);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
