using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Fls.Supervision.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fls.Supervision.Api.Commands
{
    public class GithubQuery :  IQuery<List<PullRequestRecordData>>
    {
        public QueryData queryData;
        public int? pageNumber;
        public int? elementsPerPageNumber;
        public class QueryData
        {
            public long? Id { get; set; }
            public TimeSpan[] DelayHistory { get; set; }
            public TimeSpan[] GapHistory { get; set; }
            public ValueTuple<DateTime, PullRequestState>[] StateHistory { get; set; }
            public DateTime? LastCommitDate { get; set; }
            public DateTime? LastReviewCommentDate { get; set; }
        }
    }
}
