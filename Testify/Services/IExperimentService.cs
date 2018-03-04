using System.Threading.Tasks;

namespace Testify.Services
{
    public interface IExperimentService
    {
        Task<bool> IsInVariantAsync(int experimentId, int userId, bool recordParticipation = true);
    }
}
