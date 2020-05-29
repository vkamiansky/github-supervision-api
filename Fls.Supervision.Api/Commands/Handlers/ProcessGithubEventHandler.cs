using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;
using Fls.Supervision.Api.Data;
using Fls.Supervision.Api.Data.Repositories;
using Fls.Supervision.Api.Domain;
using Fls.Supervision.Api.Services.Implementations;
using MongoDB.Driver;

namespace Fls.Supervision.Api.Commands.Handlers
{
    public class ProcessGithubEventHandler : ICommandHandler<ProcessGithubEvent>
    {
        private GithubMetricsService service = new GithubMetricsService();
        private PullRequestRecordRepository repository =
            new PullRequestRecordRepository(new MongoDbOptions(), new MongoClient());
        
        public async Task HandleAsync(ProcessGithubEvent githubEvent)
        {
            IInfo info;
            PullRequestRecordData data = await repository.GetAsync(new Guid(githubEvent.HookId.ToString()));
            switch (githubEvent.Type)
            {
                case CommandType.PullRequest:
                    info = EventToNewCommitInfo(githubEvent);
                    service.RecalculateData(data, (NewCommitInfo) info);
                    break;
            }
            
        }

        private NewCommitInfo EventToNewCommitInfo(ProcessGithubEvent githubEvent)
        {
            var info = new NewCommitInfo();
            if (githubEvent.CreatedAt!=null && githubEvent.Sender.Login != null)
            {
                info.CommitDate = githubEvent.CreatedAt;
                info.Creator = githubEvent.Sender.Login;
            }
            return info;
        }

    }
}