using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Fls.Supervision.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fls.Supervision.Api.Data.Repositories;
using MongoDB.Driver;
using Convey.Persistence.MongoDB;

namespace Fls.Supervision.Api.Commands.Handlers
{


    public class GithubQueryHandler : IQueryHandler<GithubQuery, List<PullRequestRecordData>>
    {
        public GithubQueryHandler(IMongoRepository<PullRequestRecordData, Guid> githubRecordsRepository)
        {
            repository = githubRecordsRepository;
        }

        //private readonly GithubMetricsService service = new GithubMetricsService();
        private readonly IMongoRepository<PullRequestRecordData, Guid> repository;

        public async Task<List<PullRequestRecordData>> HandleAsync(GithubQuery query)
        {
            //return new List<PullRequestRecordData>() { new PullRequestRecordData() { LastCommitDate = new DateTime(100000) } };
            if (query.Id.HasValue)
            {
                if (await repository.ExistsAsync(e => e.Id == query.Id))
                {
                    var t = await repository.GetAsync(query.Id.Value);
                    return new List<PullRequestRecordData>() { t };
                }
                else return null;
            }
            else
            {
                Func<PullRequestRecordData, bool> t = e =>
                {
                    if (query.LastCommitBefore.HasValue)
                    {
                        if (query.LastCommitBefore < e.LastCommitDate) return false;
                    }
                    if (query.LastCommitAfter.HasValue)
                    {
                        if (query.LastCommitAfter > e.LastCommitDate) return false;
                    }
                    if (query.LastReviewCommentBefore.HasValue)
                    {
                        if (query.LastReviewCommentBefore < e.LastReviewCommentDate) return false;
                    }
                    if (query.LastReviewCommentAfter.HasValue)
                    {
                        if (query.LastReviewCommentAfter > e.LastReviewCommentDate) return false;
                    }
                    return true;
                };
                if (query.elementsPerPageNumber.HasValue)
                {
                    query.pageNumber ??= 50;
                    return ((List<PullRequestRecordData>)await repository.FindAsync(e => t(e))).GetRange((query.pageNumber.Value - 1) * query.elementsPerPageNumber.Value, query.elementsPerPageNumber.Value);
                }
                else return (List<PullRequestRecordData>)await repository.FindAsync(e => t(e));
            }
        }
    }
}
