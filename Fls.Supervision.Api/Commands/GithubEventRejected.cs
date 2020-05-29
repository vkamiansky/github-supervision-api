using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fls.Supervision.Api.Commands
{
    public class GithubEventRejected : IRejectedEvent
    {
        public string Reason { get; set; }

        public string Code { get; set; }
    }
}
