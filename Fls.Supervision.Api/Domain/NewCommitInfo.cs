using System;

namespace Fls.Supervision.Api.Domain
{
    public class NewCommitInfo : IInfo
    {
        public DateTime? CommitDate { get; set; }

        public String Creator { get; set;}

        
    }
}