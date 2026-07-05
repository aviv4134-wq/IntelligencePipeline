using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Validation;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Xml;

namespace IntelligencePipeline.Pipeline
{
    class ReportPipeline
    {
        private ReportRepository _validatedReports;
        private RejectedReportRepository _rejectedReports;
        private int _nextReportId ;

        public int NextReportId { get => _nextReportId;}

        //להשים פה פרופרטי לכול השורות 
        public ReportPipeline()
        {
            _validatedReports = new ReportRepository(); 
            _rejectedReports = new RejectedReportRepository();
            _nextReportId = 1;
        }

        public void ProcessReport(Report report)
        {
            _nextReportId++;
            report.Status = ReportStatus.Validating;

            ValidateReport(report);
            if (report.Status == ReportStatus.Validated)
                CalculateMetrics(report);
            StoreReport(report);
        }
            
           
              
              
        public ReportRepository GetValidatedReports()
        {
            return _validatedReports;
        }
        public RejectedReportRepository GetRejectedReports()
        {
            return _rejectedReports;
        }
       
        public void DisplayStatistics()
        {
            string[] sourceTypes = ["Drone", "Soldier", "Signal", "Radar"];
            int countInvalidReports  = _rejectedReports.GetTotalCount();
            int countValdatedReports = _validatedReports.GetTotalCount();
            int countTotalReports = countInvalidReports + countValdatedReports;
            double validatedPercentage = countTotalReports > 0 ? (countValdatedReports * 100.0) / countTotalReports: 0;

            Console.WriteLine($"""
                total reports : {countTotalReports}
                validated reports : {countValdatedReports}
                Invalid reports : {countInvalidReports}
                valid reports rate : {validatedPercentage} 
                """);


            foreach (Priority priority in Enum.GetValues<Priority>())
            {
                Console.WriteLine($"{priority} reports : {_validatedReports.GetCountByPriority(priority)}");
            }

            foreach (ReportStatus status in Enum.GetValues<ReportStatus>())
            {
                Console.WriteLine($"{status} reports : {_validatedReports.GetCountByStatus(status)}");
            }

            foreach (string sourceType in sourceTypes)
            {
                Console.WriteLine($"{sourceType} reports : {CountBySourceType(sourceType)}");
            }
            
        }
       

           private IValidator GetValidator(Report report)
        {
            string reportType = report.GetSourceType();
            IValidator reportValidator = null;

            switch (reportType)
            {
                case "Drone":
                   reportValidator  = new DroneValidator();
                   break;

                case "Soldier":
                    
                   reportValidator = new SoldierValidator();
                   break;

                case "Signal":
                    reportValidator = new SignalValidator();
                    break;

                case "Radar":
                    reportValidator = new RadarValidator();
                    break;
            }
            
            return reportValidator;
        }




        private void ValidateReport(Report report)
        {
            IValidator reportValidation = GetValidator(report);

            ValidationResult valdiationResoult = reportValidation.Validate(report);
            if (valdiationResoult.IsValid == true)
                report.Status = ReportStatus.Validated;
            else
            {
                report.Status = ReportStatus.Rejected;
                report.RejectionReason = valdiationResoult.ErrorMessage;
            }
        }

        private void CalculateMetrics(Report report)
        {
            ReliabilityCalculator calculateReliability = new ReliabilityCalculator();
            int reliabilityScore = calculateReliability.Calculate(report);
            report.ReliabilityScore = reliabilityScore;

            PriorityCalculator calculatePriority = new PriorityCalculator();
            Priority priorityResult = calculatePriority.Calculate(report);
            report.Priority = priorityResult;

            ClassificationCalculator classificationCalculator = new ClassificationCalculator();
            Classification classificationResult = classificationCalculator.Calculate(report);
            report.Classification = classificationResult;

        }
        private void StoreReport(Report report)
        {
            if (report.Status == ReportStatus.Rejected)
                _rejectedReports.Add(report);
            else
                _validatedReports.Add(report);
         }

        private int CountBySourceType(string type)
        {
            int countByType = 0;
            
            foreach (Report report in _validatedReports.GetAll())
            
                if (report.GetSourceType() == type) countByType++;

            foreach (Report report in _rejectedReports.GetAll())

                if (report.GetSourceType() == type) countByType++;

            return countByType; 
        }
    }
}
