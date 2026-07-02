using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Storage
{
    class RejectedReportRepository
    {
        private List<Report> _rejectedReports;

    
        public RejectedReportRepository()
        {
            _rejectedReports = new List<Report>();
        }

        public void Add(Report report)
        {
            _rejectedReports.Add(report);
        }
        public List<Report> GetAll()
        {
            return _rejectedReports;
        }
        public int GetTotalCount()
        {
            return _rejectedReports.Count;
        }
        public List<Report> GetByReason(string reasonKeyword)
        {
            PriorityCalculator priorityCalculator = new PriorityCalculator();
            List<Report> rejectedByreason = new List<Report>();
            foreach (Report report in _rejectedReports)
            {
                if (priorityCalculator.ContainsKeyword(report.RejectionReason, [reasonKeyword]))
                {
                    rejectedByreason.Add(report);
                }
            }
            return rejectedByreason;
        }


    }
}
