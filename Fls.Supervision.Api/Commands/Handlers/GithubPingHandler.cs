using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Events;
using System.Text.Json;


namespace Fls.Supervision.Api.Commands.Handlers
{
    public class GithubPingHandler : IEventHandler<GithubEvent<PingPayload>>
    {
        public async Task HandleAsync(GithubEvent<PingPayload> githubEvent)
        {
            //[ToDo] Metrics calculation

            //[ToDo] data create
            

            //[ToDo] event: MetricAdded, EventProcessed
        }
    }
}