using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fls.Supervision.Api.Providers.Implementations
{
    public class MongoStorageProvider : IStorageProvider
    {
        private readonly MongoDbOptions _mongoOptions;

        public MongoStorageProvider(MongoDbOptions mongoOptions)
        {
            _mongoOptions = mongoOptions;
        }
        
        public bool EnsureStorageDeleted()
        {
            new MongoClient(_mongoOptions.ConnectionString).DropDatabase(_mongoOptions.Database);
            return true;
        }

        public void MigrateStorage()
        {
        }
    }
}
