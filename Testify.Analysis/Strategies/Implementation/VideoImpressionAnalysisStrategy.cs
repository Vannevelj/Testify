using System.Collections.Generic;
using System.Threading.Tasks;
using Testify.Analysis.Models;
using Testify.Models;

namespace Testify.Analysis.Strategies.Implementation
{
    public class VideoImpressionAnalysisStrategy : IAnalysisStrategy
    {
        public async Task<List<Participation>> GetStrategyResultAsync(Experiment experiment)
        {
            // This would be a strategy-specific implementation of what we want to evaluate the experiment on
            // This should ideally be a very static number of strategies: 
            // if it looks like we add more and more strategies, we might be focusing on a too narrow vision and risk missing the bigger picture

            //select p.Variant, 
            //count(case when vi.UserId is null then 1 else 0 end) as 'NumberOfCompletions'
            //count(*) as 'NumberOfOccurrences'
            //from Participations p
            //    join VideoImpressions vi on p.UserId = vi.UserId
            //where 1 = 1
            //and p.ParticipationDateTime >= experiment.StartDate
            //and p.ParticipationDateTime < getutcdate()
            //and p.ExperimentId = experiment.Id
            //group by p.Variant

            // Alternatively, if we put all Goal data in a new table structure rather than re-using existing stuff, 
            // we can make this very generic and end up with just a single data-retrieval strategy
        }
    }
}
