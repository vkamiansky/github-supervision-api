using System;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Convey.Types;
using MongoDB.Driver;

namespace Fls.Supervision.Api.Data
{
    public class PullRequestRecordData : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public TimeSpan[] DelayHistory { get; set; }
        public TimeSpan[] GapHistory { get; set; }
        public ValueTuple<DateTime, PullRequestState>[] StateHistory { get; set; }
        public DateTime? LastCommitDate { get; set; }
        public DateTime? LastReviewCommentDate { get; set; }
    }
}