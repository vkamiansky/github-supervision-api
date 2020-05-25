using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;

namespace Fls.Supervision.Api.Commands.Handlers
{

    public class ProcessGithubEventHandler : ICommandHandler<ProcessGithubEvent>
    {
        public async Task HandleAsync(ProcessGithubEvent GithubEvent)
        {
        }
    }
}