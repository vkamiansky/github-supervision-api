using Fls.Supervision.Api.Data;
using Fls.Supervision.Api.Domain;
using System;

namespace Fls.Supervision.Api.Services.Implementations 
{
    public static class GithubMetricsService
    {
        public static void RecalculateData(PullRequestRecordData data, NewCommitInfo info)
        {
            data.DelayHistory.Add(data.LastCommitDate.HasValue ? info.CommitDate - data.LastCommitDate.Value : TimeSpan.Zero);
            data.LastCommitDate = info.CommitDate;
        }

        public static void RecalculateGap(PullRequestRecordData data, NewReviewInfo info)
        {
            data.DelayHistory.Add(data.LastReviewCommentDate.HasValue ? info.ReviewDate - data.LastReviewCommentDate.Value : TimeSpan.Zero);
            data.LastReviewCommentDate = info.ReviewDate;
        }

        public static void RecalculateState(PullRequestRecordData data, StateChangedInfo info)
        {
            data.StateHistory.Add(ValueTuple.Create(info.ChangeDate, info.NewState));
        }
    }
}