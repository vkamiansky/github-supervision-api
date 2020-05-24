using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fls.Supervision.Api.Providers;

namespace Fls.Supervision.Test.Integration.Helpers
{
    public static class TestHelper
    {
        // The file is configured to be copied to the output path if newer
        private const string ConfigPath = ".\\appsettings.Test.json";

        public static TestServer CreateTestServer(Func<IWebHostBuilder, IWebHostBuilder> useHost)
        {
            return new TestServer(useHost(new WebHostBuilder())
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile(ConfigPath);
                }));
        }

        public static HttpClient CreateTestClient(this TestServer server)
        {
            return server.CreateClient();
        }

        public static void EnsureBlankTestDb(this TestServer server)
        {
            using (var serviceScope = server.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var storageProvider = serviceScope.ServiceProvider.GetRequiredService<IStorageProvider>();
                storageProvider.EnsureStorageDeleted();
                storageProvider.MigrateStorage();
            }
        }
    }
}
