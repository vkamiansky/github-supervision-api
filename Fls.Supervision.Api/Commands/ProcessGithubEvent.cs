using System;
using Convey.CQRS.Events;

namespace Fls.Supervision.Api.Commands
{
    public interface IGithubEventPayload
    {
    }

    public class PingPayload : IGithubEventPayload
    {
        public string Zen { get; set; }
    }

    public class ProcessGithubEvent<T> : IEvent
      where T : IGithubEventPayload
    {

        public Guid Id { get; }
        public T Payload { get; private set; }

        public ProcessGithubEvent(Guid id, T payload)
        {
            Id = id;
            Payload = payload;
        }
    }
}
