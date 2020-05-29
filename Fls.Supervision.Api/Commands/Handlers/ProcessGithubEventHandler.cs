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
        public ProcessGithubEventHandler(IMongoRepository<PullRequestRecordData, long> githubRecordsRepository)
        {
            _githubRecordsRepository = githubRecordsRepository;
        }
    
        private readonly GithubMetricsService service = new GithubMetricsService();
        private readonly IMongoRepository<PullRequestRecordData, long> _githubRecordsRepository;        
        public async Task HandleAsync(ProcessGithubEvent githubEvent)
        {
            // if(!githubEvent.HookId.HasValue)
            //  throw new Exception("HookId must be specified");
            // IInfo info;
            // //PullRequestRecordData data = await _githubRecordsRepository.GetAsync(githubEvent.HookId);
            // switch (githubEvent.Type)
            // {
            //     case CommandType.PullRequest:
            //         info = EventToNewCommitInfo(githubEvent);
            //         service.RecalculateData(data, (NewCommitInfo) info);
            //         break;
            // }
        }

        private NewCommitInfo EventToNewCommitInfo(ProcessGithubEvent githubEvent)
        {
            var info = new NewCommitInfo();
            if (githubEvent.CreatedAt.HasValue && githubEvent.Sender.Login != null)
            {
                info.CommitDate = githubEvent.CreatedAt.Value;
                info.Creator = githubEvent.Sender.Login;
            }
            return info;
        }

    }
}