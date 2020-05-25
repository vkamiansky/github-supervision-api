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
        private readonly IMongoCollection<PullRequestRecordData> _collection;

        public IMongoCollection<PullRequestRecordData> Collection => _collection;

        public PullRequestRecordRepository(MongoDbOptions mongoOptions, IMongoClient mongoClient)
        {
            _collection = mongoClient.GetDatabase(mongoOptions.Database).GetCollection<PullRequestRecordData>(CollectionName);
        }

        public async Task AddAsync(PullRequestRecordData entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public Task<PagedResult<PullRequestRecordData>> BrowseAsync<TQuery>(Expression<Func<PullRequestRecordData, bool>> predicate, TQuery query) where TQuery : IPagedQuery
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Expression<Func<PullRequestRecordData, bool>> predicate)
        {
            await _collection.DeleteManyAsync(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<PullRequestRecordData, bool>> predicate)
        {
            return await (await _collection.FindAsync(predicate)).AnyAsync();
        }

        public async Task<IReadOnlyList<PullRequestRecordData>> FindAsync(Expression<Func<PullRequestRecordData, bool>> predicate)
        {
            var t = await _collection.FindAsync(predicate);
            return t.ToList();
        }

        public async Task<PullRequestRecordData> GetAsync(Guid id)
        {
            var t = await _collection.FindAsync(e => e.Id == id);
            return t.FirstOrDefault();
        }

        public async Task<PullRequestRecordData> GetAsync(Expression<Func<PullRequestRecordData, bool>> predicate)
        {
            var t = await _collection.FindAsync(predicate);
            return t.First();
        }

        public async Task UpdateAsync(PullRequestRecordData entity)
        {
            var builder = new UpdateDefinitionBuilder<PullRequestRecordData>();
            var update = builder
            .Set(x => x.DelayHistory, entity.DelayHistory)
            .Set(x => x.GapHistory, entity.GapHistory)
            .Set(x => x.LastCommitDate, entity.LastCommitDate)
            .Set(x => x.LastReviewCommentDate, entity.LastReviewCommentDate)
            .Set(x => x.StateHistory, entity.StateHistory);
            var result = await _collection.UpdateOneAsync(x => x.Id == entity.Id, update);
        }

        public async Task UpdateAsync(PullRequestRecordData entity, Expression<Func<PullRequestRecordData, bool>> predicate)
        {
            var builder = new UpdateDefinitionBuilder<PullRequestRecordData>();
            var update = builder
            .Set(x => x.DelayHistory, entity.DelayHistory)
            .Set(x => x.GapHistory, entity.GapHistory)
            .Set(x => x.LastCommitDate, entity.LastCommitDate)
            .Set(x => x.LastReviewCommentDate, entity.LastReviewCommentDate)
            .Set(x => x.StateHistory, entity.StateHistory);
            await _collection.UpdateOneAsync(predicate, update);
        }
    }
}