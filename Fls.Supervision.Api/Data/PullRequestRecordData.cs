using System;
using System.Collections.Generic;
using Convey.Types;

namespace Fls.Supervision.Api.Data 
{
    public class PullRequestRecordData : IIdentifiable<long> 
    {
        public long Id { get; set; }

        public IEnumerable<TimeSpan> DelayHistory { get; set; }
        public IEnumerable<TimeSpan> GapHistory { get; set; }
        public IEnumerable<ValueTuple<DateTime, PullRequestState>> StateHistory { get; set; }
        public DateTime? LastCommitDate { get; set; }
        public DateTime? LastReviewCommentDate { get; set; }
    }
}