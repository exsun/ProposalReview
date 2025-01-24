namespace Shared.Models
{
    public class Bonus
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public Proposal Proposal { get; set; }
        public decimal Amount { get; set; }
        public bool IsApproved { get; set; }
    }
}
