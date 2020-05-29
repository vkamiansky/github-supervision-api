
using System;
using Convey.CQRS.Commands;

namespace Fls.Supervision.Api.Commands
{

    public class ProcessGithubEvent : ICommand
    { 
        public class HookPayload
        {
            public long? Id { get; set; }
            public string Type { get; set; }
            public string Name { get; set; }
        }

        public class RepositoryPayload
        {
            public long? Id { get; set; }
            public string FullName { get; set; }
            public string Url { get; set; }
            public bool? Fork { get; set; }
        }

        public class SenderPayload
        {
            public string Login { get; set; }
            public long? Id { get; set; }
            public string ReceivedEventsUrl { get; set; }
        }
        public CommandType Type { get; set; }
        public string Zen { get; set; }
        public long? HookId { get; set; } // Notice that all value types should be nullable. As they might not be present on some events.
        public HookPayload Hook { get; set; } // This class we define here
        public RepositoryPayload Repository { get; set; } // This class we define here
        public SenderPayload Sender { get; set; } // This class we define here

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Add more fields using the above code as an example

        public string Action { get; set; }
        public int? Number { get; set; }

        public DateTime? ClosedAt { get; set; }
        public DateTime? MergedAt { get; set; }

        public string CommitsUrl { get; set; }
        public string CommentsUrl { get; set; }
    }

}
