using System;

namespace Shared.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ProposalText { get; set; }
        public DateTime SubmissionDate { get; set; }
        public ProposalStatus Status { get; set; }
        public int? ReviewerId { get; set; }  // Make ReviewerId nullable
        public virtual Reviewer? Reviewer { get; set; }
    }

    public enum ProposalStatus
    {
        Pending,
        UnderReview,
        Approved,
        Rejected
    }
}
