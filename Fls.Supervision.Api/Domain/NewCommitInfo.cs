using System;

namespace Fls.Supervision.Api.Domain
{
    public class NewCommitInfo : IInfo
    {
        public string Creator { get; set; }
        
        public DateTime CommitDate { get; set; }
    }
}