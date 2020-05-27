using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fls.Supervision.Api.Commands.Handlers
{
    public class GithubQueryHandler : IQueryHandler<GithubQuery, List<ProcessGithubEvent>>
    {
        public Task<List<ProcessGithubEvent>> HandleAsync(GithubQuery query)
        {

            return null;
        }
    }
}
