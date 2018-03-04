using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Testify.Models;
using Testify.Repositories;

namespace Testify.Services.Implementation
{
    public class ExperimentService : IExperimentService
    {
        private readonly IExperimentRepository _experimentRepository;
        private readonly IUserRepository _userRepository;

        public ExperimentService(IExperimentRepository experimentRepository, IUserRepository userRepository)
        {
            _experimentRepository = experimentRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> IsInVariantAsync(int experimentId, int userId, bool recordParticipation = true)
        {
            var experiment = await _experimentRepository.GetExperimentAsync(experimentId);
            if (!experiment.IsActive)
            {
                return false;
            }

            var user = await _userRepository.GetUserAsync(userId);

            // Do some bit fiddling to make sure we always return the same value
            // Use experiment.SplitType to calculate it based on the userId or the teamId
            int differentiator;
            switch (experiment.SplitType)
            {
                case GroupSplitType.User:
                    differentiator = user.UserId;
                    break;
                case GroupSplitType.Team:
                    differentiator = user.TeamId;
                    break;
                case GroupSplitType.None:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var hash = GetParticipationHash(experiment.ExperimentId, differentiator);
            var variant = (int) (hash % experiment.Variants.Length);

            if (recordParticipation)
            {
                await _experimentRepository.RecordParticipationAsync(experimentId, userId, variant);
            }

            // 0 is always control
            return variant > 0;
        }

        private long GetParticipationHash(int experimentId, int differentiator)
        {
            // return a deterministic hash that combines experiment and differentiator without introducing affinity
            // as found on https://stackoverflow.com/a/13871379/1864167

            var A = (ulong)(experimentId >= 0 ? 2 * (long)experimentId : -2 * (long)experimentId - 1);
            var B = (ulong)(differentiator >= 0 ? 2 * (long)differentiator : -2 * (long)differentiator - 1);
            var C = (long)((A >= B ? A * A + A + B : A + B * B) / 2);
            return experimentId < 0 && differentiator < 0 || experimentId >= 0 && differentiator >= 0 ? C : -C - 1;
        }
    }
}
