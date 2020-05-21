using Newtonsoft.Json;
using Convey.CQRS.Events;
using System;
using Fls.Supervision.Api.Commands;
namespace Fls.Supervision.Api.Services 
{
    class GithubMetricsService
    {
        static IEvent ParseGithubEvent(string data) 
        {
            dynamic t = JsonConvert.DeserializeObject(data);
            Guid id = Guid.NewGuid();
            IGithubEventPayload payload = null;
            switch(t.action)
                {
                //TODO
                    case("added"):
                        break;
                    default:
                        break;
                }
            return new GithubEvent<IGithubEventPayload>(id, payload);
        }
    }

}