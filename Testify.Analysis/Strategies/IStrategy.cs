using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Testify.Analysis.Models;
using Testify.Models;

namespace Testify.Analysis.Strategies
{
    public interface IAnalysisStrategy
    {
        Task<List<Participation>> GetStrategyResultAsync(Experiment experiment);
    }
}
