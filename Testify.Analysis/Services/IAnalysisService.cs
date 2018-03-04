using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Testify.Analysis.Models;

namespace Testify.Analysis.Services
{
    public interface IAnalysisService
    {
        Task<Significance> GetSignificanceAsync(int experimentId);
    }
}
