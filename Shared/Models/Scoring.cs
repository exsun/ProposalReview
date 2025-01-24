namespace Shared.Models
{
    public class Scoring
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public Proposal Proposal { get; set; }
        public int Score { get; set; }
        public string Comments { get; set; }
        public bool IsFinalized { get; set; }
    }
}
