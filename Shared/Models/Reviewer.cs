using System.Collections.Generic;

namespace Shared.Models
{
    public class Reviewer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Proposal> Proposals { get; set; }
    }
}
