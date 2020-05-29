using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Fls.Supervision.Api.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fls.Supervision.Api.Commands
{
    public class GithubQuery : IQuery<List<PullRequestRecordData>>
    {


        public int? pageNumber;
        public int? elementsPerPageNumber;

        public Guid? Id { get; set; }
        public DateTime? LastCommitBefore { get; set; }
        public DateTime? LastCommitAfter { get; set; }
        public DateTime? LastReviewCommentBefore { get; set; }
        public DateTime? LastReviewCommentAfter { get; set; }
    }
}
