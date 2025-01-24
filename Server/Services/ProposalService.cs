using Server.Data;
using Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ProposalService
    {
        private readonly ApplicationDbContext _context;

        public ProposalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Proposal> CreateProposalAsync(Proposal proposal)
        {
            proposal.SubmissionDate = DateTime.UtcNow;
            proposal.Status = ProposalStatus.Pending;
            _context.Proposals.Add(proposal);
            await _context.SaveChangesAsync();
            return proposal;
        }

        public async Task<Proposal> UpdateProposalStatusAsync(int proposalId, ProposalStatus status)
        {
            var proposal = await _context.Proposals.FindAsync(proposalId);
            if (proposal == null)
            {
                throw new KeyNotFoundException("Proposal not found.");
            }
            proposal.Status = status;
            await _context.SaveChangesAsync();
            return proposal;
        }

        public async Task<IEnumerable<Proposal>> GetAllProposalsAsync()
        {
            return await _context.Proposals.ToListAsync();
        }

        public async Task<Proposal> GetProposalByIdAsync(int id)
        {
            var proposal = await _context.Proposals
                .Include(p => p.Reviewer)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (proposal == null)
            {
                throw new KeyNotFoundException("Proposal not found.");
            }
            return proposal;
        }
    }
}
