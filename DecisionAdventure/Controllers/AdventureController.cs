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
    public class AdventureController : ControllerBase
    {
        private readonly IAdventureService _adventureService;
        private readonly ILogger<AdventureController> _logger;
        public AdventureController(IAdventureService adventureService, ILogger<AdventureController> logger)
        {
            _adventureService = adventureService;
            _logger = logger;
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateAdventure(Adventure adv)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Adventure object is not valid", adv);
                return BadRequest("Name should be empty");
            }

            try
            {
                var adventure = await _adventureService.CreateAdventure(adv.Name);
                return Ok(adventure);
            } 
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("addpath")]
        [HttpPost]
        public async Task<IActionResult> AddAdventurePath(AdventurePath path)
        {
            if (string.IsNullOrEmpty(path.Question) || path.ID == Guid.Empty)
            {
                _logger.LogInformation("Path does not have all required properties", path);
                return BadRequest("Path does not have all required properties");
            }
                
            try
            {
                await _adventureService.AddAdventurePath(path);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("getall")]
        [HttpGet]
        public async Task<IActionResult> GetAdventures()
        {
            try
            {
                var results = await _adventureService.GetAdventures();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
