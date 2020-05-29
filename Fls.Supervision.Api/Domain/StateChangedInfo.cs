using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fls.Supervision.Api.Data;

namespace Fls.Supervision.Api.Domain
{
    public class StateChangedInfo : IInfo
    {
        public String Author { get; set; }

        public DateTime ChangeDate { get; set; }

        public PullRequestState NewState { get; set; }
    }
}
