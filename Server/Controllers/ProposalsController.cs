using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProposalsController : ControllerBase
    {
        private readonly ProposalService _proposalService;

        public ProposalsController(ProposalService proposalService)
        {
            _proposalService = proposalService;
        }

        [HttpPost("submit")]
        public async Task<ActionResult<Proposal>> SubmitProposal([FromBody] Proposal proposal)
        {
            try
            {
                var createdProposal = await _proposalService.CreateProposalAsync(proposal);
                return CreatedAtAction(nameof(GetProposalById), new { id = createdProposal.Id }, createdProposal);
            }
            catch
            {
                return BadRequest("Error submitting proposal.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proposal>> GetProposalById(int id)
        {
            try
            {
                var proposal = await _proposalService.GetProposalByIdAsync(id);
                if (proposal == null)
                    return NotFound("Proposal not found.");

                return Ok(proposal);
            }
            catch
            {
                return NotFound("Proposal not found.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proposal>>> GetAllProposals()
        {
            var proposals = await _proposalService.GetAllProposalsAsync();
            return Ok(proposals);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProposalStatus(int id, [FromBody] ProposalStatus status)
        {
            try
            {
                var updatedProposal = await _proposalService.UpdateProposalStatusAsync(id, status);
                return Ok(updatedProposal);
            }
            catch
            {
                return NotFound("Proposal not found.");
            }
        }
    }
}
