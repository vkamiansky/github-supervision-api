using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Events;

public class ProcessGithubPingHandler<PingPayload> : IEventHandler<ProcessGithubEvent<PingPayload>>
{
    public Task HandleAsync(ProcessGithubEvent<PingPayload> GithubEvent)
    {
        //do something
        return new Task(() => {});
    }
}