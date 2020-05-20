using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Events;

namespace Fls.Supervision.Api.Commands.Handlers
{
    public class ProcessGithubPingHandler : IEventHandler<ProcessGithubEvent<PingPayload>>
    {
        public async Task HandleAsync(ProcessGithubEvent<PingPayload> processGithubEvent)
        {
            //[ToDo] Metrics calculation

            //[ToDo] data create

            //[ToDo] event: MetricAdded, EventProcessed
        }
    }
}