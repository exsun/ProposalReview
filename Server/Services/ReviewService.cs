using Server.Data;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Scoring> AddReviewAsync(int proposalId, Scoring scoring)
        {
            var proposal = await _context.Proposals.FindAsync(proposalId);
            if (proposal == null)
            {
                throw new KeyNotFoundException("Proposal not found.");
            }
            scoring.ProposalId = proposalId;
            _context.Scorings.Add(scoring);
            await _context.SaveChangesAsync();
            return scoring;
        }

        public async Task<Scoring> FinalizeReviewAsync(int scoringId)
        {
            var scoring = await _context.Scorings.FindAsync(scoringId);
            if (scoring == null)
            {
                throw new KeyNotFoundException("Scoring not found.");
            }
            scoring.IsFinalized = true;
            await _context.SaveChangesAsync();
            return scoring;
        }
    }
}
