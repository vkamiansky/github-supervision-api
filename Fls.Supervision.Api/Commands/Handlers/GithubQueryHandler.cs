using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Fls.Supervision.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fls.Supervision.Api.Data.Repositories;
using MongoDB.Driver;

namespace Fls.Supervision.Api.Commands.Handlers
{

    public class GithubQueryHandler : IQueryHandler<GithubQuery, List<PullRequestRecordData>>
    {
        private readonly static PullRequestRecordRepository repository = new PullRequestRecordRepository(null, null);   //[TODO] разобраться с репозиторием

        public async Task<List<PullRequestRecordData>> HandleAsync(GithubQuery query)
        {
            if (query.queryData != null)
            {
                if (query.queryData.Id.HasValue)
                {
                    if (await repository.ExistsAsync(e => e.Id == query.queryData.Id))
                    {
                        var t = await repository.GetAsync(query.queryData.Id.Value);
                        return new List<PullRequestRecordData>() { t };
                    }
                    else return null;
                }
                else
                {
                    Func<PullRequestRecordData, bool> t = e =>
                    {
                        if (query.queryData.DelayHistory != null)
                        {
                            if (query.queryData.DelayHistory != e.DelayHistory) return false;
                        }
                        if (query.queryData.GapHistory != null)
                        {
                            if (query.queryData.GapHistory != e.GapHistory) return false;
                        }
                        if (query.queryData.StateHistory != null)
                        {
                            if (query.queryData.StateHistory != e.StateHistory) return false;
                        }
                        if (query.queryData.LastCommitDate.HasValue)
                        {
                            if (query.queryData.LastCommitDate != e.LastCommitDate) return false;
                        }
                        if (query.queryData.LastReviewCommentDate.HasValue)
                        {
                            if (query.queryData.LastReviewCommentDate != e.LastReviewCommentDate) return false;
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
            return null;
        }
    }
}
