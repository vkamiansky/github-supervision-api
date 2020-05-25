using System;
using Convey.CQRS.Events;
using Convey.CQRS.Commands;

namespace Fls.Supervision.Api.Commands
{

    public class ProcessGithubEvent : ICommand
    {
        public class HookPayload
        {
            public long Id { get; set; }
            public string Type { get; set; }
            public string Name { get; set; }
        }
        public string Zen { get; set; }
        public long? HookId { get; set; } // Notice that all value types should be nullable. As they might not be present on some events.
        public HookPayload Hook { get; set; } // This class we define here
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Add more fields using the above code as an example
    }
}
