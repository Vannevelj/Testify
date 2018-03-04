using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.Analysis.Models;
using Testify.Analysis.Strategies;
using Testify.Analysis.Strategies.Implementation;
using Testify.Repositories;

namespace Testify.Analysis.Services.Implementation
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IExperimentRepository _experimentRepository;

        public AnalysisService(IExperimentRepository experimentRepository)
        {
            _experimentRepository = experimentRepository;
        }

        public async Task<Significance> GetSignificanceAsync(int experimentId)
        {
            var experiment = await _experimentRepository.GetExperimentAsync(experimentId);

            IAnalysisStrategy evaluationStrategy = null;
            switch (experiment.EvaluationType)
            {
                case Testify.Models.EvaluationType.VideoImpressions:
                    evaluationStrategy = new VideoImpressionAnalysisStrategy();
                    break;
                case Testify.Models.EvaluationType.VideosInteracted:
                    break;

                case Testify.Models.EvaluationType.None:
                default:
                    break;
            }

            var analyticsData = await evaluationStrategy.GetStrategyResultAsync(experiment);

            return GetSignificance(analyticsData);
        }

        private Significance GetSignificance(List<Participation> data)
        {
            // Simple solution: verify whether the ratio in variant 1 is higher than variant 0
            // Advanced: verify whether there is a significant result (p value < 0.05)

            var control = data.Single(x => x.Variant == 0);
            var variant = data.Single(x => x.Variant == 1);

            var controlEffect = control.NumberOfCompletions / control.NumberOfOccurrences;
            var variantEffect = variant.NumberOfCompletions / variant.NumberOfOccurrences;

            return variantEffect > controlEffect ? Significance.Positive : Significance.Insignificant;
        }
    }
}
