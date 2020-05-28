using System;

namespace Fls.Supervision.Api.Domain
{
    public class NewCommitInfo : Info
    {
        public DateTime CommitDate { get; set; }

        public String Creator { get; }

        
    }
}