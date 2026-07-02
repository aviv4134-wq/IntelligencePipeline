using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Calculators
{
    class ClassificationCalculator
    {
        public Classification Calculate(Report report)
        {
            Priority reportPriority = report.Priority;
            string reportType  = report.GetSourceType();
            string[] topSecretContent = ["target", "attack", "missile"];
            string[] secretDiscription = { "border ","weapon"};
            string discription = report.Description;
            PriorityCalculator calculator = new PriorityCalculator(); //create objet to reuse cases



            if (reportPriority == Priority.Critical) return Classification.TopSecret;
            
            if (report is SignalReport signalReport)
            {
                string content = signalReport.Content;
                if (calculator.ContainsKeyword(content, topSecretContent)) //use this class to use this methoad to check 
                    return Classification.TopSecret;
                return Classification.Secret;        //other wise must be secret couse it is signal type report
            }
          
            if (reportPriority == Priority.High) return Classification.Secret;

            else if (calculator.ContainsKeyword(discription, secretDiscription))
                return Classification.Secret;

            if (reportPriority == Priority.Medium) return Classification.Restricted;

            else if (reportType == "Soldier") return Classification.Restricted;

            return Classification.Unclassified;
            
        }
    }
}
