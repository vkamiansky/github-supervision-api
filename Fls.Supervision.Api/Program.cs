
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Events;
using Convey.Discovery.Consul;
using Convey.LoadBalancing.Fabio;
using Convey.Logging;
using Convey.MessageBrokers.RabbitMQ;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Convey.CQRS.Commands;
using Fls.Supervision.Api.Commands;

namespace Fls.Supervision.Api
{
    public class Program
    {
        public static Task Main(string[] args)
            => CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services => services
                        .AddConvey()
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
                            .Post<ProcessGithubEvent>("webhook", afterDispatch: (cmd, ctx) => ctx.Response.Ok())))
                        //.UseRabbitMq())
                    .UseLogging();
            });
    }
}
