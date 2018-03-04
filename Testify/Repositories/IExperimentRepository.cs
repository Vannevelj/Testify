using System.Threading.Tasks;
using Testify.Models;

namespace Testify.Repositories
{
    public interface IExperimentRepository
    {
        Task<Experiment> GetExperimentAsync(int experimentId);
        Task RecordParticipationAsync(int experimentId, int userId, int variant);
    }
}
