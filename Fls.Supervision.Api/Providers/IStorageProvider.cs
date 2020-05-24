namespace Fls.Supervision.Api.Providers
{
    public interface IStorageProvider
    {
        bool EnsureStorageDeleted();
        void MigrateStorage();
    }
}
