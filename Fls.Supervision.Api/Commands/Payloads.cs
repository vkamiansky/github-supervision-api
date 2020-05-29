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
    public class smhngPayload : IGithubEventPayload
    {

    }
}
