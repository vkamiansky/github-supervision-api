
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.LoadBalancing.Fabio;
using Convey.Logging;
using Convey.MessageBrokers.RabbitMQ;
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
using Fls.Supervision.Api.Data;
using Microsoft.AspNetCore.Builder;
using System.IO;

namespace Fls.Supervision.Api
{
    public static class Extensions
    {
        public static async Task OkAsync<T>(this HttpResponse response, T value)
        {
            var serialized = JsonSerializer.Serialize<T>(value, new JsonSerializerOptions { WriteIndented = true });
            await response.WriteAsync(serialized);
        }

        public static IWebHostBuilder UseSupervisionApi(this IWebHostBuilder webBuilder)
        {
            webBuilder
                    .ConfigureServices(services => services
                    .Configure<IISServerOptions>(options =>
                    {
                        options.AllowSynchronousIO = true;
                    })
                        .AddScoped<IStorageProvider, MongoStorageProvider>()
                        .AddConvey()
                        .AddMongo()
                        //.AddConsul()
                        //.AddFabio()
                        .AddEventHandlers()
                        .AddCommandHandlers()
                        .AddQueryHandlers()
                        .AddInMemoryCommandDispatcher()
                        .AddInMemoryQueryDispatcher()
                        //.AddRabbitMq()
                        .AddWebApi()
                        .Build())
                    .Configure(app => app
                        .UseConvey()
                        .UseDispatcherEndpoints(endpoints => endpoints
                            .Get("", ctx => ctx.Response.WriteAsync("FLS Supervision API"))
                            .Get("ping", ctx => ctx.Response.WriteAsync("pong"))
                            .Get<GithubQuery, List<PullRequestRecordData>>("query/{LastCommitAfter}/{LastCommitBefore}/{LastReviewCommentAfter}/{LastReviewCommentBefore}"/*,
                            (query, cmx)=>{
                                int.TryParse((string)cmx.Items["pageNumber"], out int t1);
                                query.pageNumber = t1;
                                int.TryParse((string)cmx.Items["elementsPerPageNumber"], out int t2);
                                query.elementsPerPageNumber = t2;
                                return null;
                            }*/,
                            afterDispatch: async (query, result, context) =>{
                                var buf = new MemoryStream();
                                await JsonSerializer.SerializeAsync(buf, result);
                                buf.Position = 0;
                                var r = new StreamReader(buf);
                                await context.Response.OkAsync( await r.ReadToEndAsync() );
                                r.Dispose();
                               })
                            .Post<ProcessGithubEvent>("webhook", afterDispatch: (cmd, ctx) => ctx.Response.OkAsync(new { Hook = cmd.Hook, Message = "Event accepted." }))))
                    //.UseRabbitMq())
                    .UseLogging();
            return webBuilder;
        }
    }
    public class Program
    {
        public static Task Main(string[] args)
            => CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder.UseSupervisionApi());
    }
}
