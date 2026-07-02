using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Calculators
{
    class ReliabilityCalculator
    {
        public int Calculate(Report report)
        {
            int baseScore = report.CalculateReliabilityScore();
            
            if (baseScore > 10) baseScore = 10;
            else if (baseScore < 1) baseScore = 1;
            
            return baseScore;
        } 
    }
}
