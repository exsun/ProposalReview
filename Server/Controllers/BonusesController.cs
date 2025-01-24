using Microsoft.AspNetCore.Mvc;
using Server.Services;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BonusesController : ControllerBase
    {
        private readonly BonusService _bonusService;

        public BonusesController(BonusService bonusService)
        {
            _bonusService = bonusService;
        }

        [HttpPost("calculate/{proposalId}")]
        public async Task<IActionResult> CalculateBonus(int proposalId, [FromBody] decimal amount)
        {
            try
            {
                var bonus = await _bonusService.CalculateAndAssignBonusAsync(proposalId, amount);
                return CreatedAtAction("CalculateBonus", new { id = bonus.Id }, bonus);
            }
            catch
            {
                return BadRequest("Error calculating bonus.");
            }
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveBonus(int id)
        {
            try
            {
                var bonus = await _bonusService.ApproveBonusAsync(id);
                return Ok(bonus);
            }
            catch
            {
                return NotFound("Bonus not found.");
            }
        }
    }
}
