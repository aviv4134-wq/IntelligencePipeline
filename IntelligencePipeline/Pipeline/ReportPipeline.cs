using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace IntelligencePipeline.Pipeline
{
    class ReportPipeline
    {
        private ReportRepository _validatedReports;
        private RejectedReportRepository _rejectedReports;
        private int _nextReportId;

        //public  ReportPipeline()
        //{
            
        //}

        public void ProcessReport(Report report)
        {
            report.Status = ReportStatus.Validating;

            IValidator reportValidation = GetValidator(report);
            
            ValidationResult valdiationResoult = reportValidation.Validate(report);
            if (valdiationResoult.IsValid == true)
            {
                report.Status = ReportStatus.Validated;

                ReliabilityCalculator calculateReliability = new ReliabilityCalculator();      
                int reliabilityScore = calculateReliability.Calculate(report);
                report.ReliabilityScore = reliabilityScore;

                PriorityCalculator calculatePriority = new PriorityCalculator();
                Priority priorityResult = calculatePriority.Calculate(report);
                report.Priority = priorityResult;
                
                //ClassificationCalculator.Calculate();
                //store in
            }
            else
            {
              report.Status = ReportStatus.Rejected;
              report.RejectionReason = valdiationResoult.ErrorMessage;
              //stroe in rjected 
            }
            string reportInfo = report.ToString();
            Console.WriteLine(reportInfo);
        }
        //public ReportRepository GetValidatedReports()
        //{

        //}
        //public RejectedReportRepository GetRejectedReports()
        //{

        //}
        public void DisplayStatistics()
        {

        }
       

           private IValidator GetValidator(Report report)
        {
            string reportType = report.GetSourceType();

            switch (reportType)
            {
                case "Drone":
                    return new DroneValidator();

                //case "Soldier":
                //    return new SoldierValidator();

                //case "Satellite":
                //    return new SatelliteValidator();

                //case "Radar":
                //    return new RadarValidator();

                default:
            
                    throw new ArgumentException($"Unknown report type: {reportType}");
                    
            }
        }


    
            
            

        
        private void ValidateReport(Report report)
        {

        }
        private void CalculateMetrics(Report report)
        {

        }
        private void StoreReport(Report report)
        {

        }

    }
}
