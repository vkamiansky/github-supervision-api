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
        private static readonly PullRequestRecordRepository repository = new PullRequestRecordRepository(null, null);   //[TODO] разобраться с репозиторием

        public async Task<List<PullRequestRecordData>> HandleAsync(GithubQuery query)
        {
            if (query.queryData != null)
            {
                if (query.queryData.Id.HasValue)
                {
                    if (await repository.ExistsAsync(e => e.Id == query.queryData.Id))
                    {
                        return new List<PullRequestRecordData>() { await repository.GetAsync(query.queryData.Id.Value) };
                    }
                    return null;
                }

                Func<PullRequestRecordData, bool> t = e =>
                {
                    if (query.queryData.DelayHistory?.SequenceEqual(e.DelayHistory) ?? false) return false;
                    if (query.queryData.GapHistory?.SequenceEqual(e.GapHistory) ?? false) return false;
                    if (query.queryData.StateHistory?.SequenceEqual(e.StateHistory) ?? false) return false;
                    if (query.queryData.LastCommitDate != e.LastCommitDate) return false;
                    return query.queryData.LastReviewCommentDate == e.LastReviewCommentDate;
                };
                if (query.elementsPerPageNumber.HasValue)
                {
                    query.pageNumber ??= 50;
                    return ((List<PullRequestRecordData>) await repository.FindAsync(e => t(e))).GetRange(
                        (query.pageNumber.Value - 1) * query.elementsPerPageNumber.Value,
                        query.elementsPerPageNumber.Value);
                }
                return (List<PullRequestRecordData>) await repository.FindAsync(e => t(e));
            }
            return null;
        }
    }
}
