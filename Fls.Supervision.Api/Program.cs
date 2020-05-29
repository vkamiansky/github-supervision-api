using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.LoadBalancing.Fabio;
using Convey.Logging;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Fls.Supervision.Api.Commands;
using Fls.Supervision.Api.Providers;
using Fls.Supervision.Api.Providers.Implementations;
using System.Text.Json;
using System.Collections.Generic;
using System;
using System.Net;
using System.IO;
using Fls.Supervision.Api.Data;

namespace Fls.Supervision.Api
{
    public static class Extensions
    {
        public static async Task OkAsync<T>(this HttpResponse response, T value)
        {
            var serialized = JsonSerializer.Serialize(value, new JsonSerializerOptions {WriteIndented = true});
            await response.WriteAsync(serialized);
        }

        public static IWebHostBuilder UseSupervisionApi(this IWebHostBuilder webBuilder)
        {
            webBuilder
                .ConfigureServices(services => services
                    .AddScoped<IStorageProvider, MongoStorageProvider>()
                    .AddConvey()
                    
                    .AddMongo()
                    .AddMongoRepository<PullRequestRecordData, long>("PullRequestRecords")
                    //.AddConsul()
                    //.AddFabio()
                    .AddEventHandlers()
                    .AddCommandHandlers()
                    .AddInMemoryCommandDispatcher()
                    //.AddRabbitMq()
                    .AddWebApi()
                    .Build())
                .Configure(app => app
                    .UseConvey()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync("FLS Supervision API"))
                        .Get("ping", ctx => ctx.Response.WriteAsync("pong"))
                        .Get("query/{query_data}", ctx => ctx.Response.WriteAsync("здесь отвечаем на запросы"))
                        .Post<ProcessGithubEvent>("webhook", afterDispatch: (cmd, ctx) => ctx.Response.OkAsync(new {Hook = cmd.Hook, Message = "Event accepted."}))))
                //.UseRabbitMq())
                .UseLogging();
            return webBuilder;
        }
    }

    public static class Program
    {
        public static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();

        private static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder.UseSupervisionApi());
    }
}