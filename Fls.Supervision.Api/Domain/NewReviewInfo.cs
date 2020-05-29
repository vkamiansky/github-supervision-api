using System;

namespace Fls.Supervision.Api.Domain
{
    public class NewReviewInfo : IInfo
    {
        public string Creator { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}