using Server.Data;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Services
{
    public class BonusService
    {
        private readonly ApplicationDbContext _context;

        public BonusService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bonus> CalculateAndAssignBonusAsync(int proposalId, decimal amount)
        {
            var proposal = await _context.Proposals.FindAsync(proposalId);
            if (proposal == null || proposal.Status != ProposalStatus.Approved)
            {
                throw new InvalidOperationException("Proposal must be approved to assign bonus.");
            }

            var bonus = new Bonus
            {
                ProposalId = proposalId,
                Amount = amount,
                IsApproved = false
            };

            _context.Bonuses.Add(bonus);
            await _context.SaveChangesAsync();
            return bonus;
        }

        public async Task<Bonus> ApproveBonusAsync(int bonusId)
        {
            var bonus = await _context.Bonuses.FindAsync(bonusId);
            if (bonus == null)
            {
                throw new KeyNotFoundException("Bonus not found.");
            }
            bonus.IsApproved = true;
            await _context.SaveChangesAsync();
            return bonus;
        }
    }
}
