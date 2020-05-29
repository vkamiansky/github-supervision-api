using System;

namespace Fls.Supervision.Api.Domain
{
    public class NewReviewInfo : IInfo
    {
        public String Creator { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}