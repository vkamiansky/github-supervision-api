namespace Fls.Supervision.Api.Providers.Implementations
{
    public class InMemoryStorageProvider : IStorageProvider
    {
        public bool EnsureStorageDeleted()
        {
            return true;
        }

        public void MigrateStorage()
        {
        }
    }
}
