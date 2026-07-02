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

        public ReportPipeline()
        {
            _validatedReports = GetValidatedReports();
            _rejectedReports = GetRejectedReports();
            _nextReportId = 0;
        }

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

                ClassificationCalculator classificationCalculator  = new ClassificationCalculator();
                Classification classificationResult = classificationCalculator.Calculate(report);
                report.Classification = classificationResult;

                
            }
            else
            {
              report.Status = ReportStatus.Rejected;
              report.RejectionReason = valdiationResoult.ErrorMessage;
              
            }
            string reportInfo = report.ToString();
            Console.WriteLine(reportInfo);
        }
        public ReportRepository GetValidatedReports()
        {
            return new ReportRepository();
        }
        public RejectedReportRepository GetRejectedReports()
        {
           return new RejectedReportRepository ();
        }
        public void DisplayStatistics()
        {


        }
       

           private IValidator? GetValidator(Report report)
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

                    return null;
                    
            }
        }


    
        
        private void ValidateReport(Report report)
        {
            IValidator reportValidation = GetValidator(report);

            ValidationResult valdiationResoult = reportValidation.Validate(report);

        }
        private void CalculateMetrics(Report report)
        {

        }
        private void StoreReport(Report report)
        {
            if (report.Status == ReportStatus.Rejected)
                _rejectedReports.Add(report);
            else
                _validatedReports.Add(report);
        }

    }
}
