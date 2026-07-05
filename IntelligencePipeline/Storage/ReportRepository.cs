using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace IntelligencePipeline.Storage
{
    class ReportRepository
    {
        private List<Report> _reports;


        public ReportRepository()
        {
            _reports = new List<Report>();
        }


        public void Add(Report report)
        {
            _reports.Add(report);
        }
        
        public List<Report> GetAll()
        {
            return _reports;
        }
        
        public List<Report> GetByStatus(ReportStatus status)
        {
            List<Report> reportsByStatus = new List<Report>();
            foreach (Report report in _reports)
            {
                if (report.Status == status) reportsByStatus.Add(report); 
            }
            return reportsByStatus;
           
        }
        
        public List<Report> GetByPriority(Priority priority)
        {
            List<Report> reportsBypriority = new List<Report>();
            foreach (Report report in _reports)
            if (report.Priority == priority) reportsBypriority.Add(report);
            
            return reportsBypriority;

        }
        public List<Report> Search(string keyword)
        {
            PriorityCalculator priorityCalculator = new PriorityCalculator();
            List<Report> reportsByKeyword = new List<Report>();
            foreach (Report report in _reports)
            {
                if (priorityCalculator.ContainsKeyword(report.Description, [keyword]) ) reportsByKeyword.Add(report);
            }
            return reportsByKeyword;
         }

        public Report? GetById(int reportId)  //מה קורה אם אין ID כזה מה מחזיר 
        {
            foreach (Report report in _reports)
            {
                if (report.ReportId == reportId) return report;
            }
            return null;
        }
        
        public void UpdateStatus(int reportId, ReportStatus newStatus)
        {
           Report? report = GetById(reportId);
            if (report != null) report.Status = newStatus;
            
            else Console.WriteLine($"error report not found by this id {reportId}");
        }
        
        public int GetTotalCount()
        {
            return _reports.Count;
        }
        
        public int GetCountByStatus(ReportStatus status)
        {
           return GetByStatus(status).Count;
        }
        public int GetCountByPriority(Priority priority)
        {
            return GetByPriority(priority).Count;
        }


    }
}

