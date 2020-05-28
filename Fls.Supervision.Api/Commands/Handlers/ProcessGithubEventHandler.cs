using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Fls.Supervision.Api.Domain;
using Fls.Supervision.Api.Commands;


namespace Fls.Supervision.Api.Commands.Handlers
{
    public class ProcessGithubEventHandler : ICommandHandler<ProcessGithubEvent>
    {
        public async Task HandleAsync(ProcessGithubEvent GithubEvent)
        {
            
        }

        private NewCommitInfo EventToNewCommitInfo(ProcessGithubEvent githubEvent)
        {
            var info = new NewCommitInfo();
            info.CommitDate = githubEvent.CreatedAt;
            return info;
        }

    }
}