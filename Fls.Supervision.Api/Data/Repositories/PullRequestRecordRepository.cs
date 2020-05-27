using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;

namespace Fls.Supervision.Api.Data.Repositories
{
    public class PullRequestRecordRepository : IMongoRepository<PullRequestRecordData, Guid>
    {
        private const string CollectionName = "PullRequestRecords";

        private const int resultsPerPage = 10;

        public IMongoCollection<PullRequestRecordData> Collection { get; }

        public PullRequestRecordRepository(MongoDbOptions mongoOptions, IMongoClient mongoClient)
        {
            Collection = mongoClient.GetDatabase(mongoOptions.Database).GetCollection<PullRequestRecordData>(CollectionName);
        }

        public async Task AddAsync(PullRequestRecordData data)
        {
            await Collection.InsertOneAsync(data);
        }

        public Task<PagedResult<PullRequestRecordData>> BrowseAsync<TQuery>(Expression<Func<PullRequestRecordData, bool>> predicate, TQuery query) where TQuery : IPagedQuery
        {
            var items = FindAsync(predicate).Result;
            var count = items.Count;
            return Task.FromResult(PagedResult<PullRequestRecordData>.Create(items, query.Page, resultsPerPage,
                decimal.ToInt32(decimal.Ceiling(decimal.Divide(Convert.ToDecimal(count), Convert.ToDecimal(resultsPerPage)))), count));
        }

        public async Task DeleteAsync(Guid id)
        {
            await Collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Expression<Func<PullRequestRecordData, bool>> predicate)
        {
            await Collection.DeleteOneAsync(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<PullRequestRecordData, bool>> predicate)
        {
            return await (await Collection.FindAsync(predicate)).AnyAsync();
        }

        public async Task<IReadOnlyList<PullRequestRecordData>> FindAsync(Expression<Func<PullRequestRecordData, bool>> predicate)
        {
            return await (await Collection.FindAsync(predicate)).ToListAsync();
        }

        public async Task<PullRequestRecordData> GetAsync(Guid id)
        {
            return await (await Collection.FindAsync(data => data.Id == id)).FirstOrDefaultAsync();
        }

        public async Task<PullRequestRecordData> GetAsync(Expression<Func<PullRequestRecordData, bool>> predicate)
        {
            return await (await Collection.FindAsync(predicate)).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(PullRequestRecordData data)
        {
            await Collection.UpdateOneAsync(x => x.Id == data.Id, new UpdateDefinitionBuilder<PullRequestRecordData>()
                .Set(x => x.DelayHistory, data.DelayHistory)
                .Set(x => x.GapHistory, data.GapHistory)
                .Set(x => x.LastCommitDate, data.LastCommitDate)
                .Set(x => x.LastReviewCommentDate, data.LastReviewCommentDate)
                .Set(x => x.StateHistory, data.StateHistory));
        }

        public async Task UpdateAsync(PullRequestRecordData data, Expression<Func<PullRequestRecordData, bool>> predicate)
        {
            await Collection.UpdateOneAsync(predicate, new UpdateDefinitionBuilder<PullRequestRecordData>()
                .Set(x => x.DelayHistory, data.DelayHistory)
                .Set(x => x.GapHistory, data.GapHistory)
                .Set(x => x.LastCommitDate, data.LastCommitDate)
                .Set(x => x.LastReviewCommentDate, data.LastReviewCommentDate)
                .Set(x => x.StateHistory, data.StateHistory));
        }
    }
}