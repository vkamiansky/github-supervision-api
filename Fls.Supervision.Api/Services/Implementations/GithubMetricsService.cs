using Fls.Supervision.Api.Data;
using Fls.Supervision.Api.Commands;
using Fls.Supervision.Api.Domain;
using System;

namespace Fls.Supervision.Api.Services.Implementations 
{
    public class GithubMetricsService
    {
        
        public GithubMetricsService()
        {
        }
        
        public async void RecalculateData(PullRequestRecordData data, NewCommitInfo info)
        {
            throw new NotImplementedException();
            //data.DelayHistory.Add(githubEvent.UpdatedAt.Value - data.LastReviewCommentDate.Value);
        }

        public async void CalculateGap(PullRequestRecordData data, NewReviewInfo info)
        {
            throw new NotImplementedException();
           //data.GapHistory.Add((githubEvent.UpdatedAt.Value - data.LastCommitDate.GetValueOrDefault()));
        }

    }
}