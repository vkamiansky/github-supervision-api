using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;

namespace Fls.Supervision.Api.Commands.Handlers
{

    public class ProcessGithubEventHandler : ICommandHandler<ProcessGithubEvent>
    {
        public static EventProcessed eventProcessed;
        public static GithubEventRejected eventRejected;
        public static MetricsUpdated metricsUpdated;
        public async Task HandleAsync(ProcessGithubEvent GithubEvent)
        {

        }
    }
}