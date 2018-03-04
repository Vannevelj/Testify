using System;
using System.Collections.Generic;
using System.Text;

namespace Testify.Models
{
    public class Experiment
    {
        public int ExperimentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Hypothesis { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public GroupSplitType SplitType { get; set; }
        public EvaluationType EvaluationType { get; set; }
        public Variant[] Variants { get; set; }
        public bool IsActive { get; set; }
    }
}
